using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using BLTS.WebUi.Configuration;

namespace BLTS.WebUi.Web.Host.Startup
{
    [DependsOn(
       typeof(WebUiWebCoreModule))]
    public class WebUiWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public WebUiWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(WebUiWebHostModule).GetAssembly());
        }
    }
}
