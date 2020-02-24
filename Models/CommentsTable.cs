using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataEntry_Tracker.Models
{
    public class CommentsTable
    {
        public int QueryId { get; set; }
        public int RequestCoId { get; set; }
        public int UserId { get; set; }
        public string ReviewMessage { get; set; }
        public string UserName { get; set; }
        public string CreateTime { get; set; }
    }
}