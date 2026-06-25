using Microsoft.EntityFrameworkCore;
using Tourist.Domain.Entities;

namespace Tourist.Domain.Data
{
    /// <summary>
    /// Контекст базы данных для предметной области
    /// (достопримечательности и туристические маршруты)
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Таблицы для сущностей предметной области
        public DbSet<Attraction> Attractions { get; set; }
        public DbSet<TourRoute> TourRoutes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Настройка связи "Один-ко-многим": один маршрут → много достопримечательностей
            modelBuilder.Entity<Attraction>()
                .HasOne(a => a.TourRoute)
                .WithMany(r => r.Attractions)
                .HasForeignKey(a => a.TourRouteId)
                .OnDelete(DeleteBehavior.Cascade);

            // 2. Индексы для производительности
            modelBuilder.Entity<Attraction>()
                .HasIndex(a => a.TourRouteId);

            modelBuilder.Entity<TourRoute>()
                .HasIndex(r => r.NormalizedName)
                .IsUnique();

            // 3. Сидинг (начальные данные) - маршруты
            modelBuilder.Entity<TourRoute>().HasData(
                new TourRoute
                {
                    Id = 1,
                    Name = "Замки Беларуси",
                    NormalizedName = "zamki",
                    Description = "Путешествие по средневековым замкам страны"
                },
                new TourRoute
                {
                    Id = 2,
                    Name = "Минск и окрестности",
                    NormalizedName = "minsk",
                    Description = "Столица Беларуси и её достопримечательности"
                },
                new TourRoute
                {
                    Id = 3,
                    Name = "Беловежская пуща",
                    NormalizedName = "belovezhskaya-pushcha",
                    Description = "Древнейший лес Европы и национальный парк"
                },
                new TourRoute
                {
                    Id = 4,
                    Name = "Брестская крепость",
                    NormalizedName = "brest",
                    Description = "Мемориальный комплекс и крепость-герой"
                },
                new TourRoute
                {
                    Id = 5,
                    Name = "Полесье",
                    NormalizedName = "polesye",
                    Description = "Уникальный край болот, лесов и древних городов"
                }
            );

            // 4. Сидинг (начальные данные) - достопримечательности
            modelBuilder.Entity<Attraction>().HasData(
                // Замки Беларуси
                new Attraction
                {
                    Id = 1,
                    Name = "Мирский замок",
                    Description = "Замково-парковый комплекс XVI века, объект ЮНЕСКО",
                    TicketPrice = 15m,
                    TourRouteId = 1
                },
                new Attraction
                {
                    Id = 2,
                    Name = "Несвижский замок",
                    Description = "Дворцово-парковый комплекс рода Радзивиллов",
                    TicketPrice = 15m,
                    TourRouteId = 1
                },
                new Attraction
                {
                    Id = 3,
                    Name = "Лидский замок",
                    Description = "Средневековый замок XIV века",
                    TicketPrice = 7m,
                    TourRouteId = 1
                },
                new Attraction
                {
                    Id = 4,
                    Name = "Коссовский замок",
                    Description = "Неоготический дворец XIX века",
                    TicketPrice = 5m,
                    TourRouteId = 1
                },

                // Минск и окрестности
                new Attraction
                {
                    Id = 5,
                    Name = "Верхний город",
                    Description = "Исторический центр Минска с Ратушей",
                    TicketPrice = 0m,
                    TourRouteId = 2
                },
                new Attraction
                {
                    Id = 6,
                    Name = "Троицкое предместье",
                    Description = "Исторический район Минска",
                    TicketPrice = 0m,
                    TourRouteId = 2
                },
                new Attraction
                {
                    Id = 7,
                    Name = "Остров слёз",
                    Description = "Мемориальный комплекс воинам-интернационалистам",
                    TicketPrice = 0m,
                    TourRouteId = 2
                },

                // Беловежская пуща
                new Attraction
                {
                    Id = 8,
                    Name = "Беловежская пуща",
                    Description = "Древнейший лес Европы, объект ЮНЕСКО",
                    TicketPrice = 25m,
                    TourRouteId = 3
                },
                new Attraction
                {
                    Id = 9,
                    Name = "Музей природы",
                    Description = "Интерактивный музей о флоре и фауне",
                    TicketPrice = 10m,
                    TourRouteId = 3
                },
                new Attraction
                {
                    Id = 10,
                    Name = "Поместье Деда Мороза",
                    Description = "Резиденция белорусского Деда Мороза",
                    TicketPrice = 12m,
                    TourRouteId = 3
                },

                // Брестская крепость
                new Attraction
                {
                    Id = 11,
                    Name = "Брестская крепость-герой",
                    Description = "Мемориальный комплекс",
                    TicketPrice = 0m,
                    TourRouteId = 4
                },
                new Attraction
                {
                    Id = 12,
                    Name = "Музей обороны Брестской крепости",
                    Description = "Музей с экспонатами об обороне 1941 года",
                    TicketPrice = 8m,
                    TourRouteId = 4
                },

                // Полесье
                new Attraction
                {
                    Id = 13,
                    Name = "Пинск",
                    Description = "Один из старейших городов Беларуси",
                    TicketPrice = 5m,
                    TourRouteId = 5
                },
                new Attraction
                {
                    Id = 14,
                    Name = "Туров",
                    Description = "Древнейший город Беларуси X века",
                    TicketPrice = 3m,
                    TourRouteId = 5
                },
                new Attraction
                {
                    Id = 15,
                    Name = "Национальный парк Припятский",
                    Description = "Уникальный заповедник с болотами Полесья",
                    TicketPrice = 10m,
                    TourRouteId = 5
                }
            );
        }
    }
}