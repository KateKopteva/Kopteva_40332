using Tourist.Domain.Interfaces;
using Kopteva.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kopteva.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IAttractionService _attractionService;
        private readonly ITourRouteService _routeService;

        public ProductController(IAttractionService attractionService, ITourRouteService routeService)
        {
            _attractionService = attractionService;
            _routeService = routeService;
        }

        public async Task<IActionResult> Index(string? category)
        {
            // Получить список маршрутов
            var routesResponse = await _routeService.GetTourRouteListAsync();
            if (!routesResponse.Success)
                return NotFound(routesResponse.ErrorMessage);

            // Передать список маршрутов во ViewData
            ViewData["categories"] = routesResponse.Data;

            // Передать во ViewData имя текущего маршрута
            var currentCategory = category == null
                ? "Все"
                : routesResponse.Data?.FirstOrDefault(r => r.NormalizedName == category)?.Name;
            ViewData["currentCategory"] = currentCategory;

            // Получить список достопримечательностей
            var attractionResponse = await _attractionService.GetAttractionListAsync(category);
            if (!attractionResponse.Success)
                ViewData["Error"] = attractionResponse.ErrorMessage;

            return View("~/Views/Product/Index.cshtml",attractionResponse.Data);
        }
    }
}