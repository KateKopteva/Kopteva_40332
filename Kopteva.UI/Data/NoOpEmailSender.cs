using Microsoft.AspNetCore.Identity;
using Tourist.Domain.Data;

namespace Kopteva.UI.Data
{
    /// <summary>
    /// Заглушка для отправки email (ничего не делает).
    /// Используется для имитации механизма подтверждения email.
    /// </summary>
    public class NoOpEmailSender : IEmailSender<ApplicationUser>
    {
        /// <summary>
        /// Отправка обычного письма
        /// </summary>
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Email не отправляется — просто помечаем как выполненный
            return Task.CompletedTask;
        }

        /// <summary>
        /// Отправка ссылки для подтверждения email
        /// </summary>
        public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
        {
            // Email не отправляется
            return Task.CompletedTask;
        }

        /// <summary>
        /// Отправка ссылки для сброса пароля
        /// </summary>
        public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
        {
            // Email не отправляется
            return Task.CompletedTask;
        }

        /// <summary>
        /// Отправка кода для сброса пароля
        /// </summary>
        public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
        {
            // Email не отправляется
            return Task.CompletedTask;
        }
    }
}