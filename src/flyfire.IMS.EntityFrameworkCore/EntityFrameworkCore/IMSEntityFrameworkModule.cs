using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using flyfire.IMS.EntityFrameworkCore.Seed;

namespace flyfire.IMS.EntityFrameworkCore
{
    [DependsOn(
        typeof(IMSCoreModule), 
        typeof(AbpZeroCoreEntityFrameworkCoreModule))]
    public class IMSEntityFrameworkModule : AbpModule
    {
        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }

        public override void PreInitialize()
        {
            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<IMSDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        IMSDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        IMSDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                    }
                });
            }

            // add this line to disable transactions
            Configuration.UnitOfWork.IsTransactional = false;

        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(IMSEntityFrameworkModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            if (!SkipDbSeed)
            {
                SeedHelper.SeedHostDb(IocManager);
            }
        }
    }
}
