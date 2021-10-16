using Microsoft.AspNetCore.Mvc;

namespace Wheely.Web.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
