using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BLTS.WebUi.Web.Core
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
            /*AutoMapper*/
            _services.AddAutoMapper(Assembly.GetAssembly(typeof(AutoMapperProfile)));

            DependencyInjectionContainer dependencyInjectionStartup = new DependencyInjectionContainer(_services);
            dependencyInjectionStartup.Initialize();

            Infrastructure.Startup infrastructureStartup = new Infrastructure.Startup(_services, _configuration);
            infrastructureStartup.Initialize();

        }

    }
}