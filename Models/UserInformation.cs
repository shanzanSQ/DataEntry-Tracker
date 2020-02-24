using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataEntry_Tracker.Models
{
    public class UserInformation
    {
        public int UserInformationId { get; set; }
        public string UserInformationName { get; set; }
        public string UserInformationEmail { get; set; }
        public string UserInformationPassword { get; set; }
        public string UserInformationPhoneNumber { get; set; }
        public string UserSQNumber { get; set; }
        public int UserTypeId { get; set; }
        public int BusinessUnitId { get; set; }
        public int CreateBY { get; set; }
        public int DesignationId { get; set; }
        public int ApproverNo { get; set; }
        public String DesignationName { get; set; }
        public bool Empty
        {
            get
            {
                return (UserInformationId == 0 &&
                        string.IsNullOrWhiteSpace(UserInformationName) &&
                        string.IsNullOrWhiteSpace(UserInformationEmail));
            }
        }
    }
}