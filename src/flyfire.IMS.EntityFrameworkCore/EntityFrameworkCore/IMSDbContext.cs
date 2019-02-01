using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using flyfire.IMS.Authorization.Roles;
using flyfire.IMS.Authorization.Users;
using flyfire.IMS.MultiTenancy;

namespace flyfire.IMS.EntityFrameworkCore
{
    public class IMSDbContext : AbpZeroDbContext<Tenant, Role, User, IMSDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public IMSDbContext(DbContextOptions<IMSDbContext> options)
            : base(options)
        {
        }
    }
}
