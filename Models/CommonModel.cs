using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataEntry_Tracker.Models
{
    public class CommonModel
    {
        public int RequestId { get; set; }
        public int RequestCodId { get; set; }
        public int UnitId { get; set; }
        public int BaseOperationId { get; set; }
        public string UnitName { get; set; }
        public int BuyerId { get; set; }
        public string BuyerName { get; set; }
        public int SeasonId { get; set; }
        public string SeasonName { get; set; }
        public int StyleId { get; set; }
        public string StyleName { get; set; }
        public string Instruction { get; set; }
        public int RivisionNo { get; set; }
        public int Progress { get; set; }
        public float RequestTime { get; set; }
        public string BaseOperationName { get; set; }
        public string UserName { get; set; }
        public DateTime CreateDate { get; set; }
        public int CoordinatorId { get; set; }
        public string AssignTo { get; set; }
        public string CoUpdatedBy { get; set; }
        public string DataEntryCreated { get; set; }
        public string DataEntryUpdated { get; set; }
        public string WorkProgress { get; set; }
        public string WorkPercentage { get; set; }
        public string Priority { get; set; }
        public string CoordinatorName { get; set; }
        public string Aging { get; set; }
        public int NoOfTransections { get; set; }
        public List<FileUploadModel> fileUploadModelsList { get; set; }
        public List<CommonModel> RevisionList { get; set; }
        public List<CommentsTable> CommentsList { get; set; }

    }
}