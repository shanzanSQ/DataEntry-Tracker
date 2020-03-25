using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataEntry_Tracker.Models
{
    public class LoadPoCatagory
    {
        public int PoCatagoryId { get; set; }
        public string PoCatagoryName { get; set; }
        public int RequestID { get; set; }
        public int Status { get; set; }
    }
}