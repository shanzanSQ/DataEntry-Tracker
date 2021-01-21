using DataEntry_Tracker.DataManager;
using DataEntry_Tracker.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataEntry_Tracker.DAL
{
    public class AdminUserDAL
    {
        private DataAccessManager accessManager = new DataAccessManager();

        public AdminUserDAL()
        {
        }
        public List<CommonModel> AllMasterRequest(int userID)
        {
            List<CommonModel> requestList = new List<CommonModel>();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.DataEntryTracker);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@userId", userID));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_AdimnAllRequest", aParameters);
                while (dr.Read())
                {
                    CommonModel requestmodal = new CommonModel();
                    requestmodal.RequestId = (int)dr["RequestId"];
                    requestmodal.BuyerName = dr["BuyerName"].ToString();
                    requestmodal.SeasonName = dr["SeasonName"].ToString();
                    requestmodal.UnitName = dr["UnitName"].ToString();
                    requestmodal.StyleName = dr["StyleName"].ToString();
                    requestmodal.UserName = dr["UserName"].ToString();
                    requestmodal.RivisionNo = (int)dr["NoTask"];
                    requestmodal.Progress = (int)dr["Pending"];
                    requestmodal.CoordinatorName = dr["CoordinatorName"].ToString();
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

        public List<DashboardModule> CoordinatorDashboard(int userId)
        {
            List<DashboardModule> userlist = new List<DashboardModule>();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.DataEntryTracker);
                List<SqlParameter> aParameters = new List<SqlParameter>(); ;
                aParameters.Add(new SqlParameter("@UserId", userId));
                SqlDataReader sqlDataReader = accessManager.GetSqlDataReader("sp_CoordinatorDashboard", aParameters);
                while (sqlDataReader.Read())
                {
                    DashboardModule common = new DashboardModule();
                    common.UserName = sqlDataReader["UserName"].ToString();
                    common.TotalTodaysCompleted =sqlDataReader["TotalTodaysCompleted"].ToString();
                    common.CurrentWorkload = sqlDataReader["CurrentWorkload"].ToString();
                    common.TodayAssign = sqlDataReader["TodayAssign"].ToString();
                    common.WorkingTime = sqlDataReader["WorkingTime"].ToString();
                    //common.WorkPercentage =(Int32.Parse(common.TotalTodaysCompleted)+ Int32.Parse(common.WorkingTime))*100/ Int32.Parse(common.CurrentWorkload);
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

        public List<CommonModel> GetAdminPending(int userId, int status)
        {
            List<CommonModel> commonModelList = new List<CommonModel>();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.DataEntryTracker);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@userId", userId));
                aParameters.Add(new SqlParameter("@status", status));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_PendingToCoordinatorShow", aParameters);
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
                    common.AssignTo = dr["Progress"].ToString();
                    common.CoordinatorName = dr["AssignTo"].ToString();
                    common.NoOfTransections = (int)dr["NoOfTransections"];
                    common.RequestTime = float.Parse(dr["RequestTime"].ToString());
                    TimeSpan timespan = TimeSpan.FromMinutes(Convert.ToDouble(dr["Aging"].ToString()));
                    common.Aging =timespan.ToString("%d")+"days "+timespan.ToString(@"hh\:mm"); 
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
        public List<ReportModel> GetDataEntryReport(string FromDate, string ToDate)
        {
            List<ReportModel> reportmodellist = new List<ReportModel>();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.DataEntryTracker);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@FromDate", FromDate));
                aParameters.Add(new SqlParameter("@ToDate", ToDate));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_ReportDataEntryTracker", aParameters);
                while (dr.Read())
                {
                    ReportModel reportmodel = new ReportModel();
                    reportmodel.RequestId = dr["RequestCooRID"].ToString();
                    reportmodel.UnitName = dr["UnitName"].ToString();
                    reportmodel.BuyerName = dr["BuyerName"].ToString();
                    reportmodel.RequestBY = dr["RequestBY"].ToString();
                    reportmodel.CoOrdinateBy = dr["CoOrdinateBy"].ToString();
                    reportmodel.AssignTo = dr["AssignTo"].ToString();
                    reportmodel.BaseOperationName = dr["BaseOperationName"].ToString();
                    reportmodel.NoOfTransections = dr["NoOfTransections"].ToString();
                    reportmodel.RequestTime = dr["RequestTime"].ToString();
                    reportmodel.TotalTime = dr["TotalTime"].ToString();
                    reportmodel.Return = dr["Return"].ToString();
                    reportmodel.RequestedDate= dr["RequestedDate"].ToString();
                    reportmodel.DataEntryStartTime= dr["DataEntryStartTime"].ToString();
                    reportmodel.DataEntryEndTime = dr["DataEntryEndTime"].ToString();
                    reportmodel.Priority = dr["Priority"].ToString();
                    reportmodel.Status = dr["Status"].ToString();
                    reportmodellist.Add(reportmodel);
                }
                return reportmodellist;
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