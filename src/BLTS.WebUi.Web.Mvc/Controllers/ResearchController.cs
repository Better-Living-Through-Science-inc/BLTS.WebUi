using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using BLTS.WebUi.Controllers;

namespace BLTS.WebUi.Web.Controllers
{
    public class ResearchController : WebUiControllerBase
    {

    public ActionResult Index()
    {
      return View();
    }

    public ActionResult Atomic()
    {
      return View();
    }

    public ActionResult Biophysics()
    {
      return View();
    }

    public ActionResult Hardware()
    {
      return View();
    }

    public ActionResult Neurophysics()
    {
      return View();
    }

    public ActionResult Software()
    {
      return View();
    }

  }
}
