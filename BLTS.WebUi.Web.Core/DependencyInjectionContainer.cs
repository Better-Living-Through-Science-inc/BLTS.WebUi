using AutoMapper;
using BLTS.WebUi.Configurations;
using BLTS.WebUi.ContentManagementSystem;
using BLTS.WebUi.Logs;
using BLTS.WebUi.UserManagers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;

namespace BLTS.WebUi.Web.Core
{
    /// <summary>
    /// dependency injection service
    /// </summary>
    public class DependencyInjectionContainer
    {
        IServiceCollection _services;

        public DependencyInjectionContainer(IServiceCollection services)
        {
            _services = services;
        }

        /// <summary>
        /// add all object to the dependency injection service provider
        /// </summary>
        /// <returns></returns>
        public void Initialize()
        {
            /*Application Services*/
            
            _services.AddTransient<UserAuthenticationManager>();
            _services.AddTransient<IApplicationLogTools, ApplicationLogTools>();
            _services.AddTransient<ContentManagementSystemManager>();
            _services.AddTransient<ConfigurationManager>();
            //_services.AddTransient<FileStorageManager>();
            _services.AddTransient<IMapper, Mapper>();
            //_services.AddTransient<ReflectionTools>();
            //_services.AddTransient<StringUtilities>();
            //_services.AddTransient<UnitConversionLogic>();
            //_services.AddTransient<UserManager>();


            //services.AddTransient<IRepository<ApplicationLog, long>, Repository<ApplicationLog, long>>();
            //services.AddTransient<IRepository<OperationalConfiguration, long>, Repository<OperationalConfiguration, long>>();


            /*Api Repository Models*/
            //_services.AddTransient<IApiRepository<Weather, WeatherDto, CreateUpdateWeatherDto, long>, ApiRepository<Weather, WeatherDto, CreateUpdateWeatherDto, long>>();
        }

    }
}
