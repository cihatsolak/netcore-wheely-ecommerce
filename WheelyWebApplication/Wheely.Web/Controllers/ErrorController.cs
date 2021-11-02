using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Wheely.Core.Utilities;

namespace Wheely.Web.Controllers
{
    [AllowAnonymous]
    public class ErrorController : BaseController
    {
        [HttpGet]
        public IActionResult InternalServerError()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            //Todo: Log

            return View($"~/Views/Shared/Error/InternalServerError.cshtml");
        }

        [HttpGet]
        public IActionResult Handle(int statusCode)
        {
            if (0 > statusCode) return RedirectToAction(nameof(InternalServerError));

            var httpStatusCode = statusCode.ToEnum<HttpStatusCode>();
            string viewName = string.Empty;

            switch (httpStatusCode)
            {
                case HttpStatusCode.NotFound:
                    viewName = "NotFound";
                    break;
                case HttpStatusCode.Forbidden:
                    break;
                default:
                    break;
            }

            return View($"~/Views/Shared/Error/{viewName}.cshtml");
        }
    }
}
