using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace ReleaseManagement.Framework.Authorisation
{
    public class GroupPolicyRequirement : IAuthorizationRequirement
    {
        public List<string> GroupNames { get; }
        public GroupPolicyRequirement(params string[] names)
        {
            GroupNames = new List<string>();
            GroupNames.AddRange(names);
        }
    }
}
