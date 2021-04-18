using BLTS.WebUi.DtoModels;
using BLTS.WebUi.Infrastructure.AzureApi;
using BLTS.WebUi.Models;
using Microsoft.Extensions.DependencyInjection;

namespace BLTS.WebUi.Infrastructure
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
            /*General Application Services*/
            _services.AddHttpClient<IApiCmsRepository, ApiCmsRepository>();
            //_services.AddHttpClient<ApiAuthentication>();

        }
    }
}
