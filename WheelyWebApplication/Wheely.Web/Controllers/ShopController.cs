using Microsoft.AspNetCore.Mvc;

namespace Wheely.Web.Controllers
{
    public class ShopController : BaseController
    {
        public IActionResult Detail()
        {
            return View();
        }
    }
}
