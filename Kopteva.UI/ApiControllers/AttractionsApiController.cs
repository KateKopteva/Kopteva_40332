using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tourist.Domain.Data;
using Tourist.Domain.Entities;
using Tourist.Domain.Models;

namespace Kopteva.UI.ApiControllers
{
    /// <summary>
    /// REST API контроллер для работы с достопримечательностями
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AttractionsApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AttractionsApiController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получить список достопримечательностей с фильтрацией по маршруту
        /// </summary>
        /// <remarks>
        /// GET: api/AttractionsApi
        /// GET: api/AttractionsApi?category=zamki
        /// </remarks>
        [HttpGet]
        public async Task<ActionResult<ResponseData<List<Attraction>>>> GetAttractions([FromQuery] string? category)
        {
            try
            {
                IQueryable<Attraction> query = _context.Attractions
                    .Include(a => a.TourRoute);  // ← Включаем маршрут в ответ

                // Фильтрация по маршруту
                if (!string.IsNullOrEmpty(category))
                {
                    query = query.Where(a => a.TourRoute.NormalizedName == category);
                }

                var attractions = await query.ToListAsync();

                if (attractions.Count == 0)
                {
                    return Ok(ResponseData<List<Attraction>>.Error(
                        "Нет достопримечательностей в выбранном маршруте"));
                }

                return Ok(ResponseData<List<Attraction>>.OK(attractions));
            }
            catch (Exception ex)
            {
                return Ok(ResponseData<List<Attraction>>.Error(
                    $"Ошибка при получении списка: {ex.Message}"));
            }
        }

        /// <summary>
        /// Получить достопримечательность по Id
        /// </summary>
        /// <remarks>
        /// GET: api/AttractionsApi/5
        /// </remarks>
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseData<Attraction>>> GetAttraction(int id)
        {
            try
            {
                var attraction = await _context.Attractions
                    .Include(a => a.TourRoute)
                    .FirstOrDefaultAsync(a => a.Id == id);

                if (attraction == null)
                {
                    return Ok(ResponseData<Attraction>.Error(
                        $"Достопримечательность с Id={id} не найдена"));
                }

                return Ok(ResponseData<Attraction>.OK(attraction));
            }
            catch (Exception ex)
            {
                return Ok(ResponseData<Attraction>.Error(
                    $"Ошибка при получении: {ex.Message}"));
            }
        }

        /// <summary>
        /// Обновить достопримечательность
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttraction(int id, Attraction attraction)
        {
            if (id != attraction.Id)
            {
                return BadRequest();
            }

            _context.Entry(attraction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttractionExists(id))
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
        /// Создать новую достопримечательность
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ResponseData<Attraction>>> PostAttraction(Attraction attraction)
        {
            try
            {
                _context.Attractions.Add(attraction);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetAttraction", new { id = attraction.Id }, 
                    ResponseData<Attraction>.OK(attraction));
            }
            catch (Exception ex)
            {
                return Ok(ResponseData<Attraction>.Error(
                    $"Ошибка при создании: {ex.Message}"));
            }
        }

        /// <summary>
        /// Удалить достопримечательность
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttraction(int id)
        {
            var attraction = await _context.Attractions.FindAsync(id);
            if (attraction == null)
            {
                return NotFound();
            }

            _context.Attractions.Remove(attraction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Загрузить изображение для достопримечательности (Задание 8)
        /// </summary>
        /// <remarks>
        /// POST: api/AttractionsApi/upload/5
        /// Content-Type: multipart/form-data
        /// </remarks>
        [HttpPost("upload/{id}")]
        public async Task<IActionResult> UploadImage(int id, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Файл не передан");
            }

            var attraction = await _context.Attractions.FindAsync(id);
            if (attraction == null)
            {
                return NotFound($"Достопримечательность с Id={id} не найдена");
            }

            try
            {
                // Генерируем уникальное имя файла
                string extension = Path.GetExtension(file.FileName);
                string newFileName = $"{Guid.NewGuid()}{extension}";
                
                // Путь к папке wwwroot/Images
                string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
                
                // Создаём папку, если её нет
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                string fullPath = Path.Combine(uploadPath, newFileName);

                // Сохраняем файл
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Сохраняем путь в БД
                attraction.Image = $"/Images/{newFileName}";
                await _context.SaveChangesAsync();

                return Ok(new { success = true, imageUrl = attraction.Image });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка при загрузке файла: {ex.Message}");
            }
        }

        private bool AttractionExists(int id)
        {
            return _context.Attractions.Any(e => e.Id == id);
        }
    }
}