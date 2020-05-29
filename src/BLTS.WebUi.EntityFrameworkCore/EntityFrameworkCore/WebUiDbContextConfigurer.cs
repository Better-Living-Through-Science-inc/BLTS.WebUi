using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace BLTS.WebUi.EntityFrameworkCore
{
    public static class WebUiDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<WebUiDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<WebUiDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
