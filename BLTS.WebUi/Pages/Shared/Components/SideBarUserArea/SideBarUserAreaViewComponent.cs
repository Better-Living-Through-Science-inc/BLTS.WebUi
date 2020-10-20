using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BLTS.WebUi.Pages.Shared.Components.SideBarUserArea
{
    public class SideBarUserAreaViewComponent : ViewComponent
    {
        public SideBarUserAreaViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            SideBarUserAreaViewModel model = new SideBarUserAreaViewModel();

            return View(model);
        }
    }
}
