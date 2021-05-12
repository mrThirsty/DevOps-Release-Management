using System.Threading.Tasks;
using ReleaseManagement.Framework.Data;
using ReleaseManagement.Framework.Data.Model;
using ReleaseManagement.Framework.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ReleaseManagement.Framework.Responses;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace ReleaseManagement.Framework.Services
{
    public class ComponentDataService : AuditableBaseDataService<Component>, IComponentDataService
    {
        public ComponentDataService(ReleaseDataContext context, IRMLogger logger, IHttpContextAccessor httpContext) : base(context, httpContext, logger)
        {
        }

        public override Task<IServiceResponse<IQueryable<Component>>> Get()
        {
            IServiceResponse<IQueryable<Component>> result = new ServiceResponse<IQueryable<Component>>();

            try
            {
                result.Result = Context.Components.Include(c => c.ComponentType);
            }
            catch(Exception ex)
            {
                result.OperationStatus = Enums.OperationResult.Error;
                result.Message = "Unable to get records of type Component";

                Logger.LogError("Get", ex, "Unable to get records of type Component");
            }

            return Task.FromResult(result);
        }

        public override Task<IServiceResponse<bool>> CanDelete(int id)
        {
            IServiceResponse<bool> result = new ServiceResponse<bool>();

            try
            {
                result.Result = Context.ComponentApprovals.Where(i => i.ComponentId == id).FirstOrDefault() == null;

                if(!result.Result)
                {
                    result.Message = "Unable to delete record as it is linked to component approvals, consider disabled it instead.";
                }
            }
            catch(Exception ex)
            {
                result.OperationStatus = Enums.OperationResult.Error;
                result.Message = $"Unable to verify if record can be deleted";

                Logger.LogError("CanDelete", ex, "Unable to verify if record can be deleted");
            }

            return Task.FromResult(result);
        }
    }
}
