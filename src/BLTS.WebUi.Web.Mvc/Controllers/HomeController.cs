using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using BLTS.WebUi.Controllers;

namespace BLTS.WebUi.Web.Controllers
{
    public class HomeController : WebUiControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
