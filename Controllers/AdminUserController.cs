using DataEntry_Tracker.DAL;
using DataEntry_Tracker.Models;
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
        TrackerDAL trackerDal = new TrackerDAL();

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

        public ActionResult GetRequestAdmin(int RequestId)
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["TrackerUserId"].ToString());
            CommonModel returnmodel = trackerDal.GetRequestInformationByReId(RequestId);
            returnmodel.RevisionList = new List<CommonModel>();
            returnmodel.RevisionList = trackerDal.GetMerchandRivisionProgress(RequestId);
            return PartialView("_AdminShowAllTaskByRequest", returnmodel);
        }
        public ActionResult DataEntryAssignProgress()
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["TrackerUserId"].ToString());
            return Json(adminUserDal.CoordinatorDashboard(userID), JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetCoordinatorFilesDatabase(int RequestId, int Reqcod, int OperationId, int Revision, string Partial)
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userid = Convert.ToInt32(Session["TrackerUserId"]);
            CommonModel comolList = trackerDal.DetailsInformationRequest(RequestId, OperationId, Revision, userid, Reqcod);
            return PartialView(Partial, comolList);
        }
        public ActionResult GetAdminPending(int Status)
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["TrackerUserId"].ToString());
            List<CommonModel> commonlist = adminUserDal.GetAdminPending(userID, Status);
            return PartialView("_adminTaskPartial", commonlist);
        }
        [HttpPost]
        public ActionResult GetDataEntryReport(string FormDate,string ToDate)
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["TrackerUserId"].ToString());
            List<ReportModel> Reportlist = adminUserDal.GetDataEntryReport(FormDate, ToDate);
            return PartialView("_reportdataTableadmin", Reportlist);
        }
    }
}