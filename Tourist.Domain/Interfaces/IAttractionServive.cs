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
        ResponseData<List<Attraction>> GetAll();

        /// <summary>
        /// Получить достопримечательность по Id
        /// </summary>
        ResponseData<Attraction> GetById(int id);

        /// <summary>
        /// Получить достопримечательности по маршруту
        /// </summary>
        ResponseData<List<Attraction>> GetByRoute(string routeNormalizedName);

        /// <summary>
        /// Получить общую стоимость посещения маршрута
        /// </summary>
        ResponseData<decimal> GetTotalCostByRoute(string routeNormalizedName);
    }
}
