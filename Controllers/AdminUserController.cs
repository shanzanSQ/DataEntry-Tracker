using DataEntry_Tracker.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataEntry_Tracker.Controllers
{
    public class AdminUserController : Controller
    {
        AdminUserDAL adminUserDal = new AdminUserDAL();
        public ActionResult AdminUserView()
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            return View();
        }
        [HttpPost]
        public ActionResult AllMasterRequest()
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userid = Convert.ToInt32(Session["TrackerUserId"]);
            return PartialView("_allRequestPartialView", adminUserDal.AllMasterRequest(userid));
        }
    }
}