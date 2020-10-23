using BLTS.WebUi.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BLTS.WebUi.Infrastructure
{
    public class Startup
    {
        IServiceCollection _services;
        IConfiguration _configuration;

        public Startup(IServiceCollection services, IConfiguration configuration)
        {
            _services = services;
            _configuration = configuration;
        }

        public void Initialize()
        {
            ConfigureServices();
        }

        public void ConfigureServices()
        {
            _services.AddDbContext<DefaultDbContext>(options => options.UseSqlServer(
                                                _configuration.GetConnectionString("Default"),
                                                b => b.MigrationsAssembly(typeof(DefaultDbContext).Assembly.FullName)));
        }
    }
}
