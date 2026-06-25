
using Tourist.Domain.Entities;
using Tourist.Domain.Models;

namespace Kopteva.UI.Services
{
    /// <summary>
    /// Интерфейс сервиса для работы с туристическими маршрутами
    /// </summary>
    public interface ITourRouteService
    {
        /// <summary>
        /// Получение списка всех маршрутов
        /// </summary>
        Task<ResponseData<List<TourRoute>>> GetTourRouteListAsync();
    }
}