namespace Kopteva.UI.Models
{
    /// <summary>
    /// Модель для отображения страницы ошибок
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Идентификатор запроса (для отладки)
        /// </summary>
        public string? RequestId { get; set; }

        /// <summary>
        /// Флаг: отображать ли RequestId
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}