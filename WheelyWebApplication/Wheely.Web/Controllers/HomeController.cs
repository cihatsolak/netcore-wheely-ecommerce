using Microsoft.AspNetCore.Mvc;
using Wheely.Service.Routes;

namespace Wheely.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRouteService _routeService;

        public HomeController(IRouteService routeService)
        {
            _routeService = routeService;
        }

        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            //_routeService.GetRoutes();
            return View();
        }
    }
}