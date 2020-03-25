using DataEntry_Tracker.DataManager;
using DataEntry_Tracker.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DataEntry_Tracker.DAL
{
    public class SupplyChainDAL
    {
        private DataAccessManager accessManager = new DataAccessManager();
        public List<RequestSupplyChain> GetSupplyChainRequest(int userId,int Progress,int UserType){
            List<RequestSupplyChain> requestList = new List<RequestSupplyChain>();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.DataEntryTracker);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@userId", userId));
                aParameters.Add(new SqlParameter("@Progress", Progress));
                aParameters.Add(new SqlParameter("@UserType", UserType));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetAllSupplyChainRequest", aParameters);
                while (dr.Read())
                {
                    RequestSupplyChain request = new RequestSupplyChain();
                    request.SupplyChainId= (int)dr["SupplyChainId"];
                    request.CreateTime=dr["CreateTime"].ToString();
                    request.BuyerName = dr["BuyerName"].ToString();
                    request.PoNumber = dr["PONumber"].ToString();
                    request.StyleName = dr["StyleName"].ToString();
                    request.ProgressState = dr["Progress"].ToString();
                    request.Progressid = (int)dr["ProgressState"];
                    request.RequestBy = dr["RequestBy"].ToString();
                    request.OttDate =dr["OttDate"].ToString();
                    requestList.Add(request);

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
        public RequestSupplyChain GetDetailsRequestSupply(int UserId,int RequstId)
        {
            RequestSupplyChain resupply = new RequestSupplyChain();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.DataEntryTracker);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@SupplyChainId", RequstId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_MerchandDetailsSupply", aParameters);
                while (dr.Read())
                {
                    resupply.SupplyChainId = (int)dr["SupplyChainId"];
                    resupply.CreateTime = dr["CreateTime"].ToString();
                    resupply.AssignTime= dr["AssignTime"].ToString();
                    resupply.StartTime = dr["StartTime"].ToString();
                    resupply.EndTime = dr["EndTime"].ToString();
                    resupply.OttDate = dr["OttDate"].ToString();
                    resupply.PoCatagoryName = dr["PoCatagoryName"].ToString();
                    resupply.CoordinatorName = dr["CoordinatorName"].ToString();
                    resupply.SpUserName = dr["DataEntryName"].ToString();
                    resupply.Remarks = dr["Remarks"].ToString();
                    resupply.Progressid = (int)dr["ProgressState"];
                    resupply.PoNumber = dr["PONumber"].ToString();
                }
                resupply.fileUploadModel = new List<FileUploadModel>();
                resupply.fileUploadModel = GetFilesByRivisionRequest(RequstId);
                return resupply;
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

        public List<FileUploadModel> GetFilesByRivisionRequest(int SupplyRequestId)
        {
            List<FileUploadModel> fileuploadlist = new List<FileUploadModel>();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.DataEntryTracker);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@SupplyId", SupplyRequestId)); 
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetFilesFromSupplyChain", aParameters);
                while (dr.Read())
                {
                    FileUploadModel fileupload = new FileUploadModel();
                    fileupload.FileUploadId = (int)dr["RMPOFileId"];
                    fileupload.FileUploadName = dr["RMPOFileName"].ToString();
                    fileupload.FileUploadPath = dr["UploadFilesPath"].ToString();
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

        public bool AssignToUsers(int SupplyChainID, int UserId, int AssignTo)
        {
            bool result = false;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.DataEntryTracker);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@SupplyRequestId", SupplyChainID));
                aParameters.Add(new SqlParameter("@CoordinatorId", UserId));
                aParameters.Add(new SqlParameter("@AssignTo", AssignTo));
                result = accessManager.SaveData("sp_AssignToSupplyUsers", aParameters);
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

        public bool SupplyChainUpdateTime(int SupplyChainId, int UserId, int Progress)
        {
            bool result = false;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.DataEntryTracker);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@SupplyChainId", SupplyChainId));
                aParameters.Add(new SqlParameter("@ProgressState", Progress));
                aParameters.Add(new SqlParameter("@UserId", UserId));
                result = accessManager.SaveData("sp_UpdateSupplyChainState", aParameters);
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

        public bool SavePoToRequest(RequestSupplyChain requestSupply,int userId)
        {
            bool result = false;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.DataEntryTracker);
                foreach (LoadPoCatagory loadPocatagory in requestSupply.LoadPoCatagories)
                {
                    List<SqlParameter> aprap = new List<SqlParameter>();
                    aprap.Add(new SqlParameter("@RequestId", requestSupply.RequestID));
                    aprap.Add(new SqlParameter("@PoName", loadPocatagory.PoCatagoryName));
                    aprap.Add(new SqlParameter("@UserId", userId));
                    result = accessManager.SaveData("sp_SavePoToRequest", aprap);
                }

                return result;
            }
            catch (Exception exception)
            {
                accessManager.SqlConnectionClose(true);
                throw exception;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public List<LoadPoCatagory> LoadPoByRequest(int RequestID)
        {
            List<LoadPoCatagory> poCatagories = new List<LoadPoCatagory>();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.DataEntryTracker);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@RequestId", RequestID));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_LoadPOBYRequestId", aParameters);
                while (dr.Read())
                {
                    LoadPoCatagory poCatagory = new LoadPoCatagory();
                    poCatagory.PoCatagoryId = (int)dr["RequestPOTableId"];
                    poCatagory.PoCatagoryName = dr["PONumber"].ToString();
                    poCatagories.Add(poCatagory);
                }

                return poCatagories;
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