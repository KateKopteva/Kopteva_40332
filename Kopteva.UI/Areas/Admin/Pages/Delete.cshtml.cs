using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tourist.Domain.Entities;
using Kopteva.UI.Services;


namespace Kopteva.UI.Areas.Admin.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IAttractionService _attractionService;

        public DeleteModel(IAttractionService attractionService)
        {
            _attractionService = attractionService;
        }

        [BindProperty]
        public Attraction Attraction { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var response = await _attractionService.GetAttractionByIdAsync(id.Value);
            if (!response.Success || response.Data == null) return NotFound();

            Attraction = response.Data;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null) return NotFound();

            await _attractionService.DeleteAttractionAsync(id.Value);
            return RedirectToPage("./Index");
        }
    }
}