using Kopteva.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tourist.Domain.Entities;

namespace Kopteva.UI.Areas.Admin.Pages
{
    public class EditModel : PageModel
    {
        private readonly IAttractionService _attractionService;
        private readonly ITourRouteService _routeService;

        public EditModel(IAttractionService attractionService, ITourRouteService routeService)
        {
            _attractionService = attractionService;
            _routeService = routeService;
        }

        [BindProperty]
        public Attraction Attraction { get; set; } = default!;

        [BindProperty]
        public IFormFile? ImageFile { get; set; }

        public List<Tourist.Domain.Entities.TourRoute>? Routes { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var response = await _attractionService.GetAttractionByIdAsync(id.Value);
            if (!response.Success || response.Data == null) return NotFound();

            Attraction = response.Data;
            LoadRoutes();
            return Page();
        }

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

            await _attractionService.UpdateAttractionAsync(Attraction.Id, Attraction, ImageFile);
            return RedirectToPage("./Index");
        }
    }
}