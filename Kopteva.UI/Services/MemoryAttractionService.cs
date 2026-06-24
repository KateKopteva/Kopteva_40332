using Microsoft.AspNetCore.Http;
using Tourist.Domain.Entities;
using Tourist.Domain.Models;

namespace Kopteva.UI.Services
{
    /// <summary>
    /// Сервис, имитирующий работу с достопримечательностями (in-memory)
    /// </summary>
    public class MemoryAttractionService : IAttractionService
    {
        List<Attraction> _attractions;
        List<TourRoute> _routes;

        public MemoryAttractionService(ITourRouteService routeService)
        {
            _routes = routeService.GetTourRouteListAsync().Result.Data!;
            SetupData();
        }


        /// Инициализация списков
        private void SetupData()
        {
            _attractions = new List<Attraction>
            {
                // === Замки Беларуси ===
        new Attraction
        {
            Id = 1,
            Name = "Мирский замок",
            Description = "Замково-парковый комплекс XVI века, объект Всемирного наследия ЮНЕСКО. Один из самых известных замков Беларуси.",
            TicketPrice = 15m,
            Image = "Images/mir-zamok.jpg",
            TourRouteId = _routes.Find(r => r.NormalizedName.Equals("zamki"))!.Id
        },
        new Attraction
        {
            Id = 2,
            Name = "Несвижский замок",
            Description = "Дворцово-парковый комплекс рода Радзивиллов XVI-XVIII веков. Объект Всемирного наследия ЮНЕСКО.",
            TicketPrice = 15m,
            Image = "Images/nesvizh.jpg",
            TourRouteId = _routes.Find(r => r.NormalizedName.Equals("zamki"))!.Id
        },
        new Attraction
        {
            Id = 3,
            Name = "Лидский замок",
            Description = "Средневековый замок XIV века, построенный князем Гедимином. Один из старейших замков Беларуси.",
            TicketPrice = 7m,
            Image = "Images/lida-zamok.jpg",
            TourRouteId = _routes.Find(r => r.NormalizedName.Equals("zamki"))!.Id
        },
        new Attraction
        {
            Id = 4,
            Name = "Коссовский замок (Дворец Пусловских)",
            Description = "Неоготический дворец XIX века, окружённый живописным парком. Реставрирован в начале XXI века.",
            TicketPrice = 5m,
            Image = "Images/kossovo.jpg",
            TourRouteId = _routes.Find(r => r.NormalizedName.Equals("zamki"))!.Id
        },

        // === Минск и окрестности ===
        new Attraction
        {
            Id = 5,
            Name = "Верхний город (Минск)",
            Description = "Исторический центр Минска с Ратушей, гостиным двором и старинными улочками. Сердце столицы.",
            TicketPrice = 0m,
            Image = "Images/verhniy-gorod.jpg",
            TourRouteId = _routes.Find(r => r.NormalizedName.Equals("minsk"))!.Id
        },
        new Attraction
        {
            Id = 6,
            Name = "Троицкое предместье",
            Description = "Исторический район Минска с уникальной застройкой XVIII-XIX веков. Музей под открытым небом.",
            TicketPrice = 0m,
            Image = "Images/troitskoe.jpg",
            TourRouteId = _routes.Find(r => r.NormalizedName.Equals("minsk"))!.Id
        },
        new Attraction
        {
            Id = 7,
            Name = "Остров слёз (Минск)",
            Description = "Мемориальный комплекс в центре Минска, посвящённый воинам-интернационалистам.",
            TicketPrice = 0m,
            Image = "Images/ostrov-slyoz.jpg",
            TourRouteId = _routes.Find(r => r.NormalizedName.Equals("minsk"))!.Id
        },

        // === Беловежская пуща ===
        new Attraction
        {
            Id = 8,
            Name = "Беловежская пуща",
            Description = "Древнейший лес Европы, объект Всемирного наследия ЮНЕСКО. Здесь обитают зубры — символ Беларуси.",
            TicketPrice = 25m,
            Image = "Images/pushcha.jpg",
            TourRouteId = _routes.Find(r => r.NormalizedName.Equals("belovezhskaya-pushcha"))!.Id
        },
        new Attraction
        {
            Id = 9,
            Name = "Музей природы Беловежской пущи",
            Description = "Интерактивный музей с экспозициями о флоре и фауне древнего леса. Отличный выбор для семейного посещения.",
            TicketPrice = 10m,
            Image = "Images/muzej-prirody.jpg",
            TourRouteId = _routes.Find(r => r.NormalizedName.Equals("belovezhskaya-pushcha"))!.Id
        },
        new Attraction
        {
            Id = 10,
            Name = "Поместье Деда Мороза",
            Description = "Сказочная резиденция белорусского Деда Мороза в Беловежской пуще. Работает круглый год.",
            TicketPrice = 12m,
            Image = "Images/ded-moroz.jpg",
            TourRouteId = _routes.Find(r => r.NormalizedName.Equals("belovezhskaya-pushcha"))!.Id
        },

        // === Брестская крепость ===
        new Attraction
        {
            Id = 11,
            Name = "Брестская крепость-герой",
            Description = "Мемориальный комплекс, посвящённый защитникам крепости в 1941 году. Символ мужества и стойкости.",
            TicketPrice = 0m,
            Image = "Images/brestskaya-krepost.jpg",
            TourRouteId = _routes.Find(r => r.NormalizedName.Equals("brest"))!.Id
        },
        new Attraction
        {
            Id = 12,
            Name = "Музей обороны Брестской крепости",
            Description = "Музей с уникальными экспонатами и документами о героической обороне крепости летом 1941 года.",
            TicketPrice = 8m,
            Image = "Images/muzej-oborony.jpg",
            TourRouteId = _routes.Find(r => r.NormalizedName.Equals("brest"))!.Id
        },

        // === Полесье ===
        new Attraction
        {
            Id = 13,
            Name = "Пинск — столица Полесья",
            Description = "Один из старейших городов Беларуси с богатой историей. Иезуитский коллегиум, дворец Бутримовича.",
            TicketPrice = 5m,
            Image = "Images/pinsk.jpg",
            TourRouteId = _routes.Find(r => r.NormalizedName.Equals("polesye"))!.Id
        },
        new Attraction
        {
            Id = 14,
            Name = "Туров — древняя столица",
            Description = "Один из древнейших городов Беларуси, упомянутый в летописях X века. Туровские кресты — уникальная реликвия.",
            TicketPrice = 3m,
            Image = "Images/turov.jpg",
            TourRouteId = _routes.Find(r => r.NormalizedName.Equals("polesye"))!.Id
        },
        new Attraction
        {
            Id = 15,
            Name = "Национальный парк Припятский",
            Description = "Уникальный заповедник с болотами Полесья, редкими птицами и первобытными лесами.",
            TicketPrice = 10m,
            Image = "Images/pripyatsky.jpg",
            TourRouteId = _routes.Find(r => r.NormalizedName.Equals("polesye"))!.Id
        }
            };
        }

