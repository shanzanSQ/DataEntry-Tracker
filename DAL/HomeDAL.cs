
using DataEntry_Tracker.DataManager;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;

namespace DataEntry_Tracker.Models.DAL
{
    public class HomeDAL
    {
        private DataAccessManager accessManager = new DataAccessManager();
        public UserInformation CheckUserLogin(string UserEmail, string UserPassword)
        {
            UserInformation user = new UserInformation();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@userName", UserEmail));
                aParameters.Add(new SqlParameter("@userPassword", PasswordManager.Encrypt(UserPassword)));
                SqlDataReader dr= accessManager.GetSqlDataReader("sp_CheckUserLogin", aParameters);
                while (dr.Read())
                {
                    user.UserInformationId = (int)dr["UserId"];
                    user.UserInformationName = dr["UserName"].ToString();
                    user.UserInformationEmail = dr["UserEmail"].ToString();
                    user.DesignationId = (int)dr["DesignationID"];
                    //user.UserInformationPhoneNumber = (int)dr["UserPhone"];
                }

                return user;
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }


        public UserInformation GetUserInformation(int userId)
        {
            UserInformation user = new UserInformation();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@userId", userId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetUserInformation", aParameters);
                while (dr.Read())
                {
                    user.UserInformationName = dr["UserName"].ToString();
                    user.UserInformationEmail = dr["UserEmail"].ToString();
                    user.UserInformationPhoneNumber = dr["UserPhone"].ToString();
                    user.UserSQNumber = dr["SqIDNumber"].ToString();
                    user.DesignationName = dr["DesignationName"].ToString();
                }

                return user;
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
        public bool changePassword(int userId,string newpass)
        {
            bool success = false;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@userId", userId));
                aParameters.Add(new SqlParameter("@newPass", PasswordManager.Encrypt(newpass)));
                success = accessManager.UpdateData("sp_changePassword", aParameters);
                return success;
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

        public bool RecoveryPassword(string semail)
        {
            String password = "", name = "", email = "";
            bool success = false;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@userEmail", semail));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_ForgotPassowrd", aParameters);
                while (dr.Read())
                {
                    password = dr["UserPassword"].ToString();
                    name = dr["UserName"].ToString();
                    email = dr["UserEmail"].ToString();
                }
                if (name != "" && email != "")
                {
                    try
                    {
                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();
                        message.From = new MailAddress("noreply.mail1@sqgc.com");
                        message.To.Add(new MailAddress(email));
                        message.Subject = "Password Recovery";
                        message.IsBodyHtml = true; //to make message body as html  
                        message.Body = "Dear Mr." + name + "<br/> You requested for Recover your password <br/> Your Password for the Approval management system is : " + PasswordManager.Decrypt(password) + " <br/>" +
                            "Thank you For Being with Us <br/>" +
                            "<br/>Thank You<br/> <a href='http://10.12.13.163:8080/'>Data Entry Service Tracker</a><br/><br/>sqgc.com";
                        smtp.Port = 587;
                        smtp.Host = "smtp.office365.com"; //for gmail host  
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential("noreply.mail1@sqgc.com", "Clothing@45678");
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Send(message);
                        success = true;

                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
                return success;
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
        public bool CreateDesignation(string designationName)
        {
            bool success = false;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@desinationName", designationName));
                success = accessManager.SaveData("sp_CreateDesignation", aParameters);
                return success;
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
        public List<ModuleClassModel> GetModuleByuUser(int userId)
        {
            List<ModuleClassModel> module = new List<ModuleClassModel>();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.DataEntryTracker);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@userId", userId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_ModulePermissionData", aParameters);
                while (dr.Read())
                {
                    ModuleClassModel moduleClass = new ModuleClassModel();
                    moduleClass.ModuleKey=(int)dr["Modulekey"];
                    moduleClass.ModuleName=dr["ModuleName"].ToString();
                    moduleClass.ModuleValue=dr["ModuleValue"].ToString();
                    moduleClass.ModuleController=dr["ModuleController"].ToString();
                    module.Add(moduleClass);
                }

                return module;
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