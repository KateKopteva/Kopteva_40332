using System.Net.Http.Json;
using System.Text.Json;
using Tourist.Domain.Entities;
using Tourist.Domain.Models;

namespace Kopteva.Blazor.Services
{
    /// <summary>
    /// Сервис для получения данных о достопримечательностях через REST API
    /// </summary>
    public class ApiAttractionService : IAttractionService<Attraction>
    {
        private readonly HttpClient _http;
        private readonly JsonSerializerOptions _jsonOptions;

        private List<Attraction> _attractions = new();
        private int _currentPage = 1;
        private int _totalPages = 1;

        public ApiAttractionService(HttpClient http)
        {
            _http = http;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
            };
        }

        /// <summary>
        /// Список достопримечательностей текущей страницы
        /// </summary>
        public IEnumerable<Attraction> Products => _attractions;

        /// <summary>
        /// Номер текущей страницы
        /// </summary>
        public int CurrentPage => _currentPage;

        /// <summary>
        /// Общее количество страниц
        /// </summary>
        public int TotalPages => _totalPages;

        /// <summary>
        /// Событие, уведомляющее об изменении списка
        /// </summary>
        public event Action? ListChanged;

        /// <summary>
        /// Получить список достопримечательностей с пагинацией
        /// </summary>
        public async Task GetProducts(int pageNo = 1, int pageSize = 3)
        {
            try
            {
                // Отправляем запрос к API
                var response = await _http.GetAsync(_http.BaseAddress);

                if (response.IsSuccessStatusCode)
                {
                    // Получаем ResponseData<List<Attraction>>
                    var responseData = await response.Content
                        .ReadFromJsonAsync<ResponseData<List<Attraction>>>(_jsonOptions);

                    if (responseData != null && responseData.Success && responseData.Data != null)
                    {
                        // Рассчитываем количество страниц
                        _totalPages = (int)Math.Ceiling(responseData.Data.Count / (double)pageSize);
                        _currentPage = pageNo;

                        // Получаем нужную страницу
                        _attractions = responseData.Data
                            .Skip((pageNo - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();

                        // Уведомляем подписчиков об изменении
                        ListChanged?.Invoke();
                    }
                    else
                    {
                        _attractions = new List<Attraction>();
                        _currentPage = 1;
                        _totalPages = 0;
                        ListChanged?.Invoke();
                    }
                }
                else
                {
                    _attractions = new List<Attraction>();
                    _currentPage = 1;
                    _totalPages = 0;
                    ListChanged?.Invoke();
                }
            }
            catch (Exception ex)
            {
                _attractions = new List<Attraction>();
                _currentPage = 1;
                _totalPages = 0;
                ListChanged?.Invoke();
            }
        }
    }
}