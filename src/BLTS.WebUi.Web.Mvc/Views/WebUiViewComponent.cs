using Abp.AspNetCore.Mvc.ViewComponents;

namespace BLTS.WebUi.Web.Views
{
    public abstract class WebUiViewComponent : AbpViewComponent
    {
        protected WebUiViewComponent()
        {
            LocalizationSourceName = WebUiConsts.LocalizationSourceName;
        }
    }
}
