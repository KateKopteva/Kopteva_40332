using Microsoft.AspNetCore.Http;
using Tourist.Domain.Entities;
using Tourist.Domain.Models;

namespace Kopteva.UI.Services
{

    /// Интерфейс сервиса для работы с достопримечательностями

    public interface IAttractionService
    {
        /// Получение списка всех достопримечательностей
        /// <param name="route">нормализованное имя маршрута для фильтрации</param>
        Task<ResponseData<List<Attraction>>> GetAttractionListAsync(string? route);

 
        /// Поиск достопримечательности по Id
        Task<ResponseData<Attraction>> GetAttractionByIdAsync(int id);

        /// Обновление достопримечательности
        Task UpdateAttractionAsync(int id, Attraction attraction, IFormFile? formFile);

        /// Удаление достопримечательности
        Task DeleteAttractionAsync(int id);

        /// Создание достопримечательности
        Task<ResponseData<Attraction>> CreateAttractionAsync(Attraction attraction, IFormFile? formFile);
    }
}