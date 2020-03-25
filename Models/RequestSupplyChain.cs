
using System;
using System.Collections.Generic;

namespace DataEntry_Tracker.Models
{
    public class RequestSupplyChain
    {
        public int SupplyChainId { get; set; }
        public int RequestID { get; set; }

        public string PoCatagoryName { get; set; }
        public string PoNumber { get; set; }
        public string RequestBy { get; set; }
        public string BuyerName { get; set; }
        public string StyleName { get; set; }
        public string CoordinatorName { get; set; }
        public string SpUserName { get; set; }

        public int SupplyCordinateId { get; set; }
        public string OttDate { get; set; }
        public string ProgressState { get; set; }
        public int AssignTo { get; set; }
        public string AssignTime { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string CreateTime { get; set; }
        public string UpdateTime { get; set; }
        public int TotalTime { get; set; }
        public string Remarks { get; set; }
        public int PoCatagoryId { get; set; }
        public int Progressid { get; set; }

        public List<FileUploadModel> fileUploadModel { get; set; }
        public List<LoadPoCatagory> LoadPoCatagories { get; set; }
    }
}