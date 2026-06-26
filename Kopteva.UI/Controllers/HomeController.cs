using Microsoft.AspNetCore.Mvc;

namespace Kopteva.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Product");
        }
    }
}
