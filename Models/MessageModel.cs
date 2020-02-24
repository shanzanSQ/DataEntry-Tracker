using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataEntry_Tracker.Models
{
    public class MessageModel
    {
        public int UserId { get; set; }
        public string Message { get; set; }
        public DateTime CreateDate { get; set; }
    }
}