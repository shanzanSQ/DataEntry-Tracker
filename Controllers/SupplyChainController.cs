using DataEntry_Tracker.DAL;
using DataEntry_Tracker.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DataEntry_Tracker.Controllers
{
    public class SupplyChainController : Controller
    {
        SupplyChainDAL supplyChain = new SupplyChainDAL();
        TrackerDAL trackerDAL = new TrackerDAL();
        // GET: SupplyChain
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SupplyChainCoordinator()
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            return View();
        }

        public ActionResult SupplyUserView()
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            return View();
        }

        public ActionResult GetSupplyChainRequest(int Progress,int UserType)
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["TrackerUserId"].ToString());
            
            if (UserType == 1)
            {
                return PartialView("_supplyChainMenuMerchand", supplyChain.GetSupplyChainRequest(userID, Progress, UserType));
            }
            else if (UserType == 2)
            {
                return PartialView("_coordinatorPartial", supplyChain.GetSupplyChainRequest(userID, Progress, UserType));
            }
            else
            {
                return PartialView("_userPartialView", supplyChain.GetSupplyChainRequest(userID, Progress, UserType));
            }
        }
        public ActionResult GetDetailsSupplyChain(int SupRequestId,int UserType)
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["TrackerUserId"].ToString());
            if (UserType == 1)
            {
                return PartialView("_supplyChainDetailsMarchand", supplyChain.GetDetailsRequestSupply(userID, SupRequestId));
            }else if (UserType == 2)
            {
                return PartialView("_CoordinatorDetailsPartial", supplyChain.GetDetailsRequestSupply(userID, SupRequestId));
            }
            else
            {
                return PartialView("_userDetailsView", supplyChain.GetDetailsRequestSupply(userID, SupRequestId));
            }
           
        }

        public ActionResult AssigntoUsers(int SupplyChainId,int AssignTo)
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["TrackerUserId"].ToString());
            return Json(supplyChain.AssignToUsers(SupplyChainId,userID,AssignTo),JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SupplyChainUpdateTime(int SupplyChainID, int Progress)
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userid = Convert.ToInt32(Session["TrackerUserId"]);
            ResultResponse result = new ResultResponse();
            if (Progress == 3)
            {
                if (trackerDAL.CheckDataEntryEngagged(userid,2) == 0)
                {
                    return Json(supplyChain.SupplyChainUpdateTime(SupplyChainID, userid, Progress), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(supplyChain.SupplyChainUpdateTime(SupplyChainID, userid, Progress), JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult SavePoAginistRequest(RequestSupplyChain requestSupplyChain)
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userid = Convert.ToInt32(Session["TrackerUserId"]);
            return Json(supplyChain.SavePoToRequest(requestSupplyChain,userid), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult LOPoNameBYRequest(int RequestID)
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userid = Convert.ToInt32(Session["TrackerUserId"]);
            List<LoadPoCatagory> comolList =supplyChain.LoadPoByRequest(RequestID);
            return Json(comolList, JsonRequestBehavior.AllowGet);
        }

    }
}