using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BLTS.WebUi.Controllers
{
  public class ErrorController : Controller
  {
    //private readonly IErrorInfoBuilder _errorInfoBuilder;

    //public ErrorController(IErrorInfoBuilder errorInfoBuilder)
    public ErrorController()
    {
      //_errorInfoBuilder = errorInfoBuilder;
    }

    public ActionResult Index()
    {
            IExceptionHandlerFeature exHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();

            Exception exception = exHandlerFeature != null
                          ? exHandlerFeature.Error
                          : new Exception("Unhandled exception!");



            IStatusCodeReExecuteFeature statusCodeReExecuteFeature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
      if (statusCodeReExecuteFeature != null)
      {
        string OriginalURL =
            statusCodeReExecuteFeature.OriginalPathBase
            + statusCodeReExecuteFeature.OriginalPath
            + statusCodeReExecuteFeature.OriginalQueryString;
      }




      return View(
          //"Error",
          //new ErrorViewModel(
              //_errorInfoBuilder.BuildForException(exception),
              //exception
          //)
      );
    }
  }
}
