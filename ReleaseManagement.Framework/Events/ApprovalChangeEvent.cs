using System;

namespace ReleaseManagement.Framework.Events
{
    public class ApprovalChangeEvent
    {
        public ApprovalChangeEvent()
        {}

        public int ApprovalId { get;set; }
        public bool Approved { get;set; }
        public DateTime DateChanged { get;set; }
        public string ApprovedBy { get;set; }
        public string ApprovedById { get;set; }
    }
}