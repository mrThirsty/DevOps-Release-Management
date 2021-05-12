using ReleaseManagement.Framework.Data;
using ReleaseManagement.Framework.Data.Model;
using ReleaseManagement.Framework.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using System;
using ReleaseManagement.Framework.Responses;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace ReleaseManagement.Framework.Services
{
    public class ComponentApprovalDataService : AuditableBaseDataService<ComponentApproval>, IComponentApprovalDataService
    {
        public ComponentApprovalDataService(ReleaseDataContext context, IRMLogger logger, IHttpContextAccessor httpContext) : base(context, httpContext, logger)
        {
        }

        public async Task<IServiceResponse<IQueryable<ComponentApproval>>> GetForRelease(int releaseId)
        {
            IServiceResponse<IQueryable<ComponentApproval>> response = new ServiceResponse<IQueryable<ComponentApproval>>();

            try
            {
                var existingApprovals = Context.ComponentApprovals.Where(i => i.ReleaseId.Equals(releaseId));
                var components = Context.Components.Where(i => i.Enabled).ToList();

                components.ForEach(c => {
                    ComponentApproval approval = existingApprovals.Where(i => i.ComponentId.Equals(c.Id)).FirstOrDefault();

                    if(approval == null)
                    {
                        approval = new ComponentApproval()
                        {
                            ComponentId = c.Id,
                            ReleaseId = releaseId
                        };

                        Context.ComponentApprovals.Add(approval);
                    }
                });

                await Context.SaveChangesAsync();

                response.Result = Context.ComponentApprovals.Where(i => i.ReleaseId.Equals(releaseId));
            }
            catch(Exception ex)
            {
                response.OperationStatus = Enums.OperationResult.Error;
                response.Message = "Unable to find approvals for release";

                Logger.LogError("GetForRelease", ex, $"Unable to find approvals for release for id {releaseId}");
            }

            return response;
        }

        public async Task<IServiceResponse> SetApproval(int approvalId, bool approved, string userId, string userName)
        {
            IServiceResponse response = new ServiceResponse();

            try
            {
                ComponentApproval record = Context.ComponentApprovals.Find(approvalId);

                if(record != null)
                {
                    record.Approved = approved;
                    record.ApprovedBy = userName;
                    record.ApprovalDate = DateTime.Now;
                    record.ApprovedById = userId;
                    await Context.SaveChangesAsync();
                }   
            }catch(Exception ex)
            {
                response.OperationStatus = Enums.OperationResult.Error;
                response.Message = "Unable to find set the approval for Component.";

                Logger.LogError("SetApproval", ex, $"Unable to find set the approval for Component, approvalId {approvalId}.");
            }

            return response;
        }

        public override Task<IServiceResponse<bool>> CanDelete(int id)
        {
            IServiceResponse<bool> result = new ServiceResponse<bool>();
            return Task.FromResult(result);
        }
    }
}
