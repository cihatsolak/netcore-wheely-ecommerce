using Microsoft.AspNetCore.Mvc;

namespace Wheely.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}