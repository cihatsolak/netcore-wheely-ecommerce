using Microsoft.AspNetCore.Mvc;
using Wheely.Web.Filters;
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
        [ValidateGoogleReCaptchaFilter]
        public IActionResult Index(ContactModel contactModel)
        {
            if (!ModelState.IsValid) 
                return View(contactModel);

            return LocalRedirect("/iletisim");
        }
    }
}
