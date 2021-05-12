using System.Collections.Generic;
using ReleaseManagement.Framework.Data.Model;

namespace ReleaseManagement.Framework.Interfaces
{
    public interface IAuditableDataService<T> : IDataService<T>
    {
        List<AuditHeader> AuditChanges();
    }
}
