using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Tourist.Domain.Data;  // ← ИЗМЕНИТЕ namespace

namespace Kopteva.UI.Data
{
    public class DbInit
    {
        public static async Task SetupIdentityAdmin(WebApplication application)
        {
            using var scope = application.Services.CreateScope();
            var services = scope.ServiceProvider;

            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            var user = await userManager.FindByEmailAsync("admin@gmail.com");

            if (user == null)
            {
                user = new ApplicationUser();
                await userManager.SetEmailAsync(user, "admin@gmail.com");
                await userManager.SetUserNameAsync(user, "admin@gmail.com");
                user.EmailConfirmed = true;

                var result = await userManager.CreateAsync(user, "123456");

                if (result.Succeeded)
                {
                    var roleClaim = new Claim(ClaimTypes.Role, "admin");
                    await userManager.AddClaimAsync(user, roleClaim);

                    var nameClaim = new Claim("Name", "Администратор");
                    await userManager.AddClaimAsync(user, nameClaim);
                }
            }
        }
    }
}