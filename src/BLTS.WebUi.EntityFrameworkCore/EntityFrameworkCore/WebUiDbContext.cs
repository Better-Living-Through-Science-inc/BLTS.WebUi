using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using BLTS.WebUi.Authorization.Roles;
using BLTS.WebUi.Authorization.Users;
using BLTS.WebUi.MultiTenancy;

namespace BLTS.WebUi.EntityFrameworkCore
{
    public class WebUiDbContext : AbpZeroDbContext<Tenant, Role, User, WebUiDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public WebUiDbContext(DbContextOptions<WebUiDbContext> options)
            : base(options)
        {
        }
    }
}
