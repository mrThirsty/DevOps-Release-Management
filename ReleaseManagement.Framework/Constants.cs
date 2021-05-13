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

        public static class SettingKeys
        {
            public const string AUTH_TYPE = "AUTH_TYPE";
            public const string AZURE_INSTANCE = "AZURE_INSTANCE";
            public const string AZURE_DOMAIN = "AZURE_DOMAIN";
            public const string AZURE_TENANTID = "AZURE_TENANTID";
            public const string AZURE_CLIENTID = "AZURE_CLIENTID";
            public const string AZURE_CALLBACKPATH = "AZURE_CALLBACKPATH";
            public const string AZURE_SIGNEDOUTCALLBACKPATH = "AZURE_SIGNEDOUTCALLBACKPATH";
            public const string AZURE_CLIENTSECRET = "AZURE_CLIENTSECRET";
            public const string AZURE_GROUPADMIN = "AZURE_GROUPADMIN";
            public const string AZURE_GROUPMEMBER = "AZURE_GROUPMEMBER";
        }
    }
}
