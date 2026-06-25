using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.Domain.Entities;
using Tourist.Domain.Models;
namespace Tourist.Domain.Interfaces


{
    /// <summary>
    /// Интерфейс сервиса для работы с достопримечательностями
    /// </summary>
    public interface IAttractionService
    {
        /// <summary>
        /// Получить все достопримечательности
        /// </summary>
        Task<ResponseData<List<Attraction>>> GetAll();

        /// <summary>
        /// Получить список достопримечательностей с фильтрацией по маршруту
        /// </summary>
        /// <param name="route">Нормализованное имя маршрута</param>
        Task<ResponseData<List<Attraction>>> GetAttractionListAsync(string? route);

        /// <summary>
        /// Получить достопримечательность по Id
        /// </summary>
        /// <param name="id">Идентификатор достопримечательности</param>
        Task<ResponseData<Attraction>> GetAttractionByIdAsync(int id);

        /// <summary>
        /// Обновить достопримечательность
        /// </summary>
        /// <param name="id">Id изменяемой достопримечательности</param>
        /// <param name="attraction">Объект с новыми параметрами</param>
        /// <param name="formFile">Файл изображения</param>
        Task UpdateAttractionAsync(int id, Attraction attraction, IFormFile? formFile);

        /// <summary>
        /// Удалить достопримечательность
        /// </summary>
        /// <param name="id">Id удаляемой достопримечательности</param>
        Task DeleteAttractionAsync(int id);

        /// <summary>
        /// Создать достопримечательность
        /// </summary>
        /// <param name="attraction">Новый объект</param>
        /// <param name="formFile">Файл изображения</param>
        Task<ResponseData<Attraction>> CreateAttractionAsync(Attraction attraction, IFormFile? formFile);
    }
}