using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Tourist.Domain.Data;
using Tourist.Domain.Entities;
using Tourist.Domain.Interfaces;
using Tourist.Domain.Models;

namespace Kopteva.UI.Services
{
    public class DbAttractionService : IAttractionService
    {
        private readonly AppDbContext _context;

        public DbAttractionService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получить все достопримечательности
        /// </summary>
        public async Task<ResponseData<List<Attraction>>> GetAll()
        {
            try
            {
                var attractions = await _context.Attractions
                    .Include(a => a.TourRoute)
                    .ToListAsync();

                return ResponseData<List<Attraction>>.OK(attractions);
            }
            catch (Exception ex)
            {
                return ResponseData<List<Attraction>>.Error(
                    $"Ошибка при получении списка: {ex.Message}");
            }
        }

        /// <summary>
        /// Получить список достопримечательностей с фильтрацией по маршруту
        /// </summary>
        public async Task<ResponseData<List<Attraction>>> GetAttractionListAsync(string? route)
        {
            try
            {
                IQueryable<Attraction> query = _context.Attractions
                    .Include(a => a.TourRoute);

                if (!string.IsNullOrEmpty(route))
                {
                    query = query.Where(a => a.TourRoute.NormalizedName == route);
                }

                var attractions = await query.ToListAsync();

                if (attractions.Count == 0)
                {
                    return ResponseData<List<Attraction>>.Error(
                        "Нет достопримечательностей в выбранном маршруте");
                }

                return ResponseData<List<Attraction>>.OK(attractions);
            }
            catch (Exception ex)
            {
                return ResponseData<List<Attraction>>.Error(
                    $"Ошибка при получении списка: {ex.Message}");
            }
        }

        /// <summary>
        /// Получить достопримечательность по Id
        /// </summary>
        public async Task<ResponseData<Attraction>> GetAttractionByIdAsync(int id)
        {
            try
            {
                var attraction = await _context.Attractions
                    .Include(a => a.TourRoute)
                    .FirstOrDefaultAsync(a => a.Id == id);

                if (attraction == null)
                {
                    return ResponseData<Attraction>.Error(
                        $"Достопримечательность с Id={id} не найдена");
                }

                return ResponseData<Attraction>.OK(attraction);
            }
            catch (Exception ex)
            {
                return ResponseData<Attraction>.Error(
                    $"Ошибка при получении: {ex.Message}");
            }
        }

        /// <summary>
        /// Обновить достопримечательность
        /// </summary>
        public async Task UpdateAttractionAsync(int id, Attraction attraction, IFormFile? formFile)
        {
            try
            {
                var existing = await _context.Attractions.FindAsync(id);
                if (existing != null)
                {
                    existing.Name = attraction.Name;
                    existing.Description = attraction.Description;
                    existing.TicketPrice = attraction.TicketPrice;
                    existing.TourRouteId = attraction.TourRouteId;

                    if (formFile != null && formFile.Length > 0)
                    {
                        existing.Image = $"Images/{formFile.FileName}";
                    }

                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при обновлении: {ex.Message}");
            }
        }

        /// <summary>
        /// Удалить достопримечательность
        /// </summary>
        public async Task DeleteAttractionAsync(int id)
        {
            try
            {
                var attraction = await _context.Attractions.FindAsync(id);
                if (attraction != null)
                {
                    _context.Attractions.Remove(attraction);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при удалении: {ex.Message}");
            }
        }

        /// <summary>
        /// Создать достопримечательность
        /// </summary>
        public async Task<ResponseData<Attraction>> CreateAttractionAsync(Attraction attraction, IFormFile? formFile)
        {
            try
            {
                if (formFile != null && formFile.Length > 0)
                {
                    attraction.Image = $"Images/{formFile.FileName}";
                }

                _context.Attractions.Add(attraction);
                await _context.SaveChangesAsync();

                return ResponseData<Attraction>.OK(attraction);
            }
            catch (Exception ex)
            {
                return ResponseData<Attraction>.Error(
                    $"Ошибка при создании: {ex.Message}");
            }
        }
    }
}