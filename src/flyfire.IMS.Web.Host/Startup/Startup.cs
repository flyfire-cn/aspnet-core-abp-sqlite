using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Castle.Facilities.Logging;
using Swashbuckle.AspNetCore.Swagger;
using Abp.AspNetCore;
using Abp.Castle.Logging.Log4Net;
using Abp.Extensions;
using flyfire.IMS.Configuration;
using flyfire.IMS.Identity;
using Abp.AspNetCore.SignalR.Hubs;

namespace flyfire.IMS.Web.Host.Startup
{
    public class Startup
    {
        private const string _defaultCorsPolicyName = "localhost";

        private readonly IConfigurationRoot _appConfiguration;

        public Startup(IHostingEnvironment env)
        {
            _appConfiguration = env.GetAppConfiguration();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // MVC
            services.AddMvc(
                options => options.Filters.Add(new CorsAuthorizationFilterFactory(_defaultCorsPolicyName))
            );

            #region Add SPA Support
#if SPA_SUPPORT
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "IMS";
            });
#endif
            #endregion

            IdentityRegistrar.Register(services);
            AuthConfigurer.Configure(services, _appConfiguration);

            services.AddSignalR();

            // Configure CORS for angular2 UI
            services.AddCors(
                options => options.AddPolicy(
                    _defaultCorsPolicyName,
                    builder => builder
                        .WithOrigins(
                            // App:CorsOrigins in appsettings.json can contain more than one address separated by comma.
                            _appConfiguration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                )
            );

#if  !SPA_SUPPORT
            // Swagger - Enable this line and the related lines in Configure method to enable swagger UI
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "IMS API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);

                // Define the BearerAuth scheme that's in use
                options.AddSecurityDefinition("bearerAuth", new ApiKeyScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                // Assign scope requirements to operations based on AuthorizeAttribute
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });
#endif

            // Configure Abp and Dependency Injection
            return services.AddAbp<IMSWebHostModule>(
                // Configure Log4Net logging
                options => options.IocManager.IocContainer.AddFacility<LoggingFacility>(
                    f => f.UseAbpLog4Net().WithConfig("log4net.config")
                )
            );
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseAbp(options => { options.UseAbpRequestLocalization = false; }); // Initializes ABP framework.

            app.UseCors(_defaultCorsPolicyName); // Enable CORS!

            #region Add SPA Support
#if SPA_SUPPORT
            app.UseSpaStaticFiles();
#endif
            #endregion

#if !SPA_SUPPORT
            app.UseStaticFiles();
#endif

            app.UseAuthentication();

            app.UseAbpRequestLocalization();


            app.UseSignalR(routes =>
            {
                routes.MapHub<AbpCommonHub>("/signalr");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "defaultWithArea",
                    template: "{area}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            #region Add SPA Support
#if SPA_SUPPORT
            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501
                spa.Options.SourcePath = "IMS";
            });

            //if (env.IsProduction())
            //{
            //    app.UseSpa(spa =>
            //    {
            //        // To learn more about options for serving an Angular SPA from ASP.NET Core,
            //        // see https://go.microsoft.com/fwlink/?linkid=864501
            //        spa.Options.SourcePath = "IMS";
            //    });
            //};

            //if (env.IsDevelopment())
            //{
            //    //spa.UseAngularCliServer(npmScript: "start");

            //    // Enable middleware to serve generated Swagger as a JSON endpoint
            //    app.UseSwagger();
            //    // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            //    app.UseSwaggerUI(options =>
            //    {
            //        options.SwaggerEndpoint("/swagger/v1/swagger.json", "IMS API V1");
            //        // options.SwaggerEndpoint(_appConfiguration["App:SwaggerRootAddress"].EnsureEndsWith('/') + "swagger/v1/swagger.json", "IMS API V1");

            //        options.IndexStream = () => Assembly.GetExecutingAssembly()
            //            .GetManifestResourceStream("flyfire.IMS.Web.Host.wwwroot.swagger.ui.index.html");
            //    }); // URL: /swagger

            //}
#endif
            #endregion

#if !SPA_SUPPORT
            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();
            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(_appConfiguration["App:SwaggerRootAddress"].EnsureEndsWith('/') + "swagger/v1/swagger.json", "IMS API V1");
                options.IndexStream = () => Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream("flyfire.IMS.Web.Host.wwwroot.swagger.ui.index.html");
            }); // URL: /swagger
#endif
        }
    }
}
