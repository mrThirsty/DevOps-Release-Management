using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReleaseManagement.Framework
{
    public static class ReleaseConstants
    {
        public static class Security
        {
            public const string ScopeUserRead = "User.Read";

            public const string ScopeGroupMemberRead = "GroupMember.Read.All";

            public const string BearerAuthorizationScheme = "Bearer";

            public static class Claims
            {
                public const string Name = "name";
                public const string UserId = "http://schemas.microsoft.com/identity/claims/objectidentifier";
            }
        }
    }
}
