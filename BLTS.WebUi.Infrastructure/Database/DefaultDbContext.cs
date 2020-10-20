using BLTS.WebUi.Models;
using Microsoft.EntityFrameworkCore;

namespace BLTS.WebUi.Database
{
    /// <summary>
    /// DB Context for Application
    /// </summary>
    public class DefaultDbContext : DbContext
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationLog> AuditLog { get; set; }
        public DbSet<OperationalConfiguration> OperationalConfiguration { get; set; }
    }
}
