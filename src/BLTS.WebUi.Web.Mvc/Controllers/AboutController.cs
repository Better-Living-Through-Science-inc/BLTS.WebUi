using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using BLTS.WebUi.Controllers;

namespace BLTS.WebUi.Web.Controllers
{
    public class AboutController : WebUiControllerBase
    {

    public ActionResult Index()
    {
      return View();
    }

    public ActionResult Budget()
    {
      return View();
    }

    [AbpMvcAuthorize]
    public ActionResult CampusInfo()
    {
      return View();
    }

    [AbpMvcAuthorize]
    public ActionResult CampusMap()
    {
      return View();
    }

    public ActionResult Contact()
    {
      return View();
    }

    public ActionResult CoreValues()
    {
      return View();
    }

    public ActionResult EmployeeRecruitment()
    {
      return View();
    }

    public ActionResult Organization()
    {
      return View();
    }

  }
}
