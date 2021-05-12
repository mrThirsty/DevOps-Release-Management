using ReleaseManagement.Framework.Data.Model;
using System.Linq;
using System.Threading.Tasks;

namespace ReleaseManagement.Framework.Interfaces
{
    public interface IComponentApprovalDataService : IAuditableDataService<ComponentApproval>
    {
        Task<IServiceResponse<IQueryable<ComponentApproval>>> GetForRelease(int releaseId);
        Task<IServiceResponse> SetApproval(int approvalId, bool approved, string userId, string userName);
    } 
}
