using DataEntry_Tracker.DataManager;
using DataEntry_Tracker.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DataEntry_Tracker.DAL
{
    public class TrackerDAL
    {
        private DataAccessManager accessManager = new DataAccessManager();

        public TrackerDAL()
        {
        }

        public List<CommonModel> GetUnitsDatabase(int userId)
        {
            List<CommonModel> commonModelList = new List<CommonModel>();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.DataEntryTracker);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@userId", userId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_LoadUNits", aParameters);
                while (dr.Read())
                {
                    CommonModel common = new CommonModel();
                    common.UnitId = (int)dr["UnitId"];
                    common.UnitName = dr["UnitName"].ToString();
                    commonModelList.Add(common);
                }

                return commonModelList;
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
        public List<CommonModel> GetAllBasOperation(int userId)
        {
            List<CommonModel> commonModelList = new List<CommonModel>();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.DataEntryTracker);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@userId", userId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_LoadStyleOperations", aParameters);
                while (dr.Read())
                {
                    CommonModel common = new CommonModel();
                    common.BaseOperationId = (int)dr["BaseOperationId"];
                    common.BaseOperationName = dr["BaseOperationName"].ToString();
                    commonModelList.Add(common);
                }

                return commonModelList;
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
        public List<CommonModel> GetBuyerFromDatabase(int userId)
        {
            List<CommonModel> commonModelList = new List<CommonModel>();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.DataEntryTracker);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@userId", userId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_LoadBuyer", aParameters);
                while (dr.Read())
                {
                    CommonModel common = new CommonModel();
                    common.BuyerId = (int)dr["BuyerId"];
                    common.BuyerName = dr["BuyerName"].ToString();
                    commonModelList.Add(common);
                }

                return commonModelList;
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
        public List<CommonModel> GetSeasonFromDatabas(int buyerId,int userId)
        {
            List<CommonModel> commonModelList = new List<CommonModel>();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.DataEntryTracker);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@userId", userId));
                aParameters.Add(new SqlParameter("@buyerId", buyerId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_LoadSeason", aParameters);
                while (dr.Read())
                {
                    CommonModel common = new CommonModel();
                    common.SeasonId = (int)dr["SeasonId"];
                    common.SeasonName = dr["SeasonName"].ToString();
                    commonModelList.Add(common);
                }

                return commonModelList;
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
        public List<CommonModel> GetStylesFromDatabase(int seasonId, int userId)
        {
            List<CommonModel> commonModelList = new List<CommonModel>();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.DataEntryTracker);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@userId", userId));
                aParameters.Add(new SqlParameter("@seasonId", seasonId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_LoadStyles", aParameters);
                while (dr.Read())
                {
                    CommonModel common = new CommonModel();
                    common.StyleId = (int)dr["StyleId"];
                    common.StyleName = dr["StyleName"].ToString();
                    commonModelList.Add(common);
                }

                return commonModelList;
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
        public List<FileUploadModel> GetFilesByRivisionRequest(int RequestId,int Status)
        {
            List<FileUploadModel> fileuploadlist = new List<FileUploadModel>();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.DataEntryTracker);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@requestId", RequestId));
                aParameters.Add(new SqlParameter("@Status", Status));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_getFilesByRivision", aParameters);
                while (dr.Read())
                {
                    FileUploadModel fileupload = new FileUploadModel();
                    fileupload.FileUploadId = (int)dr["UploadFiilesId"];
                    fileupload.FileUploadName = dr["UploadFilesName"].ToString();
                    fileupload.FileUploadPath = dr["UploadFilesPath"].ToString();
                    fileupload.OperationID = (int)dr["OperationId"];
                    fileupload.OperationName = dr["BaseOperationName"].ToString();
                    fileupload.RevisionNo = (int)dr["RivisionNo"];
                    fileuploadlist.Add(fileupload);
                }

                return fileuploadlist;
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public CommonModel SaveRequestReturnPrimarykey(CommonModel common,int userId)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.DataEntryTracker);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@UserId", userId));
                aParameters.Add(new SqlParameter("@BuyerId", common.BuyerId));
                aParameters.Add(new SqlParameter("@SessionId", common.SeasonId));
                aParameters.Add(new SqlParameter("@UnitId", common.UnitId));
                aParameters.Add(new SqlParameter("@StyleId", common.StyleId));
                aParameters.Add(new SqlParameter("@Instructions", common.Instruction));
                aParameters.Add(new SqlParameter("@Cordinator", common.CoordinatorId));
                
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_SaveMarchandiseRequestTable", aParameters);
                while (dr.Read())
                {
                    common.RequestId = (int)dr["RequestId"];
                    common.RivisionNo = (int)dr["RevisionNo"];
                }
                return common;
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public FileUploadModel FileUploadsToDataBase(int optionId, int requestId, string fileName, string filePath, string instructions,int userId,int RivisionNo)
        {
            FileUploadModel fileUploadModels = new FileUploadModel();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.DataEntryTracker);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@uploadFileName",fileName));
                aParameters.Add(new SqlParameter("@uploadFilepath", filePath));
                aParameters.Add(new SqlParameter("@instructions", instructions));
                aParameters.Add(new SqlParameter("@operationId", optionId));
                aParameters.Add(new SqlParameter("@requestId", requestId));
                aParameters.Add(new SqlParameter("@marchandId", userId));
                aParameters.Add(new SqlParameter("@RivisionNo", RivisionNo));

                SqlDataReader dr = accessManager.GetSqlDataReader("sp_FileUploadToDatabase", aParameters);
                while (dr.Read())
                {
                    fileUploadModels.FileUploadId= (int)dr["UploadFiilesId"];
                    fileUploadModels.FileUploadName= dr["UploadFilesName"].ToString();
                    fileUploadModels.FileUploadPath= dr["UploadFilesPath"].ToString();
                    fileUploadModels.Instructions= dr["Instructions"].ToString();
                    fileUploadModels.OperationName= dr["BaseOperationName"].ToString();
                }

                return fileUploadModels;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }


        public string DeleteFile(int fileUploadId,int UserID)
        {
            string filePath = "";
            try
            {
                accessManager.SqlConnectionOpen(DataBase.DataEntryTracker);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@fileId", fileUploadId));
                aParameters.Add(new SqlParameter("@userID", UserID));
                SqlDataReader dr= accessManager.GetSqlDataReader("sp_deleteFilesFromDatabase", aParameters);
                while(dr.Read()){
                    filePath = dr["UploadFilesPath"].ToString();
                }

                return filePath;
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public List<CommonModel> GetAllRequestById(int userID)
        {
            List<CommonModel> requestList = new List<CommonModel>();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.DataEntryTracker);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@userId", userID));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_AllRequestByUserId", aParameters);
                while (dr.Read())
                {
                    CommonModel requestmodal = new CommonModel();
                    requestmodal.RequestId= (int)dr["RequestId"];
                    requestmodal.BuyerName = dr["BuyerName"].ToString();
                    requestmodal.SeasonName = dr["SeasonName"].ToString();
                    requestmodal.UnitName = dr["UnitName"].ToString();
                    requestmodal.StyleName = dr["StyleName"].ToString();
                    requestmodal.RivisionNo = (int)dr["RevisionNo"];
                    requestmodal.CreateDate = (DateTime)dr["CreateDate"];
                    requestList.Add(requestmodal);
                    
                }

                return requestList;
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
        public List<CommonModel> GetMerchandRivisionProgress(int RequestId)
        {
            List<CommonModel> requestList = new List<CommonModel>();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.DataEntryTracker);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@RequestId", RequestId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_MerchandRevisionByRequest", aParameters);
                while (dr.Read())
                {
                    CommonModel requestmodal = new CommonModel();
                    requestmodal.RequestCodId = (int)dr["RequestCooRID"];
                    requestmodal.RivisionNo = (int)dr["RivisionNo"];
                    requestmodal.BaseOperationId = (int)dr["OperationId"];
                    requestmodal.RequestId = (int)dr["RequestId"];
                    requestmodal.BaseOperationName = dr["BaseOperationName"].ToString();
                    requestmodal.CreateDate =(DateTime) dr["CreateTime"];
                    requestmodal.AssignTo = dr["Progress"].ToString();
                    requestmodal.Priority = dr["Priority"].ToString();
                    requestmodal.Instruction = dr["Instructions"].ToString();
                    requestmodal.NoOfTransections = (int)dr["NoOfTransections"];
                    requestmodal.RequestTime = float.Parse(dr["RequestTime"].ToString());
                    requestList.Add(requestmodal);

                }

                return requestList;
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public CommonModel GetRequestInformationByReId(int RequestId)
        {
            CommonModel requestmodal = new CommonModel();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.DataEntryTracker);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@RequestId", RequestId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetRequestByRequestID", aParameters);
                while (dr.Read())
                {
                    requestmodal.RequestId = (int)dr["RequestId"];
                    requestmodal.BuyerName = dr["BuyerName"].ToString();
                    requestmodal.SeasonName = dr["SeasonName"].ToString();
                    requestmodal.UnitName = dr["UnitName"].ToString();
                    requestmodal.StyleName = dr["StyleName"].ToString();
                    requestmodal.RivisionNo = (int)dr["RevisionNo"];
                    requestmodal.CreateDate = (DateTime)dr["CreateDate"];
                }
               // requestmodal.fileUploadModelsList = new List<FileUploadModel>();
               // requestmodal.fileUploadModelsList = GetFilesByRivisionRequest(RequestId,0);
                return requestmodal;
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
        public bool SubmitRequestToTracker(int RequestId,int UserId,string Instruction, int OperationId, int Priority,int NoOfTransection)
        {
            bool result = false;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.DataEntryTracker);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@UserId", UserId));
                aParameters.Add(new SqlParameter("@RequestId", RequestId));
                aParameters.Add(new SqlParameter("@Instruction", Instruction));
                aParameters.Add(new SqlParameter("@OperationId", OperationId));
                aParameters.Add(new SqlParameter("@Priority", Priority));
                aParameters.Add(new SqlParameter("@NoOfTransections", NoOfTransection));
                result= accessManager.SaveData("sp_SubmitRequestToTracker", aParameters);
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
        public List<CommonModel> GetAllPendingRequest(int pendingNo, int userId,int dataEntryORco)
        {
            List<CommonModel> commonModelList = new List<CommonModel>();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.DataEntryTracker);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@userId", userId));
                aParameters.Add(new SqlParameter("@status", pendingNo));
                aParameters.Add(new SqlParameter("@dataOrCo", dataEntryORco));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_CoordinatorGetRequest", aParameters);
                while (dr.Read())
                {
                    CommonModel common = new CommonModel();
                    common.RequestId = (int)dr["RequestId"];
                    common.RequestCodId = (int)dr["RequestCooRID"];
                    common.RivisionNo = (int)dr["RivisionNo"];
                    common.BaseOperationId = (int)dr["OperationId"];
                    common.UnitName = dr["UnitName"].ToString();
                    common.BuyerName = dr["BuyerName"].ToString();
                    common.SeasonName = dr["SeasonName"].ToString();
                    common.StyleName = dr["StyleName"].ToString();
                    common.BaseOperationName = dr["BaseOperationName"].ToString();
                    common.UserName = dr["UserName"].ToString();
                    common.CreateDate = (DateTime)dr["CreateTime"];
                    common.Progress = (int)dr["ProgessId"];
                    common.Priority = dr["Priority"].ToString();
                    common.RequestTime = float.Parse(dr["RequestTime"].ToString());
                    commonModelList.Add(common);
                }

                return commonModelList;
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public List<UserInformation> GetAllTrackerUsers(int Module)
        {
            List<UserInformation> trackerUserList = new List<UserInformation>();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.DataEntryTracker);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@Modulekey", Module));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetAllUsesFromTracker",aParameters);
                while (dr.Read())
                {
                    UserInformation userInformation = new UserInformation();
                    userInformation.UserInformationId = (int)dr["UserId"];
                    userInformation.UserTypeId = (int)dr["Pending"];
                    userInformation.UserInformationName = dr["UserName"].ToString();
                    trackerUserList.Add(userInformation);
                }

                return trackerUserList;
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public bool SaveChatToDatabase(int sendTo,int userId,string message)
        {
            
            try
            {
                accessManager.SqlConnectionOpen(DataBase.DataEntryTracker);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@userId", userId));
                aParameters.Add(new SqlParameter("@Message", message));
                aParameters.Add(new SqlParameter("@SendTo", sendTo));
                bool result = accessManager.SaveData("sp_ChattingMessage",aParameters);
                return result;
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public List<MessageModel> GetAllChattingMessage(int sendTo,int userId)
        {
            List<MessageModel> messageModelList = new List<MessageModel>();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.DataEntryTracker);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@userId", userId));
                aParameters.Add(new SqlParameter("@SendTo", sendTo));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetChattingMessage", aParameters);
                while (dr.Read())
                {
                    MessageModel message = new MessageModel();
                    message.UserId = (int)dr["UserId"];
                    message.Message = dr["Message"].ToString();
                    message.CreateDate =(DateTime) dr["CreateTime"];
                    messageModelList.Add(message);
                }

                return messageModelList;
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public List<FileUploadModel> GetFilesByCoordinator(int RequestId,int OperationId,int RivisionId,int UserId,int ReqcodId)
        {
            List<FileUploadModel> fileuploadlist = new List<FileUploadModel>();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.DataEntryTracker);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@RecoId", ReqcodId));
                aParameters.Add(new SqlParameter("@RivisionId", RivisionId));
                aParameters.Add(new SqlParameter("@RequestId", RequestId));
                aParameters.Add(new SqlParameter("@OperationID", OperationId));
                aParameters.Add(new SqlParameter("@UserId", UserId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_CofilesByReCoId", aParameters);
                while (dr.Read())
                {
                    FileUploadModel fileupload = new FileUploadModel();
                    fileupload.FileUploadId = (int)dr["UploadFiilesId"];
                    fileupload.FileUploadName = dr["UploadFilesName"].ToString();
                    fileupload.FileUploadPath = dr["UploadFilesPath"].ToString();
                    fileupload.OperationID = (int)dr["OperationId"];
                    fileupload.OperationName = dr["BaseOperationName"].ToString();
                    fileupload.RevisionNo = (int)dr["RivisionNo"];
                    fileuploadlist.Add(fileupload);
                }

                return fileuploadlist;
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public bool AssignToDataEntryOperator(int RequestId,int UserId,int AssignTo)
        {
            bool result = false;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.DataEntryTracker);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@RequestId", RequestId));
                aParameters.Add(new SqlParameter("@CoordinatorId", UserId));
                aParameters.Add(new SqlParameter("@AssignTo", AssignTo));
                result = accessManager.SaveData("sp_AssignToDataEntryTracker", aParameters);
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public bool DataEntryUpdateTime(int RequestCoID, int UserId, int Progress)
        {
            bool result = false;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.DataEntryTracker);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@RequestCoId", RequestCoID));
                aParameters.Add(new SqlParameter("@Progress", Progress));
                aParameters.Add(new SqlParameter("@UserId", UserId));
                result = accessManager.SaveData("sp_DataEntryUpdateTime", aParameters);
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public bool CordinatorTimeUpdate(int RequestCoID)
        {
            bool result = false;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.DataEntryTracker);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@RequestCoId", RequestCoID));
                result = accessManager.UpdateData("sp_updateCodinatorView", aParameters);
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public bool AddNewStyleBYSeason(int seasonId,string styleName)
        {
            bool result = false;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.DataEntryTracker);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@SeasonId", seasonId));
                aParameters.Add(new SqlParameter("@NewStyleName", styleName));
                result = accessManager.SaveData("sp_AddNewStyleBySeason", aParameters);
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public bool AddNewSeasonByBuyer(int seasonId, string styleName)
        {
            bool result = false;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.DataEntryTracker);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@BuyerId", seasonId));
                aParameters.Add(new SqlParameter("@NewSeasonName", styleName));
                result = accessManager.SaveData("sp_AddnewSeasonByBuyer", aParameters);
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
        public int CheckDataEntryEngagged(int UserId)
        {
            int count= 0;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.DataEntryTracker);
                List<SqlParameter> aParameters = new List<SqlParameter>();;
                aParameters.Add(new SqlParameter("@UserId", UserId));
                SqlDataReader sqlDataReader = accessManager.GetSqlDataReader("sp_CheckDataEntryEngagged", aParameters);
                while (sqlDataReader.Read())
                {
                    count = (int)sqlDataReader["NumberCount"];
                }
                return count;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
        
        public List<CommonModel> DataEntryProgressList(int Process)
        {
            List<CommonModel> userlist = new List<CommonModel>();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.DataEntryTracker);
                List<SqlParameter> aParameters = new List<SqlParameter>(); ;
                aParameters.Add(new SqlParameter("@progress", Process));
                SqlDataReader sqlDataReader = accessManager.GetSqlDataReader("sp_DataEntryProgress", aParameters);
                while (sqlDataReader.Read())
                {
                    CommonModel common = new CommonModel();
                    common.AssignTo = sqlDataReader["UserName"].ToString();
                    common.RequestTime = float.Parse(sqlDataReader["RequestTime"].ToString());
                    common.DataEntryCreated = sqlDataReader["DataEntryStartTime"].ToString();
                    common.DataEntryUpdated = sqlDataReader["EstimatedEndTime"].ToString();
                    common.BaseOperationName = sqlDataReader["BaseOperationName"].ToString();
                    common.WorkProgress =sqlDataReader["WorkingTime"].ToString();
                    common.NoOfTransections =(int)sqlDataReader["NoOfTransections"];
                    //common.WorkProgress= (int)DateTime.Now.Subtract(Convert.ToDateTime(common.DataEntryCreated)).TotalMinutes;
                    common.WorkPercentage = (Convert.ToInt32((Convert.ToDouble(common.WorkProgress) * 100) / common.RequestTime)).ToString();
                    userlist.Add(common);
                }
                return userlist;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public CommonModel DetailsInformationRequest(int RequestId, int OperationId, int RivisionId, int UserId, int ReqcodId)
        {
            CommonModel modeldetails = new CommonModel();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.DataEntryTracker);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@RecoId", ReqcodId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetDetailsByRequestId", aParameters);
                while (dr.Read())
                {
                    
                    modeldetails.RequestCodId = (int)dr["RequestCooRID"];
                    modeldetails.CreateDate =(DateTime) dr["CreateTime"];
                    modeldetails.CoUpdatedBy = dr["CoUpdateTime"].ToString();
                    modeldetails.DataEntryCreated = dr["DataEntryStartTime"].ToString();
                    modeldetails.DataEntryUpdated = dr["DataEntryEndTime"].ToString();
                    modeldetails.UserName = dr["UserName"].ToString();
                    modeldetails.BaseOperationName = dr["BaseOperationName"].ToString();
                    modeldetails.Instruction = dr["Instructions"].ToString();
                    modeldetails.Progress = (int)dr["ProgessId"];
                    modeldetails.RequestId = (int)dr["RequestId"];
                    modeldetails.BaseOperationId = (int)dr["OperationId"];
                    modeldetails.RivisionNo = (int)dr["RivisionNo"];
                    
                }
                modeldetails.fileUploadModelsList = new List<FileUploadModel>();
                modeldetails.fileUploadModelsList = GetFilesByCoordinator(RequestId,OperationId,RivisionId,UserId,ReqcodId);
                return modeldetails;
                
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public List<CommentsTable> GetAllComments(int RequestId)
        {
            List<CommentsTable> requestList = new List<CommentsTable>();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.DataEntryTracker);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@RequestId", RequestId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetAllComments", aParameters);
                while (dr.Read())
                {
                    CommentsTable requestmodal = new CommentsTable();
                    requestmodal.UserName = dr["UserName"].ToString();
                    requestmodal.ReviewMessage = dr["Comments"].ToString();
                    requestmodal.CreateTime = dr["CreateTime"].ToString();
                    requestList.Add(requestmodal);
                }

                return requestList;
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
        public bool InsertComments(int RequestId,int UserId,string Comments)
        {
            bool result = false;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.DataEntryTracker);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@UserId", UserId));
                aParameters.Add(new SqlParameter("@Comments", Comments));
                aParameters.Add(new SqlParameter("@RequestId", RequestId));
                result = accessManager.SaveData("sp_InsertComments", aParameters);
                return result;
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
    }
    

}