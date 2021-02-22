using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FomemaMedicalCard.CommonClass;
using FomemaMedicalCard.DataLayer;
using FomemaMedicalCard.Models;
using Newtonsoft.Json;
using static FomemaMedicalCard.Models.CommonEnums;

namespace FomemaMedicalCard.Controllers
{
    public class CandidateController : Controller
    {
        // GET: Candidate
        [RequireHttps]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CandidateDetails()
        {
            if (Session["UserId"] != null && Session["UserType"] != null)
            {
                UserType type = (UserType)Convert.ToInt32(Session["UserType"]);
                ViewBag.UserType = type;
                //return View(type); hiren code
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public JsonResult GetAllCandidate()
        {
            int UserType = 0;
            if (Request.Form.AllKeys.Contains("UserType"))
            {
                UserType = Convert.ToInt32(Request.Form["UserType"]);
            }
            string SearchPassportNo = "";
            if (Request.Form.AllKeys.Contains("SearchPassportNo"))
            {
                SearchPassportNo = Request.Form["SearchPassportNo"];
            }

            string CandidateCardNumber = "";
            if (Request.Form.AllKeys.Contains("CandidateCardNumber"))
            {
                CandidateCardNumber = Request.Form["CandidateCardNumber"];
            }
            string Name = "";
            if (Request.Form.AllKeys.Contains("Name"))
            {
                Name = Request.Form["Name"];
            }
            int Age = 0;
            if (Request.Form.AllKeys.Contains("Age"))
            {
                Age = Convert.ToInt32(Request.Form["Age"]);
            }
            string NewPassportNo = "";
            if (Request.Form.AllKeys.Contains("NewPassportNo"))
            {
                NewPassportNo = Request.Form["NewPassportNo"];
            }
            string OldPassportNo = "";
            if (Request.Form.AllKeys.Contains("OldPassportNo"))
            {
                OldPassportNo = Request.Form["OldPassportNo"];
            }
            string CountryName = "";
            if (Request.Form.AllKeys.Contains("CountryName"))
            {
                CountryName = Request.Form["CountryName"];
            }
            int FomemaTestYear = 0;
            if (Request.Form.AllKeys.Contains("FomemaTestYear"))
            {
                FomemaTestYear = Convert.ToInt32(Request.Form["FomemaTestYear"]);
            }
            string FomemaTestResult = "";
            if (Request.Form.AllKeys.Contains("FomemaTestResult"))
            {
                FomemaTestResult = Request.Form["FomemaTestResult"];
            }
            int pageIndex = 0;
            if (Request.Form.AllKeys.Contains("pageIndex"))
            {
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            }
            int pageSize = 0;
            if (Request.Form.AllKeys.Contains("pageSize"))
            {
                pageSize = Convert.ToInt32(Request.Form["pageSize"]);
            }
            string SortField = "";
            if (Request.Form.AllKeys.Contains("sortField"))
            {
                SortField = Request.Form["sortField"];
            }
            string SortOrder = "";
            if (Request.Form.AllKeys.Contains("sortOrder"))
            {
                SortOrder = Request.Form["sortOrder"];
            }

            Tuple<List<CandidateModel>, int> candidateModel = FomemaDBUtility.GetAllCandidateForGrid(pageIndex, pageSize, SortField, SortOrder, CandidateCardNumber, Name, Age, NewPassportNo, OldPassportNo, CountryName, FomemaTestYear, FomemaTestResult, UserType, SearchPassportNo);
            var json = JsonConvert.SerializeObject(candidateModel);

            return Json(json, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">GUID of candidate</param>
        /// <returns></returns>
        public ActionResult CandidateMedicalCard(string id)
        {
            CandidateModel candidateModel = FomemaDBUtility.GetCandidateMedicalCardDetails(id);
            if (string.IsNullOrEmpty(candidateModel.PictureURL))
            {
                candidateModel.PictureURL = "/Content/Images/default-avatar-profile-icon.png";
            }
            return View(candidateModel);
        }

        [HttpPost]
        public ActionResult SaveCandidateDetails(CandidateModel candidateModel)
        {
            int cadidateId = 0;
            //candidateModel.CandidateId = 30;
            //return Json(candidateModel);
            //return Json(isEntrySave, JsonRequestBehavior.AllowGet);
            if (candidateModel.CandidateGuid == null || candidateModel.CandidateGuid == Guid.Empty)
            {
                candidateModel.CandidateGuid = Guid.NewGuid();
            }
            if (!string.IsNullOrEmpty(candidateModel.CandidateCardNumber))
            {
                candidateModel.CandidateCardNumberEncrypted = Encrypt.EncryptString(candidateModel.CandidateCardNumber);
            }

            candidateModel.ClinicLocation = candidateModel.ClinicLocation ?? "";
            candidateModel.ClinicName = candidateModel.ClinicName ?? "";
            candidateModel.CountryName = candidateModel.CountryName ?? "";
            candidateModel.FomemaTestResult = candidateModel.FomemaTestResult ?? "";
            candidateModel.Name = candidateModel.Name ?? "";
            candidateModel.NewPassportNo = candidateModel.NewPassportNo ?? "";
            candidateModel.OldPassportNo = candidateModel.OldPassportNo ?? "";

            candidateModel.CandidateCardNumber = candidateModel.CandidateCardNumber.Replace("-", "");
            candidateModel.PictureURL = SaveAndGetImageUrl(candidateModel.CandidateGuid, candidateModel.PictureURL, candidateModel.PictureFromComputer);
            candidateModel.PictureFromComputer = null;
            cadidateId = FomemaDBUtility.IUDCandidateDetails(candidateModel, candidateModel.CandidateId > 0 ? DataOperationMode.Update : DataOperationMode.Insert);
            if (cadidateId > 0)
            {
                candidateModel.CandidateId = cadidateId;
                string CardNumber = Convert.ToString(candidateModel.CandidateCardNumber);
                candidateModel.CandidateCardNumber = string.Format("{0}-{1}-{2}", CardNumber.Substring(0, 3),
                          CardNumber.Substring(3, 3), CardNumber.Substring(6));

                candidateModel.DateofBirth = GetFormatedDate(candidateModel.DateofBirth);
                candidateModel.Covid19TestDate1 = GetFormatedDate(candidateModel.Covid19TestDate1);
                candidateModel.Covid19TestDate2 = GetFormatedDate(candidateModel.Covid19TestDate2);
                candidateModel.Covid19TestDate3 = GetFormatedDate(candidateModel.Covid19TestDate3);
                candidateModel.Covid19TestDate4 = GetFormatedDate(candidateModel.Covid19TestDate4);
                candidateModel.VaccineDose1Date = GetFormatedDate(candidateModel.VaccineDose1Date);
                candidateModel.VaccineDose2Date = GetFormatedDate(candidateModel.VaccineDose2Date);
                return Json(candidateModel, JsonRequestBehavior.AllowGet);
            }
            return Json(cadidateId, JsonRequestBehavior.AllowGet);
        }
        string GetFormatedDate(string dateinyyyyformat)
        {
            return !string.IsNullOrEmpty(dateinyyyyformat) ? Convert.ToDateTime(dateinyyyyformat).ToString("dd-MM-yyyy") : null;
        }

        public JsonResult ValidateCardNumber(FormCollection form)
        {
            try
            {
                bool result = true;
                string message = string.Empty;
                string cardNumber = form["cardnumber"];
                bool isNew = Convert.ToBoolean(form["isNew"]);

                DataTable dtCard = FomemaDBUtility.GetCardListRecord(cardNumber);
                if (dtCard.Rows.Count <= 0)
                {
                    result = false;
                    message = "This card number is invalid.";
                }

                if (isNew)
                {
                    DataTable dtCardMaster = FomemaDBUtility.GetCardMasterRecord(cardNumber);
                    if (dtCardMaster.Rows.Count > 0)
                    {
                        result = false;
                        message = "This card is already used for another candidate.";
                    }
                }



                return Json(new { Result = result, Message = message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exObj)
            {
                throw exObj;
            }
        }

        [HttpPost]
        public ActionResult DeleteCandidate(CandidateModel candidateModel)
        {
            int candidateId = 0;
            candidateId = FomemaDBUtility.IUDCandidateDetails(candidateModel, DataOperationMode.Delete);
            return Json(candidateId > 0, JsonRequestBehavior.AllowGet);
        }
        #region Utilities
        string SaveAndGetImageUrl(Guid candidateGUID, string capturedImageBase64 = "", HttpPostedFileBase PictureFromComputer = null)
        {
            string uploadImagePath = Server.MapPath(Path.Combine(FomemaDBUtility.ImageUploadRootPath, FomemaDBUtility.CadidatePhotoFolderName));
            string fileName = candidateGUID.ToString();
            string dbFileSavePath = FomemaDBUtility.CadidatePhotoFolderName;
            if (!Directory.Exists(uploadImagePath))
            {
                Directory.CreateDirectory(uploadImagePath);
            }
            if (PictureFromComputer != null)
            {
                fileName += Path.GetExtension(PictureFromComputer.FileName);
                dbFileSavePath = Path.Combine(FomemaDBUtility.ImageUploadRootPath, dbFileSavePath, fileName);
                uploadImagePath = Path.Combine(uploadImagePath, fileName);
                PictureFromComputer.SaveAs(uploadImagePath);
            }
            else if (!string.IsNullOrWhiteSpace(capturedImageBase64) && capturedImageBase64.Contains("data:image/jpeg;base64,"))
            {
                fileName += ".jpeg"; // because we are getting captured image in jpeg and we are replace base64.Replace("data:image/jpeg;base64,", "");
                dbFileSavePath = Path.Combine(FomemaDBUtility.ImageUploadRootPath, dbFileSavePath, fileName);
                uploadImagePath = Path.Combine(uploadImagePath, fileName);
                // using camera uploaded image save in location.
                SaveBase64Picture(capturedImageBase64, uploadImagePath);
            }
            else if (!string.IsNullOrWhiteSpace(capturedImageBase64) && !capturedImageBase64.Contains("undefined"))
            {
                dbFileSavePath = capturedImageBase64;
            }
            else
            {
                return null;
            }
            return dbFileSavePath;
        }
        public void SaveBase64Picture(string base64, string filenameWithPath)  // Drawing image from Base64 string.  
        {
            base64 = base64.Replace("data:image/jpeg;base64,", "");
            using (FileStream fs = new FileStream(filenameWithPath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(base64);
                    bw.Write(data);
                    bw.Close();
                }
            }
        }
        #endregion

    }

}
