using DataEntry_Tracker.Models;
using DataEntry_Tracker.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataEntry_Tracker.Controllers
{
    public class HomeController : Controller
    {
        HomeDAL homedal = new HomeDAL();
        public ActionResult Index()
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult GetUserInformation()
        {

            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["TrackerUserId"].ToString());
            UserInformation users = homedal.GetUserInformation(userID);
            return Json(users, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult RecoveryPassword(string Email)
        {
            bool result = homedal.RecoveryPassword(Email);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ChangePassword(string email, string oldpass, string newpass)
        {
            bool result = false;
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["TrackerUserId"].ToString());
            UserInformation users = homedal.CheckUserLogin(email, oldpass);
            if (users.Empty)
            {
                result = false;
            }
            else
            {
                result = homedal.changePassword(userID, newpass);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}