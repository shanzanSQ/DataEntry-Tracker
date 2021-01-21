using DataEntry_Tracker.DAL;
using DataEntry_Tracker.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataEntry_Tracker.Controllers
{
    public class TrackerController : Controller
    {

        TrackerDAL trackerDAL = new TrackerDAL();
        // GET: Tracker
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetUnitsFromDataBase()
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userid = Convert.ToInt32(Session["TrackerUserId"]);
            List<CommonModel> comolList = trackerDAL.GetUnitsDatabase(userid);
            return Json(comolList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetBuyerFromDataBase()
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userid = Convert.ToInt32(Session["TrackerUserId"]);
            List<CommonModel> comolList = trackerDAL.GetBuyerFromDatabase(userid);
            return Json(comolList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult LoadPoCatagory()
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userid = Convert.ToInt32(Session["TrackerUserId"]);
            List<LoadPoCatagory> comolList = trackerDAL.LoadPoFromDataBase(userid);
            return Json(comolList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetAllBaseOperationsDatabase()
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userid = Convert.ToInt32(Session["TrackerUserId"]);
            List<CommonModel> baselist = trackerDAL.GetAllBasOperation(userid);
            return Json(baselist, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetSeasonFromDatabase(int buyerId)
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userid = Convert.ToInt32(Session["TrackerUserId"]);
            List<CommonModel> comolList = trackerDAL.GetSeasonFromDatabas(buyerId, userid);
            return Json(comolList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetStylesFromFromDatabase(int seasonId)
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userid = Convert.ToInt32(Session["TrackerUserId"]);
            List<CommonModel> comolList = trackerDAL.GetStylesFromDatabase(seasonId,userid);
            return Json(comolList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetFilesFromDatabase(int RequestId,int Status)
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userid = Convert.ToInt32(Session["TrackerUserId"]);
            List<FileUploadModel> comolList = trackerDAL.GetFilesByRivisionRequest(RequestId,Status);
            return PartialView("_showAllNewUploadFiles", comolList);
        }

        public ActionResult GetCoordinatorFilesDatabase(int RequestId, int Reqcod, int OperationId,int Revision,string Partial)
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userid = Convert.ToInt32(Session["TrackerUserId"]);
            CommonModel comolList = trackerDAL.DetailsInformationRequest(RequestId,OperationId,Revision,userid,Reqcod);
            return PartialView(Partial, comolList);
        }

        public ActionResult CoordinatorDetailsFirstime(int RequestId, int Reqcod, int OperationId, int Revision, string Partial,int Status)
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userid = Convert.ToInt32(Session["TrackerUserId"]);
            if (Status == 0)
            {
                bool res = trackerDAL.CordinatorTimeUpdate(Reqcod);
            }
            CommonModel comolList = trackerDAL.DetailsInformationRequest(RequestId, OperationId, Revision, userid, Reqcod);
            return PartialView(Partial, comolList);
        }

        public ActionResult CreateSupplyChain(int RequestId, int NumberTrans,string Partial)
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userid = Convert.ToInt32(Session["TrackerUserId"]);

            CommonModel comolList = new CommonModel();
            comolList.RequestCodId = RequestId;
            comolList.NoOfTransections = NumberTrans;
            return PartialView(Partial, comolList);
        }


        public ActionResult CreateMarchandiseRequest()
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }

            return View();
        }
        public ActionResult MerchandiseCreateView()
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }

            return View();
        }
        public ActionResult DataEntryView()
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }

            return View();
        }

        public ActionResult CoordinatorView()
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }

            return View();
        }
        [HttpPost]
        public ActionResult AllRequestById()
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userid = Convert.ToInt32(Session["TrackerUserId"]);
            return PartialView("_allRequestPartialView",trackerDAL.GetAllRequestById(userid));
        }

        [HttpPost]
        public ActionResult DeleteFiles(int FileuploadId,int TableName)
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["TrackerUserId"].ToString());
            string filepath = trackerDAL.DeleteFile(FileuploadId,userID,TableName);
            bool result = false;
                    try
                    {
                        System.IO.File.Delete(filepath);
                        result = true;
                    }
                    catch (IOException ioExp)
                    {
                        Console.WriteLine(ioExp.Message);
                        result = false;
                    }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GenarateNewRequest(CommonModel commonModel)
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["TrackerUserId"].ToString());
            CommonModel returnmodel = trackerDAL.SaveRequestReturnPrimarykey(commonModel,userID);
            returnmodel.BuyerName = Server.HtmlDecode(commonModel.BuyerName);
            return PartialView("_merchandiseOperationCreation", returnmodel);
        }

        [HttpPost]
        public ActionResult GetRequestInformationById(int RequestId)
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userID = Convert.ToInt32(Session["TrackerUserId"].ToString());
            CommonModel returnmodel = trackerDAL.GetRequestInformationByReId(RequestId);
            returnmodel.RevisionList = new List<CommonModel>();
            returnmodel.RevisionList = trackerDAL.GetMerchandRivisionProgress(RequestId);
            return PartialView("_merchandiseOperationCreation", returnmodel);
        }

        public ActionResult GetPendingRequest(int Pending,int DataOrCo)
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userid = Convert.ToInt32(Session["TrackerUserId"]);
            List<CommonModel> commonModels = new List<CommonModel>();
            commonModels = trackerDAL.GetAllPendingRequest(Pending,userid,DataOrCo);
            if (DataOrCo == 1)
            {
                return PartialView("_coordinatorAllRequestPartial", commonModels);
            }else if (DataOrCo == 3)
            {
                return PartialView("_merchandiserPartialRequest", commonModels);
            }
            else if(DataOrCo==2)
            {
                return PartialView("_dataEntryPartialViewRequest", commonModels);
            }
            else
            {
                return PartialView("_pendingRequestPartial", commonModels);
            }
        }
        public ActionResult GetTrackerUser(int Module)
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            return Json(trackerDAL.GetAllTrackerUsers(Module),JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveChatMessageToDatabase(string Message,int SendTo)
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userid = Convert.ToInt32(Session["TrackerUserId"]);
            return Json(trackerDAL.SaveChatToDatabase(SendTo,userid,Message), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllChattingMessage(int SendTo)
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userid = Convert.ToInt32(Session["TrackerUserId"]);
            return Json(trackerDAL.GetAllChattingMessage(SendTo, userid), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SubmitToTracker(int RequestId, string Instructions,int OperationId,int Priority,int NoOfTransections)
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userid = Convert.ToInt32(Session["TrackerUserId"]);
            return Json(trackerDAL.SubmitRequestToTracker(RequestId,userid,Instructions,OperationId,Priority,NoOfTransections),JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SubmitToSupplyChain(int RequestId, string Remarks, string CatagoryId,string OttDate,int PoNumber)
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userid = Convert.ToInt32(Session["TrackerUserId"]);
            return Json(trackerDAL.SubmitTOSupplyChain(RequestId,Remarks,CatagoryId,OttDate,userid,PoNumber), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult AssignRequestDataEntry(int RequestId,int AssignTo,int UpdateSLA)
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userid = Convert.ToInt32(Session["TrackerUserId"]);
            if (AssignTo==0)
            {
                return Json(trackerDAL.AssignToDataEntryOperator(RequestId, userid, userid,UpdateSLA), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(trackerDAL.AssignToDataEntryOperator(RequestId, userid, AssignTo,UpdateSLA), JsonRequestBehavior.AllowGet);
            }
            
        }

        [HttpPost]
        public ActionResult DataEntryUpdateTime(int RequestId, int Progress)
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userid = Convert.ToInt32(Session["TrackerUserId"]);
            ResultResponse result = new ResultResponse();
            if(Progress==3 || Progress == 5)
            {
                if (trackerDAL.CheckDataEntryEngagged(userid,1) == 0)
                {
                    return Json(trackerDAL.DataEntryUpdateTime(RequestId, userid, Progress), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(trackerDAL.DataEntryUpdateTime(RequestId, userid, Progress), JsonRequestBehavior.AllowGet);
            }
            
        }
        public FileResult DownloadFile(string filepath, string filename)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
            string fileName = filename;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, Path.GetFileName(filepath));
        }


        [HttpPost]
        public ActionResult UploadFiles()
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }

            int userID = Convert.ToInt32(Session["TrackerUserId"].ToString());
            List<FileUploadModel> fileuploadList = new List<FileUploadModel>();
            if (Request.Files.Count > 0)
            {
                var files = Request.Files;
                var optionId = Convert.ToInt32(Request.Form["formdataId"]);
                var requestId = Convert.ToInt32(Request.Form["requestId"]);
                int rivisionNo = Convert.ToInt32(Request.Form["rivisiono"]);
                var instructions = Request.Form["instructions"];
                foreach (string str in files)
                {
                    HttpPostedFileBase file = Request.Files[str] as HttpPostedFileBase;
                    //Checking file is available to save.  
                    if (file != null)
                    {
                        var currentmilse = DateTime.Now.Ticks;
                        var InputFileName = Path.GetFileNameWithoutExtension(file.FileName);
                        var InputFileExtention = Path.GetExtension(file.FileName);
                        var FullFileWithext = InputFileName + currentmilse + InputFileExtention;
                        var ServerSavePath = Path.Combine(Server.MapPath("~/Uploads/") + FullFileWithext);
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);
                            FileUploadModel fileUploadModel = trackerDAL.FileUploadsToDataBase(optionId, requestId, file.FileName.ToString(), ServerSavePath, instructions, userID, rivisionNo);
                            fileuploadList.Add(fileUploadModel);
                    }
                }
            }
            return Json(fileuploadList,JsonRequestBehavior.AllowGet);
        }
        public ActionResult RmpoFileUpload()
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }

            int userID = Convert.ToInt32(Session["TrackerUserId"].ToString());
            List<FileUploadModel> fileuploadList = new List<FileUploadModel>();
            if (Request.Files.Count > 0)
            {
                var files = Request.Files;
                var requestId = Convert.ToInt32(Request.Form["requestId"]);
                foreach (string str in files)
                {
                    HttpPostedFileBase file = Request.Files[str] as HttpPostedFileBase;
                    //Checking file is available to save.  
                    if (file != null)
                    {
                        var currentmilse = DateTime.Now.Ticks;
                        var InputFileName = Path.GetFileNameWithoutExtension(file.FileName);
                        var InputFileExtention = Path.GetExtension(file.FileName);
                        var FullFileWithext = InputFileName + currentmilse + InputFileExtention;
                        var ServerSavePath = Path.Combine(Server.MapPath("~/Uploads/") + FullFileWithext);
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);
                        FileUploadModel fileUploadModel = trackerDAL.RmpoFileUploadToDatabase(requestId, file.FileName.ToString(), ServerSavePath,userID);
                        fileuploadList.Add(fileUploadModel);
                    }
                }
            }
            return Json(fileuploadList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ChangeDivView(int status)
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            string viewName = "";
            switch (status)
            {
                case 0:
                    viewName = "_createRequestPartial";
                    break;
                case 1:
                    viewName = "_addseasonPartialView";
                    break;
                case 3:
                    viewName = "_cordinatorDashboard";
                    break;
                case 4:
                    viewName = "_addFilesLog";
                    break;
                case 5:
                    viewName = "_ReportPartialView";
                    break;
            }
            return PartialView(viewName);
        }

        public ActionResult DataEntryCurrentProcess(int Process)
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            return Json(trackerDAL.DataEntryProgressList(Process),JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddNewStyleBySeason(int SeasonId,string StyleName)
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            return Json(trackerDAL.AddNewStyleBYSeason(SeasonId,StyleName), JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddNewSeasonByBuyer(int BuyerId, string SeasonName)
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            return Json(trackerDAL.AddNewSeasonByBuyer(BuyerId,SeasonName), JsonRequestBehavior.AllowGet);
        }

        public ActionResult InsertComments(int RequestId, string Comments)
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userid = Convert.ToInt32(Session["TrackerUserId"]);
            return Json(trackerDAL.InsertComments(RequestId,userid, Comments), JsonRequestBehavior.AllowGet);
        }
        public ActionResult ReadAllComments (int RequestId)
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userid = Convert.ToInt32(Session["TrackerUserId"]);
            return Json(trackerDAL.GetAllComments(RequestId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEstimatedTime(int OperationId, int Priority, int NoOfTransections)
        {
            if (Session["TrackerUserId"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userid = Convert.ToInt32(Session["TrackerUserId"]);
            return Json(trackerDAL.GetEstimatedTime(userid,OperationId,Priority,NoOfTransections), JsonRequestBehavior.AllowGet);
        }
    }
}