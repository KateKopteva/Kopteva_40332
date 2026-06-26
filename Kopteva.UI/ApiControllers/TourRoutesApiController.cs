using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tourist.Domain.Data;
using Tourist.Domain.Entities;

namespace Kopteva.UI.ApiControllers
{
    /// <summary>
    /// REST API контроллер для работы с туристическими маршрутами
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TourRoutesApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TourRoutesApiController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получить все маршруты
        /// </summary>
        /// <remarks>
        /// GET: api/TourRoutesApi
        /// </remarks>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TourRoute>>> GetTourRoutes()
        {
            return await _context.TourRoutes.ToListAsync();
        }

        /// <summary>
        /// Получить маршрут по Id
        /// </summary>
        /// <remarks>
        /// GET: api/TourRoutesApi/5
        /// </remarks>
        [HttpGet("{id}")]
        public async Task<ActionResult<TourRoute>> GetTourRoute(int id)
        {
            var tourRoute = await _context.TourRoutes.FindAsync(id);

            if (tourRoute == null)
            {
                return NotFound();
            }

            return tourRoute;
        }

        /// <summary>
        /// Обновить маршрут
        /// </summary>
        /// <remarks>
        /// PUT: api/TourRoutesApi/5
        /// </remarks>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTourRoute(int id, TourRoute tourRoute)
        {
            if (id != tourRoute.Id)
            {
                return BadRequest();
            }

            _context.Entry(tourRoute).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TourRouteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Создать новый маршрут
        /// </summary>
        /// <remarks>
        /// POST: api/TourRoutesApi
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult<TourRoute>> PostTourRoute(TourRoute tourRoute)
        {
            _context.TourRoutes.Add(tourRoute);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTourRoute", new { id = tourRoute.Id }, tourRoute);
        }

        /// <summary>
        /// Удалить маршрут
        /// </summary>
        /// <remarks>
        /// DELETE: api/TourRoutesApi/5
        /// </remarks>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTourRoute(int id)
        {
            var tourRoute = await _context.TourRoutes.FindAsync(id);
            if (tourRoute == null)
            {
                return NotFound();
            }

            _context.TourRoutes.Remove(tourRoute);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TourRouteExists(int id)
        {
            return _context.TourRoutes.Any(e => e.Id == id);
        }
    }
}