using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tourist.Domain.Entities;
using Tourist.Domain.Interfaces;

namespace Kopteva.UI.Areas.Admin.Pages
{
    [Authorize(Policy = "admin")]  // ← ДОБАВЬТЕ ЭТОТ АТРИБУТ!
    public class IndexModel : PageModel
    {
        private readonly IAttractionService _attractionService;

        public IndexModel(IAttractionService attractionService)
        {
            _attractionService = attractionService;
        }

        public List<Attraction> Attractions { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var response = await _attractionService.GetAttractionListAsync(null);
            if (response.Success)
            {
                Attractions = response.Data ?? new List<Attraction>();
            }
            else
            {
                Attractions = new List<Attraction>();
                TempData["Error"] = response.ErrorMessage;
            }
        }
    }
}