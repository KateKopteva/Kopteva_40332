using Kopteva.UI.Data;
using Kopteva.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Tourist.Domain.Data;
using Tourist.Domain.Interfaces;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ============================================
// 1. РЕГИСТРАЦИЯ СЕРВИСОВ
// ============================================
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Игнорировать циклические ссылки в JSON
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;

        // Форматировать JSON (для удобства отладки)
        options.JsonSerializerOptions.WriteIndented = true;
    });

// Swagger (OpenAPI)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Tourist API",
        Version = "v1",
        Description = "REST API для работы с достопримечательностями и маршрутами"
    });
});

// ============================================
// 2. СТРОКА ПОДКЛЮЧЕНИЯ
// ============================================
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// ============================================
// 3. КОНТЕКСТ IDENTITY
// ============================================
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// ============================================
// 4. IDENTITY
// ============================================
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 4;
})
.AddEntityFrameworkStores<ApplicationDbContext>();

// ============================================
// 5. ПОЛИТИКА "admin"
// ============================================
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("admin", policy =>
        policy.RequireClaim(ClaimTypes.Role, "admin"));
});

// ============================================
// 6. NoOpEmailSender
// ============================================
builder.Services.AddSingleton<IEmailSender<ApplicationUser>, NoOpEmailSender>();

// ============================================
// 7. КОНТЕКСТ ПРЕДМЕТНОЙ ОБЛАСТИ
// ============================================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        connectionString,
        sqlOptions => sqlOptions.MigrationsAssembly("Tourist.Domain")
    ));

// ============================================
// 8. СЕРВИСЫ ПРЕДМЕТНОЙ ОБЛАСТИ
// ============================================
builder.Services.AddScoped<ITourRouteService, DbTourRouteService>();
builder.Services.AddScoped<IAttractionService, DbAttractionService>();

// ============================================
// ПОСТРОЕНИЕ ПРИЛОЖЕНИЯ
// ============================================
var app = builder.Build();

// Создание БД
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var identityContext = services.GetRequiredService<ApplicationDbContext>();
        identityContext.Database.Migrate();

        var domainContext = services.GetRequiredService<AppDbContext>();
        domainContext.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ошибка при создании базы данных.");
    }
}

// Сидинг администратора
await DbInit.SetupIdentityAdmin(app);

// ============================================
// MIDDLEWARE
// ============================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tourist API v1");
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// ============================================
// МАРШРУТЫ
// ============================================
app.MapControllers();  // ← ВАЖНО для API контроллеров!
app.MapRazorPages();

app.MapAreaControllerRoute(
    name: "Admin",
    areaName: "Admin",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");

app.Run();