using Microsoft.AspNetCore.Mvc;

namespace Wheely.Web.Controllers
{
    public class HomeController : BaseController
    {
        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}