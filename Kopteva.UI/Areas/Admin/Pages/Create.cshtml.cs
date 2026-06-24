using Kopteva.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tourist.Domain.Entities;

namespace Kopteva.UI.Areas.Admin.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IAttractionService _attractionService;
        private readonly ITourRouteService _routeService;

        public CreateModel(IAttractionService attractionService, ITourRouteService routeService)
        {
            _attractionService = attractionService;
            _routeService = routeService;
        }

        public IActionResult OnGet()
        {
            LoadRoutes();
            return Page();
        }

        [BindProperty]
        public Attraction Attraction { get; set; } = default!;

        [BindProperty]
        public IFormFile? ImageFile { get; set; }

        public List<Tourist.Domain.Entities.TourRoute>? Routes { get; set; }

        private void LoadRoutes()
        {
            var response = _routeService.GetTourRouteListAsync().Result;
            Routes = response.Success ? response.Data : new List<Tourist.Domain.Entities.TourRoute>();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                LoadRoutes();
                return Page();
            }

            var response = await _attractionService.CreateAttractionAsync(Attraction, ImageFile);
            if (response.Success)
            {
                return RedirectToPage("./Index");
            }

            ModelState.AddModelError("", response.ErrorMessage ?? "Ошибка при создании");
            LoadRoutes();
            return Page();
        }
    }
}