using System.Collections.Generic;

namespace ReleaseManagement.Framework.Data.Model.API
{
    public class ReleaseHeader
    {
        public ReleaseHeader()
        {
            Components = new List<ReleaseComponent>();
        }

        public string ReleaseName { get;set; }

        public List<ReleaseComponent> Components { get;set; }
    }
}
