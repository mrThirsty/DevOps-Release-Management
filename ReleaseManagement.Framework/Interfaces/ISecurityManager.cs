using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReleaseManagement.Framework.Enums;

namespace ReleaseManagement.Framework.Interfaces
{
    public interface ISecurityManager
    {
        AuthenticationType AuthType {  get; }
        bool IsConfigured { get; }

        bool VerifyAndLoadAuthConfig();
        void Configure(IServiceCollection services, IConfiguration Configuration);
    }
}
