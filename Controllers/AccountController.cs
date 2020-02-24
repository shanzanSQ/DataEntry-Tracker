
using DataEntry_Tracker.Models;
using DataEntry_Tracker.Models.DAL;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DataEntry_Tracker.Controllers { 
    public class AccountController : Controller
    {

        HomeDAL homeDAL = new HomeDAL();

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckLogin(string UserEmail,string UserPassword)
        {
            //ResultResponse result = new ResultResponse();
            ResultResponse result = new ResultResponse();
            UserInformation users = homeDAL.CheckUserLogin(UserEmail, UserPassword);
            if (users.Empty)
            {

                result.isSuccess = true;
                result.msg = "Wrong Username Or Password";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                
                List<ModuleClassModel> moduleList = new List<ModuleClassModel>();
                moduleList = homeDAL.GetModuleByuUser(users.UserInformationId);
                if (moduleList.Count <= 0)
                {
                    result.isSuccess = true;
                    result.msg = "You Don't Have Permission To This System";
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    result.isSuccess = false;
                    result.msg = Url.Action(moduleList[0].ModuleValue, moduleList[0].ModuleController);
                    Session["TrackerUserId"] = users.UserInformationId;
                    Session["TrackerUserInfo"] = users.UserInformationName;
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }

        }
        
        [HttpPost]
        public ActionResult LoadPermissionMenu()
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userid = Convert.ToInt32(Session["TrackerUserId"]);
            List<ModuleClassModel> moduleList = new List<ModuleClassModel>();
            moduleList = homeDAL.GetModuleByuUser(userid);
            return Json(moduleList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            bool result = true;
            Session.Clear();
            Session.Abandon();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RecoveryPassword(string Email)
        {
            bool result = homeDAL.RecoveryPassword(Email);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


    }
}