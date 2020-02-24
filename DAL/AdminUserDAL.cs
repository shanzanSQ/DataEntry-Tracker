using DataEntry_Tracker.DataManager;
using DataEntry_Tracker.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

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
    }
}