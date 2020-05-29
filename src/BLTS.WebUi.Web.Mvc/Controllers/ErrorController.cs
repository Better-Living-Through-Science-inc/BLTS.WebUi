﻿using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Controllers;
using Abp.Web.Models;
using Abp.Web.Mvc.Models;

namespace BLTS.WebUi.Web.Controllers
{
  public class ErrorController : AbpController
  {
    private readonly IErrorInfoBuilder _errorInfoBuilder;

    public ErrorController(IErrorInfoBuilder errorInfoBuilder)
    {
      _errorInfoBuilder = errorInfoBuilder;
    }

    public ActionResult Index()
    {
      var exHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();

      var exception = exHandlerFeature != null
                          ? exHandlerFeature.Error
                          : new Exception("Unhandled exception!");



      var statusCodeReExecuteFeature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
      if (statusCodeReExecuteFeature != null)
      {
        string OriginalURL =
            statusCodeReExecuteFeature.OriginalPathBase
            + statusCodeReExecuteFeature.OriginalPath
            + statusCodeReExecuteFeature.OriginalQueryString;
      }




      return View(
          "Error",
          new ErrorViewModel(
              _errorInfoBuilder.BuildForException(exception),
              exception
          )
      );
    }
  }
}
