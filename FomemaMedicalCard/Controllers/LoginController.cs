using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FomemaMedicalCard.Models;
using FomemaMedicalCard.DataLayer;
using System.Data;

namespace FomemaMedicalCard.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Login(FormCollection form)
        {
            try
            {
                string userId = form["userId"];
                string password = form["password"];
                bool isAuthorize = false;

                if (form != null)
                {

                    LoginModel login = new LoginModel
                    {
                        UserId = userId,
                        Password = password
                    };

                    DataTable dtAuthorizedUser = FomemaDBUtility.ValidateUserLogin(login);

                    if (dtAuthorizedUser.Rows.Count > 0)
                    {
                        isAuthorize = true;
                        Session["UserId"] = Convert.ToString(dtAuthorizedUser.Rows[0]["UserUniqueId"]);
                        Session["UserType"] = Convert.ToString(dtAuthorizedUser.Rows[0]["UserType"]);
                    }

                    return Json(isAuthorize, JsonRequestBehavior.AllowGet);

                }    

                return Json("");

            }
            catch (Exception exObj)
            {
                throw exObj;
            }
        }

        public JsonResult ClinicRegistrationSubmit(FormCollection form)
        {
            try
            {
                string ClinicName = form["clinicname"];
                string DoctorName = form["doctorsname"];
                string DoctorsICNumber = form["doctorsicnumber"];
                string Password = form["password"];

                ClinicRegistrationModel clinicRegistration = new ClinicRegistrationModel
                {
                    ClinicName = ClinicName,
                    DoctorName = DoctorName,
                    DoctorsICNumber = DoctorsICNumber,
                    Password = Password
                };

                var isEntrySave = FomemaDBUtility.ClinicRegistrationSave(clinicRegistration);

                if (isEntrySave)
                {
                    Session["UserId"] = clinicRegistration.DoctorsICNumber;
                    Session["Password"] = clinicRegistration.Password;
                    Session["UserType"] = CommonEnums.UserType.Clinic.GetHashCode();
                }
                else
                {
                    Session.Abandon();
                }

                return Json(isEntrySave, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exObj)
            {
                throw exObj;
            }
        }

        public JsonResult CheckICNumberAvailibility(FormCollection form)
        {
            try
            {
                bool result = false;
                string DoctorsICNumber = form["doctorsicnumber"];

                DataTable dtResult = FomemaDBUtility.CheckICNumberAvailibility(DoctorsICNumber);

                result = (dtResult.Rows.Count <= 0);

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exObj)
            {
                throw exObj;
            }
        }
    }
}