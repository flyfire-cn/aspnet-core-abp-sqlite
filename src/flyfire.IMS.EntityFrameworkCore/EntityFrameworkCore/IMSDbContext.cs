using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using flyfire.IMS.Authorization.Roles;
using flyfire.IMS.Authorization.Users;
using flyfire.IMS.MultiTenancy;
using flyfire.IMS.Facilities;

namespace flyfire.IMS.EntityFrameworkCore
{
    public class IMSDbContext : AbpZeroDbContext<Tenant, Role, User, IMSDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public  DbSet<Facility> facilities { get; set; }

        public IMSDbContext(DbContextOptions<IMSDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Facility>(f =>
            {
                f.ToTable("Facilities");
                f.Property(a => a.Location).ForSqliteHasSrid(4326);               //SQLiteSpatialite
            });
        }
    }
}
