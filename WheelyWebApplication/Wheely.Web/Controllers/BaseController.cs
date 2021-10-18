using Microsoft.AspNetCore.Mvc;

namespace Wheely.Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class BaseController : Controller
    {
    }
}
