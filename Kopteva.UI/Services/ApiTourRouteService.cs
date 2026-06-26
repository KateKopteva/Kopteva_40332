using System.Net.Http.Json;
using System.Text.Json;
using Tourist.Domain.Entities;
using Tourist.Domain.Interfaces;
using Tourist.Domain.Models;

namespace Kopteva.UI.Services
{
    /// <summary>
    /// Сервис для работы с маршрутами через REST API
    /// </summary>
    public class ApiTourRouteService : ITourRouteService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public ApiTourRouteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<ResponseData<List<TourRoute>>> GetTourRouteListAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("");

                if (!response.IsSuccessStatusCode)
                {
                    return ResponseData<List<TourRoute>>.Error(
                        $"Ошибка HTTP: {response.StatusCode}");
                }

                var result = await response.Content.ReadFromJsonAsync<ResponseData<List<TourRoute>>>(_jsonOptions);

                return result ?? ResponseData<List<TourRoute>>.Error("Пустой ответ от API");
            }
            catch (Exception ex)
            {
                return ResponseData<List<TourRoute>>.Error(
                    $"Ошибка при получении маршрутов: {ex.Message}");
            }
        }
    }
}