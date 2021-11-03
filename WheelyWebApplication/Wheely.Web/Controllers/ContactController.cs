using Microsoft.AspNetCore.Mvc;
using Wheely.Web.Models.Contacts;

namespace Wheely.Web.Controllers
{
    public class ContactController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Send(ContactModel contactModel)
        {
            if (!ModelState.IsValid) return View();

            return View();
        }
    }
}
