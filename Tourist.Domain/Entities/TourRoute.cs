using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Tourist.Domain.Entities
{
    public class TourRoute
    {
        
        /// Уникальный идентификатор маршрута
        public int Id { get; set; }

       
        /// Название маршрута
        [Required(ErrorMessage = "Название маршрута обязательно")]
        [StringLength(150, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;
        
        /// Описание маршрута (протяжённость, длительность, особенности)
        public string? Description { get; set; }
        
        /// Имя маршрута в нотации kebab-case для URL
        
        [Required(ErrorMessage = "Нормализованное имя обязательно")]
        [RegularExpression(@"^[a-z0-9-]+$",
            ErrorMessage = "Имя должно содержать только строчные буквы, цифры и дефисы")]
        public string NormalizedName { get; set; } = string.Empty;

        /// Путь к изображению маршрута (фото-обложка)
        public string? Image { get; set; }


        /// Навигационное свойство: список достопримечательностей маршрута
        /// (отношение "один-ко-многим")
        public ICollection<Attraction> Attractions { get; set; } = new List<Attraction>();
    }
}
