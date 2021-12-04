using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wheely.Web.Controllers.Public
{
    [AllowAnonymous]
    public class HealthCheckController : BaseController
    {
        public IActionResult WheelyWeb()
        {
            return View();
        }
    }
}
