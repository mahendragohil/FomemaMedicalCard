using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FomemaMedicalCard.DataLayer;
using FomemaMedicalCard.Models;
using Newtonsoft.Json;
using static FomemaMedicalCard.Models.CommonEnums;

namespace FomemaMedicalCard.Controllers
{
    public class CandidateController : Controller
    {
        // GET: Candidate
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

            Tuple<List<CandidateModel>, int> candidateModel = FomemaDBUtility.GetAllCandidateForGrid(pageIndex, pageSize, SortField, SortOrder, Name, Age, NewPassportNo, OldPassportNo, CountryName, FomemaTestYear, FomemaTestResult, UserType, SearchPassportNo);
            var json = JsonConvert.SerializeObject(candidateModel);

            return Json(json, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">GUID of candidate</param>
        /// <returns></returns>
        public ActionResult CandidateMedicalCard(Guid id)
        {
            CandidateModel candidateModel = FomemaDBUtility.GetCandidateMedicalCardDetails(id);
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
            candidateModel.PictureURL = SaveAndGetImageUrl(candidateModel.CandidateGuid, candidateModel.PictureURL, candidateModel.PictureFromComputer);
            cadidateId = FomemaDBUtility.IUDCandidateDetails(candidateModel, candidateModel.CandidateId > 0 ? DataOperationMode.Update : DataOperationMode.Insert);
            if (cadidateId > 0)
            {
                candidateModel.CandidateId = cadidateId;
                return Json(candidateModel, JsonRequestBehavior.AllowGet);
            }
            return Json(cadidateId, JsonRequestBehavior.AllowGet);
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
