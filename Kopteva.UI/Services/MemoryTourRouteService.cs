using Tourist.Domain.Entities;
using Tourist.Domain.Models;

namespace Kopteva.UI.Services
{
    /// <summary>
    /// Сервис, имитирующий работу с белорусскими туристическими маршрутами
    /// </summary>
    public class MemoryTourRouteService : ITourRouteService
    {
        public Task<ResponseData<List<TourRoute>>> GetTourRouteListAsync()
        {
            var routes = new List<TourRoute>
            {
                new TourRoute
                {
                    Id = 1,
                    Name = "Замки Беларуси",
                    NormalizedName = "zamki",
                    Description = "Путешествие по средневековым замкам страны",
                    Image = "Images/zamki.jpg"
                },
                new TourRoute
                {
                    Id = 2,
                    Name = "Минск и окрестности",
                    NormalizedName = "minsk",
                    Description = "Столица Беларуси и её достопримечательности",
                    Image = "Images/minsk.jpg"
                },
                new TourRoute
                {
                    Id = 3,
                    Name = "Беловежская пуща",
                    NormalizedName = "belovezhskaya-pushcha",
                    Description = "Древнейший лес Европы и национальный парк",
                    Image = "Images/pushcha.jpg"
                },
                new TourRoute
                {
                    Id = 4,
                    Name = "Брестская крепость",
                    NormalizedName = "brest",
                    Description = "Мемориальный комплекс и крепость-герой",
                    Image = "Images/brest.jpg"
                },
                new TourRoute
                {
                    Id = 5,
                    Name = "Полесье",
                    NormalizedName = "polesye",
                    Description = "Уникальный край болот, лесов и древних городов",
                    Image = "Images/polesye.jpg"
                }
            };

            var result = new ResponseData<List<TourRoute>>();
            result.Data = routes;
            return Task.FromResult(result);
        }
    }
}