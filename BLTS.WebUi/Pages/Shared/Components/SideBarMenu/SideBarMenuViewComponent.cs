using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BLTS.WebUi.Pages.Shared.Components.SideBarMenu
{
    public class SideBarMenuViewComponent : ViewComponent
    {

        public SideBarMenuViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new SideBarMenuViewModel
            {
                //MainMenu = await _userNavigationManager.GetMenuAsync("MainMenu", _abpSession.ToUserIdentifier())
            };

            return View(model);
        }
    }
}
