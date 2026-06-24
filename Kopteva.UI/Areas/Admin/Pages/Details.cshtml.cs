using Kopteva.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tourist.Domain.Entities;

namespace Kopteva.UI.Areas.Admin.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly IAttractionService _attractionService;

        public DetailsModel(IAttractionService attractionService)
        {
            _attractionService = attractionService;
        }

        public Attraction Attraction { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var response = await _attractionService.GetAttractionByIdAsync(id.Value);
            if (!response.Success || response.Data == null) return NotFound();

            Attraction = response.Data;
            return Page();
        }
    }
}