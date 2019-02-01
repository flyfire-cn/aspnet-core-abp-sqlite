using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using flyfire.IMS.Configuration;

namespace flyfire.IMS.Web.Host.Startup
{
    [DependsOn(
       typeof(IMSWebCoreModule))]
    public class IMSWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public IMSWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(IMSWebHostModule).GetAssembly());
        }
    }
}
