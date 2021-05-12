using System;
namespace ReleaseManagement.Framework.Data.Model.API
{
    public class ReleaseComponent
    {
        public ReleaseComponent()
        {
        }

        public string ComponentName { get;set; }
        public bool Approved { get;set; }
        public string DateApproved { get;set; }
        public string ApprovedBy { get;set; }
    }
}
