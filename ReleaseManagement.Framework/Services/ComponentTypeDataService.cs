using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ReleaseManagement.Framework.Data;
using ReleaseManagement.Framework.Data.Model;
using ReleaseManagement.Framework.Interfaces;
using ReleaseManagement.Framework.Responses;

namespace ReleaseManagement.Framework.Services
{
    public class ComponentTypeDataService : AuditableBaseDataService<ComponentType>, IComponentTypeDataService
    {
        public ComponentTypeDataService(ReleaseDataContext context, IRMLogger logger, IHttpContextAccessor httpContext): base(context, httpContext, logger)
        {
        }

        public override Task<IServiceResponse<bool>> CanDelete(int id)
        {
            IServiceResponse<bool> result = new ServiceResponse<bool>();

            try
            {
                result.Result = Context.Components.Where(i => i.ComponentTypeId == id).FirstOrDefault() == null;

                if(!result.Result)
                {
                    result.Message = "Record can't be deleted as it has Components linked to it.";
                }
            }
            catch(Exception ex)
            {
                result.OperationStatus = Enums.OperationResult.Error;
                result.Message = "Unable to verify if record can be deleted";

                Logger.LogError("Can Delete", ex, "Unable to verify if record can be deleted");
            }

            return Task.FromResult(result);
        }
    }
}
