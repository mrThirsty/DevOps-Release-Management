using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using Microsoft.Identity.Web;
using ReleaseManagement.Framework.Authorisation;
using ReleaseManagement.Framework.Data;
using ReleaseManagement.Framework.Enums;
using ReleaseManagement.Framework.Graph;
using ReleaseManagement.Framework.Interfaces;

namespace ReleaseManagement.Framework.Managers
{
    public class SecurityManager : ISecurityManager
    {
        public SecurityManager(ISettingsManager settings)
        {
            //_Context = db;
            _settings = settings;
            _authSettings = new Dictionary<string, object>();
        }

        //private readonly ReleaseDataContext _Context;
        private readonly ISettingsManager _settings;
        private bool _isConfigured = false;
        private Dictionary<string, object> _authSettings;
        private AuthenticationType _authType = AuthenticationType.NotSet;

        public AuthenticationType AuthType {  get { return _authType; } }
        public bool IsConfigured { get {  return _isConfigured; } }

        public bool VerifyAndLoadAuthConfig()
        {
            _isConfigured = false;

            var record = _settings.GetValue(ReleaseConstants.SettingKeys.AUTH_TYPE);

            _authType = record == null ? AuthenticationType.NotSet : (AuthenticationType)record;

            if(_authType == AuthenticationType.NotSet) return false;

            else if(_authType == AuthenticationType.AzureAD)
            {
                string instance = _settings.GetValue(ReleaseConstants.SettingKeys.AZURE_INSTANCE).ToString();
                string domain = _settings.GetValue(ReleaseConstants.SettingKeys.AZURE_DOMAIN).ToString();
                string tenant = _settings.GetValue(ReleaseConstants.SettingKeys.AZURE_TENANTID).ToString();
                string client = _settings.GetValue(ReleaseConstants.SettingKeys.AZURE_CLIENTID).ToString();
                string callback = _settings.GetValue(ReleaseConstants.SettingKeys.AZURE_CALLBACKPATH).ToString();
                string signout = _settings.GetValue(ReleaseConstants.SettingKeys.AZURE_SIGNEDOUTCALLBACKPATH).ToString();
                string secret = _settings.GetValue(ReleaseConstants.SettingKeys.AZURE_CLIENTSECRET).ToString();
                string groupadmin = _settings.GetValue(ReleaseConstants.SettingKeys.AZURE_GROUPADMIN).ToString();
                string groupmember = _settings.GetValue(ReleaseConstants.SettingKeys.AZURE_GROUPMEMBER).ToString();

                if(String.IsNullOrWhiteSpace(instance) || String.IsNullOrWhiteSpace(domain) || String.IsNullOrWhiteSpace(tenant) || String.IsNullOrWhiteSpace(client) ||
                    String.IsNullOrWhiteSpace(callback) || String.IsNullOrWhiteSpace(signout) || String.IsNullOrWhiteSpace(secret) || String.IsNullOrWhiteSpace(groupadmin) ||
                    String.IsNullOrWhiteSpace(groupmember))
                    return false;

                _authSettings[ReleaseConstants.SettingKeys.AZURE_INSTANCE] = instance;
                _authSettings[ReleaseConstants.SettingKeys.AZURE_DOMAIN] = domain;
                _authSettings[ReleaseConstants.SettingKeys.AZURE_TENANTID] = tenant;
                _authSettings[ReleaseConstants.SettingKeys.AZURE_CLIENTID] = client;
                _authSettings[ReleaseConstants.SettingKeys.AZURE_CALLBACKPATH] = callback;
                _authSettings[ReleaseConstants.SettingKeys.AZURE_SIGNEDOUTCALLBACKPATH] = signout;
                _authSettings[ReleaseConstants.SettingKeys.AZURE_CLIENTSECRET] = secret;
                _authSettings[ReleaseConstants.SettingKeys.AZURE_GROUPADMIN] = groupadmin;
                _authSettings[ReleaseConstants.SettingKeys.AZURE_GROUPMEMBER] = groupmember;

            }
            else if(_authType == AuthenticationType.InApp)
            {

            }

            _isConfigured = true;
            return true;
        }

        public void Configure(IServiceCollection services, IConfiguration Configuration)
        {
            string groupAdmin = "";
            string groupMember = "";

            if(_authType == AuthenticationType.AzureAD)
            { 
                var initialScopes = new string[] { ReleaseConstants.Security.ScopeUserRead, ReleaseConstants.Security.ScopeGroupMemberRead };

                MicrosoftIdentityOptions adOptions = new MicrosoftIdentityOptions()
                { 
                    TenantId = _authSettings[ReleaseConstants.SettingKeys.AZURE_TENANTID].ToString(),
                    Instance = _authSettings[ReleaseConstants.SettingKeys.AZURE_INSTANCE].ToString(),
                    Domain = _authSettings[ReleaseConstants.SettingKeys.AZURE_DOMAIN].ToString(),
                    ClientId = _authSettings[ReleaseConstants.SettingKeys.AZURE_CLIENTID].ToString(),
                    CallbackPath = _authSettings[ReleaseConstants.SettingKeys.AZURE_CALLBACKPATH].ToString(),
                    SignedOutCallbackPath = _authSettings[ReleaseConstants.SettingKeys.AZURE_SIGNEDOUTCALLBACKPATH].ToString(),
                    ClientSecret = _authSettings[ReleaseConstants.SettingKeys.AZURE_CLIENTSECRET].ToString()
                };

                ConfidentialClientApplicationOptions apiOptions = new ConfidentialClientApplicationOptions()
                { 
                    TenantId = _authSettings[ReleaseConstants.SettingKeys.AZURE_TENANTID].ToString(),
                    Instance = _authSettings[ReleaseConstants.SettingKeys.AZURE_INSTANCE].ToString(),
                    ClientId = _authSettings[ReleaseConstants.SettingKeys.AZURE_CLIENTID].ToString(),
                    ClientSecret = _authSettings[ReleaseConstants.SettingKeys.AZURE_CLIENTSECRET].ToString()
                };

                services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme).AddMicrosoftIdentityWebApp(options => {
                        options = adOptions;
                        options.Events = new OpenIdConnectEvents();
                        options.Events.OnTokenValidated = async context =>
                        {
                            var overageGroupClaims = await GraphHelper.GetSignedInUsersGroups(context);
                        };
                    }).EnableTokenAcquisitionToCallDownstreamApi(options => options = apiOptions, initialScopes)
                            .AddMicrosoftGraph(Configuration.GetSection("GraphAPI"))
                            .AddInMemoryTokenCaches();

                groupAdmin = _authSettings[ReleaseConstants.SettingKeys.AZURE_GROUPADMIN].ToString();
                groupAdmin = _authSettings[ReleaseConstants.SettingKeys.AZURE_GROUPMEMBER].ToString();
            }
            else if(_authType == AuthenticationType.InApp)
            {

            }

            services.AddAuthorization(options =>
            {
                options.AddPolicy("GroupAdmin", policy => policy.Requirements.Add(new GroupPolicyRequirement(groupAdmin)));
                options.AddPolicy("GroupMember", policy => policy.Requirements.Add(new GroupPolicyRequirement(groupMember)));
                options.AddPolicy("GroupAny", policy => { 
                    policy.Requirements.Add(new GroupPolicyRequirement(groupMember, groupAdmin));
                });
            });

            services.AddSingleton<IAuthorizationHandler, GroupPolicyHandler>();
        }
    }
}
