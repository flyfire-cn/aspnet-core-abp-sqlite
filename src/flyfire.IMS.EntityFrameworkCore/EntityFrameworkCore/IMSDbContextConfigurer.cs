using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace flyfire.IMS.EntityFrameworkCore
{
    public static class IMSDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<IMSDbContext> builder, string connectionString)
        {
            builder.UseSqlite(connectionString, opt => opt.UseNetTopologySuite());
        }

        public static void Configure(DbContextOptionsBuilder<IMSDbContext> builder, DbConnection connection)
        {
            builder.UseSqlite(connection,opt=>opt.UseNetTopologySuite());
        }
    }
}
