using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ReleaseManagement.Framework.Data;
using ReleaseManagement.Framework.Data.Model;
using ReleaseManagement.Framework.Interfaces;
using ReleaseManagement.Framework.Responses;

namespace ReleaseManagement.Framework.Services
{
    public class AuditHeaderDataService : BaseLoggableDataService<AuditHeader>, IAuditHeaderDataService
    {
        public AuditHeaderDataService(ReleaseDataContext context, IRMLogger logger) : base(context, logger)
        {
        }

        public override Task<IServiceResponse<bool>> CanDelete(int id)
        {
            IServiceResponse<bool> result = new ServiceResponse<bool>();
            return Task.FromResult(result);
        }
    }
}
