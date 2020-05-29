using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace BLTS.WebUi.Web.Views
{
    public abstract class WebUiRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected WebUiRazorPage()
        {
            LocalizationSourceName = WebUiConsts.LocalizationSourceName;
        }
    }
}
