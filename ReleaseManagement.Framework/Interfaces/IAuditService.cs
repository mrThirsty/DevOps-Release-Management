using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using ReleaseManagement.Framework.Data.Model;

namespace ReleaseManagement.Framework.Interfaces
{
    public interface IAuditService
    {
        Task<IIncludableQueryable<AuditHeader, ICollection<AuditItem>>> GetAuditDetails(string recordType, int recordId);
        Task<bool> DeleteAuditRecords(string recordType, int recordId);
    }
}