        public Task<ResponseData<List<Attraction>>> GetAttractionListAsync(string? route)
        {
            ResponseData<List<Attraction>> result;

            // Id маршрута для фильтрации
            int? routeId = null;

            // Если требуется фильтрация, найти Id маршрута
            if (route != null)
            {
                routeId = _routes
                    .Find(r => r.NormalizedName.Equals(route))
                    ?.Id;
            }

            // Выбрать объекты, отфильтрованные по Id маршрута
            var data = _attractions
                .Where(a => routeId == null || a.TourRouteId.Equals(routeId))
                .ToList();

            // Если список пустой
            if (data.Count == 0)
            {
                result = ResponseData<List<Attraction>>.Error("Нет достопримечательностей в выбранном маршруте");
            }
            else
            {
                result = ResponseData<List<Attraction>>.OK(data);
            }

            return Task.FromResult(result);
        }

        public Task<ResponseData<Attraction>> GetAttractionByIdAsync(int id)
        {
            try
            {
                var attraction = _attractions.FirstOrDefault(a => a.Id == id);
                if (attraction == null)
                {
                    return Task.FromResult(ResponseData<Attraction>.Error(
                        $"Достопримечательность с Id={id} не найдена"));
                }
                return Task.FromResult(ResponseData<Attraction>.OK(attraction));
            }
            catch (Exception ex)
            {
                return Task.FromResult(ResponseData<Attraction>.Error(
                    $"Ошибка при получении: {ex.Message}"));
            }
        }

        public Task UpdateAttractionAsync(int id, Attraction attraction, IFormFile? formFile)
        {
            try
            {
                var existing = _attractions.FirstOrDefault(a => a.Id == id);
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
                }
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                return Task.CompletedTask;
            }
        }

        public Task DeleteAttractionAsync(int id)
        {
            try
            {
                var attraction = _attractions.FirstOrDefault(a => a.Id == id);
                if (attraction != null)
                {
                    _attractions.Remove(attraction);
                }
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                return Task.CompletedTask;
            }
        }

        public Task<ResponseData<Attraction>> CreateAttractionAsync(Attraction attraction, IFormFile? formFile)
        {
            try
            {
                attraction.Id = _attractions.Max(a => a.Id) + 1;

                if (formFile != null && formFile.Length > 0)
                {
                    attraction.Image = $"Images/{formFile.FileName}";
                }

                _attractions.Add(attraction);
                return Task.FromResult(ResponseData<Attraction>.OK(attraction));
            }
            catch (Exception ex)
            {
                return Task.FromResult(ResponseData<Attraction>.Error(
                    $"Ошибка при создании: {ex.Message}"));
            }
        }
    }
}