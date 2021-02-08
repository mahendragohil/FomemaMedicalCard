using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FomemaMedicalCard.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Session.Abandon();
            return View();
        }
        public ActionResult About()
        {
            return View(viewName: "About");
        }
    }
}