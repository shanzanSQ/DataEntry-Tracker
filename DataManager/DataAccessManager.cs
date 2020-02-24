using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DataEntry_Tracker.DataManager
{
    public class DataAccessManager
    {
        private SqlConnection sqlConnection = null;
        private SqlCommand sqlCommand = null;
        private SqlDataReader sqlDataReader = null;
        private SqlTransaction sqlTransaction = null;
        private SqlDataAdapter sqlDataAdapter = null;
        private const int CommandTimeout = 120000;
        private bool isException;
        private bool returnValue = true;
        private string ConnectionString(string database)
        {
            return @"data source=" + SqlUserAccess.DataSource + ";Initial Catalog=" + database +
                   ";Integrated Security=false; User Id=" +
                   SqlUserAccess.UserName + "; password=" + SqlUserAccess.PassWord + ";";
        }
        public bool SqlConnectionOpen(string database)
        {
            try
            {
                sqlConnection = new SqlConnection(ConnectionString(database));

                if (sqlConnection.State != ConnectionState.Open)
                {
                    sqlConnection.Open();
                    sqlTransaction = sqlConnection.BeginTransaction();

                }
            }
            catch (SqlException sqlException)
            {
                isException = true;
                throw sqlException;
            }
            finally
            {
                if (isException)
                {
                    returnValue = false;
                }

            }
            return returnValue;
        }
        public bool SqlConnectionClose(bool IsRollBack = false)
        {
            try
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    if (sqlTransaction != null)
                    {
                        if (sqlDataReader != null)
                        {
                            sqlDataReader.Close();

                        }
                        if (IsRollBack)
                        {
                            sqlTransaction.Rollback();
                        }
                        else
                        {
                            sqlTransaction.Commit();
                        }
                        sqlTransaction.Dispose();
                    }
                    sqlCommand.Dispose();
                    sqlConnection.Close();
                }

            }
            catch (SqlException sqlException)
            {
                isException = true;
                throw sqlException;
            }
            finally
            {
                if (isException)
                {
                    returnValue = false;
                }

            }

            return returnValue;
        }
        public SqlDataReader GetSqlDataReader(string StoreProcedure, bool IsBigData = false)
        {
            try
            {
                sqlCommand = new SqlCommand
                {
                    Connection = sqlConnection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = StoreProcedure,
                    Transaction = sqlTransaction
                };
                if (IsBigData)
                {
                    sqlCommand.CommandTimeout = CommandTimeout;
                }

                sqlDataReader = sqlCommand.ExecuteReader();
            }
            catch (SqlException sqlException)
            {
                isException = true;
                sqlDataReader = null;
                throw sqlException;
            }
            finally
            {
                if (isException)
                {
                    SqlConnectionClose(true);
                }

            }
            return sqlDataReader;
        }
        public SqlDataReader GetSqlDataReader(string StoreProcedure, List<SqlParameter> SqlParameterList, bool IsBigData = false)
        {
            try
            {
                sqlCommand = new SqlCommand
                {
                    Connection = sqlConnection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = StoreProcedure,
                    Transaction = sqlTransaction
                };

                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddRange(SqlParameterList.ToArray());
                if (IsBigData)
                {
                    sqlCommand.CommandTimeout = CommandTimeout;
                }



                sqlDataReader = sqlCommand.ExecuteReader();
                sqlCommand.Parameters.Clear();
            }
            catch (SqlException sqlException)
            {
                isException = true;
                sqlDataReader = null;
                throw sqlException;
            }
            finally
            {
                sqlCommand.Parameters.Clear();
                sqlCommand.Dispose();
                if (isException)
                {
                    SqlConnectionClose(true);
                }

            }
            return sqlDataReader;
        }
        public DataTable GetDataTable(string StoreProcedure, bool IsBigData = false)
        {
            DataTable dt = new DataTable();

            try
            {
                DataSet ds = new DataSet();
                sqlCommand = new SqlCommand
                {
                    Connection = sqlConnection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = StoreProcedure,
                    Transaction = sqlTransaction
                };

                if (IsBigData)
                {
                    sqlCommand.CommandTimeout = CommandTimeout;
                }
                sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(ds);
            }
            catch (SqlException sqlException)
            {
                isException = true;
                dt = null;
                throw sqlException;
            }
            finally
            {
                if (isException)
                {
                    SqlConnectionClose(true);
                }

            }
            return dt;
        }
        public DataTable GetDataTable(string StoreProcedure, List<SqlParameter> SqlParameterList, bool IsBigData = false)
        {
            DataTable dt = new DataTable();

            try
            {
                DataSet ds = new DataSet();
                sqlCommand = new SqlCommand
                {
                    Connection = sqlConnection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = StoreProcedure,
                    Transaction = sqlTransaction
                };

                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddRange(SqlParameterList.ToArray());
                if (IsBigData)
                {
                    sqlCommand.CommandTimeout = CommandTimeout;
                }
                sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(ds);
                dt = ds.Tables[0];

            }
            catch (SqlException sqlException)
            {
                isException = true;
                dt = null;
                throw sqlException;
            }
            finally
            {
                sqlCommand.Parameters.Clear();
                if (isException)
                {
                    SqlConnectionClose(true);
                }
            }
            return dt;
        }
        public DataSet GetDataSet(string StoreProcedure, bool IsBigData = false)
        {
            DataSet ds = new DataSet();
            try
            {

                sqlCommand = new SqlCommand
                {
                    Connection = sqlConnection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = StoreProcedure,
                    Transaction = sqlTransaction
                };

                if (IsBigData)
                {
                    sqlCommand.CommandTimeout = CommandTimeout;
                }
                sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(ds);
            }
            catch (SqlException sqlException)
            {
                isException = true;
                ds = null;
                throw sqlException;
            }
            finally
            {
                if (isException)
                {
                    SqlConnectionClose(true);
                }

            }
            return ds;
        }
        public DataSet GetDataSet(string StoreProcedure, List<SqlParameter> SqlParameterList, bool IsBigData = false)
        {
            DataSet ds = new DataSet();
            try
            {

                sqlCommand = new SqlCommand
                {
                    Connection = sqlConnection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = StoreProcedure,
                    Transaction = sqlTransaction
                };

                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddRange(SqlParameterList.ToArray());
                if (IsBigData)
                {
                    sqlCommand.CommandTimeout = CommandTimeout;
                }
                sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(ds);

            }
            catch (SqlException sqlException)
            {
                isException = true;
                ds = null;
                throw sqlException;
            }
            finally
            {
                sqlCommand.Parameters.Clear();
                if (isException)
                {
                    SqlConnectionClose(true);
                }

            }
            return ds;
        }
        private int ExecuteNonQueryData(string StoreProcedure, List<SqlParameter> SqlParameterList, bool IsPrimaryKey = true, bool IsBigData = false)
        {
            int primaryKey = 0;
            try
            {
                sqlCommand = new SqlCommand();

                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = StoreProcedure;
                sqlCommand.Transaction = sqlTransaction;

                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddRange(SqlParameterList.ToArray());
                if (IsBigData)
                {
                    sqlCommand.CommandTimeout = CommandTimeout;
                }
                primaryKey = Convert.ToInt32(sqlCommand.ExecuteScalar());

            }
            catch (SqlException sqlException)
            {
                returnValue = false;
                isException = true;
                throw sqlException;
            }
            finally
            {
                sqlCommand.Parameters.Clear();
                if (isException)
                {
                    SqlConnectionClose(true);
                }
            }
            return primaryKey;
        }
        /// <summary>
        /// After Saving Data Only Identity Value will be Returned
        /// </summary>
        /// <param name="StoreProcedure"></param>
        /// <param name="SqlParameterList"></param>
        /// <returns></returns>
        public int SaveDataReturnPrimaryKey(string StoreProcedure, List<SqlParameter> SqlParameterList, bool IsBigData = false)
        {
            int primaryKey = 0;
            try
            {
                primaryKey = ExecuteNonQueryData(StoreProcedure, SqlParameterList, true, IsBigData);
            }
            catch (SqlException sqlException)
            {
                throw sqlException;
            }
            return primaryKey;
        }
        private bool ExecuteNonQueryData(string StoreProcedure, List<SqlParameter> SqlParameterList, bool IsBigData = false)
        {
            try
            {
                sqlCommand = new SqlCommand
                {
                    Connection = sqlConnection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = StoreProcedure,
                    Transaction = sqlTransaction
                };
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddRange(SqlParameterList.ToArray());
                if (IsBigData)
                {
                    sqlCommand.CommandTimeout = CommandTimeout;
                }
                sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException sqlException)
            {
                returnValue = false;
                isException = true;
                throw sqlException;
            }
            finally
            {
                sqlCommand.Parameters.Clear();
                if (isException)
                {
                    SqlConnectionClose(true);
                }
            }

            return returnValue;
        }
        /// <summary>
        /// After Saving Data a Boolean will be Returned
        /// </summary>
        /// <param name="StoreProcedure"></param>
        /// <param name="SqlParameterList"></param>
        /// <returns></returns>
        public bool SaveData(string StoreProcedure, List<SqlParameter> SqlParameterList, bool IsBigData = false)
        {
            try
            {
                returnValue = ExecuteNonQueryData(StoreProcedure, SqlParameterList, IsBigData);
            }
            catch (SqlException sqlException)
            {
                returnValue = false;
                throw sqlException;
            }
            return returnValue;
        }
        /// <summary>
        /// After Updating Data a Boolean will be Returned
        /// </summary>
        /// <param name="StoreProcedure"></param>
        /// <param name="SqlParameterList"></param>
        /// <returns></returns>
        public bool UpdateData(string StoreProcedure, List<SqlParameter> SqlParameterList, bool IsBigData = false)
        {
            try
            {
                returnValue = ExecuteNonQueryData(StoreProcedure, SqlParameterList, IsBigData);
            }
            catch (SqlException sqlException)
            {
                returnValue = false;
                throw sqlException;
            }
            return returnValue;
        }
        /// <summary>
        /// After Deleting Data a Boolean will be Returned
        /// </summary>
        /// <param name="StoreProcedure"></param>
        /// <param name="SqlParameterList"></param>
        /// <returns></returns>
        public bool DeleteData(string StoreProcedure, List<SqlParameter> SqlParameterList, bool IsBigData = false)
        {
            try
            {
                returnValue = ExecuteNonQueryData(StoreProcedure, SqlParameterList, IsBigData);
            }
            catch (SqlException sqlException)
            {
                returnValue = false;
                throw sqlException;
            }
            return returnValue;
        }
    }
}