using Microsoft.AspNetCore.Mvc;

namespace Pustok_MVC.Areas.Manage.Controllers
{
    public class ErrorController : Controller
    {
        [Area("manage")]
        public IActionResult NotFound()
        {
            return View();
        }
    }
}
