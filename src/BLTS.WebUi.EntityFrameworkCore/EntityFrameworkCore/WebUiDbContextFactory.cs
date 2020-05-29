using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using BLTS.WebUi.Configuration;
using BLTS.WebUi.Web;

namespace BLTS.WebUi.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class WebUiDbContextFactory : IDesignTimeDbContextFactory<WebUiDbContext>
    {
        public WebUiDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<WebUiDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            WebUiDbContextConfigurer.Configure(builder, configuration.GetConnectionString(WebUiConsts.ConnectionStringName));

            return new WebUiDbContext(builder.Options);
        }
    }
}
