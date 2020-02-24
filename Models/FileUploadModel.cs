using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataEntry_Tracker.Models
{
    public class FileUploadModel
    {
        public int FileUploadId { get; set; }
        public string FileUploadName { get; set; }
        public string FileUploadPath { get; set; }
        public string UserId { get; set; }
        public int OperationID { get; set; }
        public int RevisionNo { get; set; }
        public string OperationName { get; set; }
        public string Instructions { get; set; }
    }
}