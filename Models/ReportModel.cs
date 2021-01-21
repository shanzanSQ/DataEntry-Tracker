using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataEntry_Tracker.Models
{
    public class ReportModel
    {
        public string RequestId { get; set; }
        public string UnitName { get; set; }
        public string BuyerName { get; set; }
        public string RequestBY { get; set; }
        public string CoOrdinateBy { get; set; }
        public string AssignTo { get; set; }
        public string BaseOperationName { get; set; }
        public string NoOfTransections { get; set; }
        public string RequestTime { get; set; }
        public string TotalTime { get; set; }
        public string Return { get; set; }
        public string RequestedDate { get; set; }
        public string DataEntryStartTime { get; set; }
        public string DataEntryEndTime { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        
    }
}