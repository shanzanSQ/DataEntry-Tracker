using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataEntry_Tracker.Models
{
    public class DashboardModule
    {
        public string UserName { get; set; }
        public string TotalTodaysCompleted { get; set; }
        public string CurrentWorkload { get; set; }
        public string TodayAssign { get; set; }
        public string RequestTime { get; set; }
        public string WorkingTime { get; set; }
        public int WorkPercentage { get; set; }
    }
}