using Microsoft.AspNetCore.Mvc;
using BLTS.WebUi.Controllers;

namespace BLTS.WebUi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
