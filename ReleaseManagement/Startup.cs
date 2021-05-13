using Blazored.Modal;
using MatBlazor;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using ReleaseManagement.Framework;
using ReleaseManagement.Framework.Authorisation;
using ReleaseManagement.Framework.Data;
using ReleaseManagement.Framework.Graph;
using ReleaseManagement.Framework.Interfaces;
using ReleaseManagement.Framework.Logging;
using ReleaseManagement.Framework.Managers;
using ReleaseManagement.Framework.Services;
using Thirsol.Notifications.Toasts;

namespace ReleaseManagement
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var dbOptions = new DbContextOptionsBuilder<ReleaseDataContext>().UseSqlite(Configuration.GetConnectionString("ReleaseDB"));
            ReleaseDataContext context = new ReleaseDataContext(dbOptions.Options);

            context.Database.Migrate();

            //services.AddDbContext<ReleaseDataContext>(options =>
            //    options.UseSqlite(
            //        Configuration.GetConnectionString("ReleaseDB")), ServiceLifetime.Singleton);

            ISettingsManager settings = new SettingsManager(context);
            ISecurityManager security = new SecurityManager(settings);

            security.VerifyAndLoadAuthConfig();

            services.AddSingleton<ISecurityManager>(security);
            services.AddSingleton<ISettingsManager>(settings);
            services.AddSingleton<ReleaseDataContext>(context);

            if(security.IsConfigured)
            {
                security.Configure(services, Configuration);

                services.AddDistributedMemoryCache();

                services.AddSession(options =>
                {
                    options.Cookie.HttpOnly = true;
                    options.Cookie.IsEssential = true;
                });

                services.AddControllersWithViews(options =>
                {
                    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();
                    options.Filters.Add(new AuthorizeFilter(policy));
                }).AddMicrosoftIdentityUI();

                services.AddRazorPages();
                services.AddServerSideBlazor().AddMicrosoftIdentityConsentHandler();

                services.AddBlazoredModal();
                services.UseToasts();
                services.AddMatBlazor();

                services.AddHttpContextAccessor();

                services.AddMvc(options => options.EnableEndpointRouting = false).SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);

                services.AddSingleton<IComponentTypeDataService, ComponentTypeDataService>();
                services.AddSingleton<IReleaseDataService, ReleaseDataService>();
                services.AddSingleton<IComponentDataService, ComponentDataService>();
                services.AddSingleton<IComponentApprovalDataService, ComponentApprovalDataService>();
                services.AddSingleton<ILogDataService, LogDataService>();
                services.AddSingleton<IAuditHeaderDataService, AuditHeaderDataService>();
                services.AddSingleton<IAuditItemDataService, AuditItemDataService>();

                services.AddTransient<IAuditService, AuditService>();
                services.AddTransient<IRMLogger, ReleaseManagementLogger>();
            }
            else
            {

            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ISecurityManager secManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            if(secManager.IsConfigured)
            { 
                app.UseAuthentication();
                app.UseAuthorization();
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
