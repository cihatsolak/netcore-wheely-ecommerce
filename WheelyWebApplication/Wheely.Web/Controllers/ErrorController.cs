using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

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

            return View();
        }

        [HttpGet]
        public IActionResult StatusCode404()
        {
            return View();
        }
    }
}
