using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BLTS.WebUi.Controllers
{
    public abstract class WebUiControllerBase : Controller
  {
    public object LocalizationSourceName { get; }
        protected WebUiControllerBase()
        {
            LocalizationSourceName = "";
        }


    protected void CheckErrors(IdentityResult identityResult)
        {
        }
    }
}
