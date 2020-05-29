using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace BLTS.WebUi.Controllers
{
    public abstract class WebUiControllerBase: AbpController
    {
        protected WebUiControllerBase()
        {
            LocalizationSourceName = WebUiConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
