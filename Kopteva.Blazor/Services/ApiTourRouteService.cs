using System.Net.Http.Json;
using System.Text.Json;
using Tourist.Domain.Entities;
using Tourist.Domain.Models;

namespace Kopteva.Blazor.Services
{
    public class ApiTourRouteService : ITourRouteService<TourRoute>
    {
        private readonly HttpClient _http;
        private readonly JsonSerializerOptions _jsonOptions;

        private List<TourRoute> _routes = new();
        private int _currentPage = 1;
        private int _totalPages = 1;

        public ApiTourRouteService(HttpClient http)
        {
            _http = http;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public IEnumerable<TourRoute> Products => _routes;
        public int CurrentPage => _currentPage;
        public int TotalPages => _totalPages;
        public event Action? ListChanged;

        public async Task GetProducts(int pageNo = 1, int pageSize = 3)
        {
            try
            {
                var response = await _http.GetAsync(_http.BaseAddress);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content
                        .ReadFromJsonAsync<ResponseData<List<TourRoute>>>(_jsonOptions);

                    if (responseData != null && responseData.Success && responseData.Data != null)
                    {
                        _totalPages = (int)Math.Ceiling(responseData.Data.Count / (double)pageSize);
                        _currentPage = pageNo;

                        _routes = responseData.Data
                            .Skip((pageNo - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();

                        ListChanged?.Invoke();
                    }
                    else
                    {
                        _routes = new List<TourRoute>();
                        _currentPage = 1;
                        _totalPages = 0;
                        ListChanged?.Invoke();
                    }
                }
                else
                {
                    _routes = new List<TourRoute>();
                    _currentPage = 1;
                    _totalPages = 0;
                    ListChanged?.Invoke();
                }
            }
            catch (Exception)
            {
                _routes = new List<TourRoute>();
                _currentPage = 1;
                _totalPages = 0;
                ListChanged?.Invoke();
            }
        }
    }
}