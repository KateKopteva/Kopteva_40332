using System.Net.Http.Json;
using System.Text.Json;
using Tourist.Domain.Entities;
using Tourist.Domain.Interfaces;
using Tourist.Domain.Models;

namespace Kopteva.UI.Services
{
    /// <summary>
    /// Сервис для работы с достопримечательностями через REST API
    /// </summary>
    public class ApiAttractionService : IAttractionService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public ApiAttractionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        /// <summary>
        /// Получить все достопримечательности
        /// </summary>
        public async Task<ResponseData<List<Attraction>>> GetAll()
        {
            return await GetAttractionListAsync(null);
        }

        /// <summary>
        /// Получить список достопримечательностей с фильтрацией по маршруту
        /// </summary>
        public async Task<ResponseData<List<Attraction>>> GetAttractionListAsync(string? route)
        {
            try
            {
                string url = string.IsNullOrEmpty(route)
                    ? ""
                    : $"?category={route}";

                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return ResponseData<List<Attraction>>.Error(
                        $"Ошибка HTTP: {response.StatusCode}");
                }

                var result = await response.Content.ReadFromJsonAsync<ResponseData<List<Attraction>>>(_jsonOptions);

                return result ?? ResponseData<List<Attraction>>.Error("Пустой ответ от API");
            }
            catch (Exception ex)
            {
                return ResponseData<List<Attraction>>.Error(
                    $"Ошибка при получении списка: {ex.Message}");
            }
        }

        /// <summary>
        /// Получить достопримечательность по Id
        /// </summary>
        public async Task<ResponseData<Attraction>> GetAttractionByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{id}");

                if (!response.IsSuccessStatusCode)
                {
                    return ResponseData<Attraction>.Error(
                        $"Ошибка HTTP: {response.StatusCode}");
                }

                var result = await response.Content.ReadFromJsonAsync<ResponseData<Attraction>>(_jsonOptions);

                return result ?? ResponseData<Attraction>.Error("Пустой ответ от API");
            }
            catch (Exception ex)
            {
                return ResponseData<Attraction>.Error(
                    $"Ошибка при получении: {ex.Message}");
            }
        }

        /// <summary>
        /// Обновить достопримечательность
        /// </summary>
        public async Task UpdateAttractionAsync(int id, Attraction attraction, IFormFile? formFile)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{id}", attraction);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Ошибка HTTP: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при обновлении: {ex.Message}");
            }
        }

        /// <summary>
        /// Удалить достопримечательность
        /// </summary>
        public async Task DeleteAttractionAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{id}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Ошибка HTTP: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при удалении: {ex.Message}");
            }
        }

        /// <summary>
        /// Создать достопримечательность (Задание 9)
        /// </summary>
        public async Task<ResponseData<Attraction>> CreateAttractionAsync(Attraction attraction, IFormFile? formFile)
        {
            try
            {
                // 1. Отправляем запрос на создание объекта
                var response = await _httpClient.PostAsJsonAsync("", attraction);

                if (!response.IsSuccessStatusCode)
                {
                    return ResponseData<Attraction>.Error(
                        $"Ошибка HTTP при создании: {response.StatusCode}");
                }

                var result = await response.Content.ReadFromJsonAsync<ResponseData<Attraction>>(_jsonOptions);

                if (result == null || !result.Success || result.Data == null)
                {
                    return ResponseData<Attraction>.Error("Ошибка при создании объекта");
                }

                // 2. Если был передан файл, загружаем его
                if (formFile != null && formFile.Length > 0)
                {
                    var uploadResult = await UploadImageAsync(result.Data.Id, formFile);

                    if (!uploadResult)
                    {
                        return ResponseData<Attraction>.Error("Объект создан, но изображение не загружено");
                    }

                    // Обновляем путь к изображению
                    result.Data.Image = $"/Images/{formFile.FileName}";
                }

                return ResponseData<Attraction>.OK(result.Data);
            }
            catch (Exception ex)
            {
                return ResponseData<Attraction>.Error(
                    $"Ошибка при создании: {ex.Message}");
            }
        }

        /// <summary>
        /// Загрузить изображение для достопримечательности
        /// </summary>
        private async Task<bool> UploadImageAsync(int id, IFormFile file)
        {
            try
            {
                using var content = new MultipartFormDataContent();

                // Добавляем файл
                var fileContent = new StreamContent(file.OpenReadStream());
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                content.Add(fileContent, "file", file.FileName);

                var response = await _httpClient.PostAsync($"upload/{id}", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}