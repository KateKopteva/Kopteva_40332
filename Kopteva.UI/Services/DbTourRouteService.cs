using Microsoft.EntityFrameworkCore;
using Tourist.Domain.Data;
using Tourist.Domain.Entities;
using Tourist.Domain.Interfaces;
using Tourist.Domain.Models;

namespace Kopteva.UI.Services
{
    /// <summary>
    /// Сервис для работы с туристическими маршрутами через базу данных
    /// </summary>
    public class DbTourRouteService : ITourRouteService
    {
        private readonly AppDbContext _context;

        public DbTourRouteService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получение списка всех маршрутов из базы данных
        /// </summary>
        public async Task<ResponseData<List<TourRoute>>> GetTourRouteListAsync()
        {
            try
            {
                // Делаем запрос к таблице TourRoutes в БД
                var routes = await _context.TourRoutes.ToListAsync();

                // Возвращаем успешный результат с данными
                return ResponseData<List<TourRoute>>.OK(routes);
            }
            catch (Exception ex)
            {
                // В случае ошибки возвращаем результат с флагом Success = false
                return ResponseData<List<TourRoute>>.Error(
                    $"Ошибка при получении списка маршрутов: {ex.Message}");
            }
        }
    }
}