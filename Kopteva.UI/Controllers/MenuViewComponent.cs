using Microsoft.AspNetCore.Mvc;

namespace Kopteva.UI.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            // Получаем имя текущего контроллера из маршрута
            ViewData["Controller"] = HttpContext.Request.RouteValues["controller"]
                ?.ToString()
                ?.ToLower()
                ?? string.Empty;

            // Получаем имя текущей области (area)
            ViewData["Area"] = HttpContext.Request.RouteValues["area"]
                ?.ToString()
                ?.ToLower()
                ?? string.Empty;

            return View();
        }
    }
}