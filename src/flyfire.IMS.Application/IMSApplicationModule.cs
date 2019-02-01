using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using flyfire.IMS.Authorization;

namespace flyfire.IMS
{
    [DependsOn(
        typeof(IMSCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class IMSApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<IMSAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(IMSApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}
