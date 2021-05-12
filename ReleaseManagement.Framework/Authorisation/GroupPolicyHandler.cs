using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using ReleaseManagement.Framework.Graph;
using System.Threading.Tasks;

namespace ReleaseManagement.Framework.Authorisation
{
    public class GroupPolicyHandler : AuthorizationHandler<GroupPolicyRequirement>
    {
        private IHttpContextAccessor _httpContextAccessor;

        public GroupPolicyHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Makes a decision if authorization is allowed based on GroupPolicyRequirement.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                   GroupPolicyRequirement requirement)
        {
            bool hasGroup = false;

            requirement.GroupNames.ForEach(group => {
                if (GraphHelper.CheckUsersGroupMembership(context, group, _httpContextAccessor))
                { 
                    hasGroup = true;
                }
            });

            if(hasGroup) context.Succeed(requirement);

            // Calls method to check if requirement exists in user claims or session.
            
            return Task.CompletedTask;
        }
    }
}
