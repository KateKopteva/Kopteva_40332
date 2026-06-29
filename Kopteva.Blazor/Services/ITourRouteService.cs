namespace Kopteva.Blazor.Services
{
    /// <summary>
    /// Обобщённый интерфейс сервиса для работы с маршрутами
    /// </summary>
    public interface ITourRouteService<T> where T : class
    {
        event Action ListChanged;
        IEnumerable<T> Products { get; }
        int CurrentPage { get; }
        int TotalPages { get; }
        Task GetProducts(int pageNo = 1, int pageSize = 3);
    }
}