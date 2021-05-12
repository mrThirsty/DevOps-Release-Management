using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ReleaseManagement.Framework.Data;
using ReleaseManagement.Framework.Data.Model;
using ReleaseManagement.Framework.Interfaces;
using ReleaseManagement.Framework.Responses;

namespace ReleaseManagement.Framework.Services
{
    public class ReleaseDataService : AuditableBaseDataService<Release>, IReleaseDataService
    {
        public ReleaseDataService(ReleaseDataContext context, IHttpContextAccessor httpContext, IRMLogger logger) : base(context, httpContext, logger)
        {
        }

        public override Task<IServiceResponse<bool>> CanDelete(int id)
        {
            IServiceResponse<bool> result = new ServiceResponse<bool>();
            return Task.FromResult(result);
        }
    }
}
