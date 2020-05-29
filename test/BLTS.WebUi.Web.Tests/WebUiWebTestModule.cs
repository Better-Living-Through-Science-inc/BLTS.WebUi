using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using BLTS.WebUi.EntityFrameworkCore;
using BLTS.WebUi.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace BLTS.WebUi.Web.Tests
{
    [DependsOn(
        typeof(WebUiWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class WebUiWebTestModule : AbpModule
    {
        public WebUiWebTestModule(WebUiEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(WebUiWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(WebUiWebMvcModule).Assembly);
        }
    }
}