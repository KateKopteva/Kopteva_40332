namespace Kopteva.Blazor.Services
{
    /// <summary>
    /// Обобщённый интерфейс сервиса для работы с объектами через API
    /// </summary>
    public interface IAttractionService<T> where T : class
    {
        /// <summary>
        /// Событие, уведомляющее об изменении списка
        /// </summary>
        event Action ListChanged;

        /// <summary>
        /// Список объектов (текущая страница)
        /// </summary>
        IEnumerable<T> Products { get; }

        /// <summary>
        /// Номер текущей страницы
        /// </summary>
        int CurrentPage { get; }

        /// <summary>
        /// Общее количество страниц
        /// </summary>
        int TotalPages { get; }

        /// <summary>
        /// Получить список объектов с пагинацией
        /// </summary>
        /// <param name="pageNo">Номер страницы (начинается с 1)</param>
        /// <param name="pageSize">Размер страницы</param>
        Task GetProducts(int pageNo = 1, int pageSize = 3);
    }
}