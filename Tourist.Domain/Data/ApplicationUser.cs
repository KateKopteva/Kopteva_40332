using Microsoft.AspNetCore.Identity;

namespace Tourist.Domain.Data
{
    /// <summary>
    /// Пользователь приложения с расширенными свойствами
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Аватар пользователя (в виде массива байт)
        /// </summary>
        public byte[]? Avatar { get; set; }
    }
}