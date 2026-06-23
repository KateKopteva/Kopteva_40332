using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tourist.Domain.Entities
{
    public class Attraction
    {
        public int Id { get; set; }

        /// Название достопримечательности

        [Required(ErrorMessage = "Название достопримечательности обязательно")]
        [StringLength(150, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        /// Описание достопримечательности
        public string? Description { get; set; }

        /// Внешний ключ на туристический маршрут
        public int TourRouteId { get; set; }


        /// Навигационное свойство: маршрут, к которому относится достопримечательность
        [ForeignKey("TourRouteId")]
        public TourRoute? TourRoute { get; set; }


        /// Стоимость входного билета (в рублях)
        [Range(0, 100000, ErrorMessage = "Стоимость должна быть от 0 до 100000 руб.")]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal TicketPrice { get; set; }


        /// Путь к файлу изображения достопримечательности
        public string? Image { get; set; }
    }
}
