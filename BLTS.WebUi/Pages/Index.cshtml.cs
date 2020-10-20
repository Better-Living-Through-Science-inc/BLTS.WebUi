using BLTS.WebUi.Logs;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BLTS.WebUi.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationLogTools _applicationLogTools;

        public IndexModel(ApplicationLogTools applicationLogTools)
        {
            _applicationLogTools = applicationLogTools;
        }

        public void OnGet()
        {

        }
    }
}
