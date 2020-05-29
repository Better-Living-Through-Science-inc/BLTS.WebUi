using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using BLTS.WebUi.Authorization;

namespace BLTS.WebUi
{
    [DependsOn(
        typeof(WebUiCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class WebUiApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<WebUiAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(WebUiApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
