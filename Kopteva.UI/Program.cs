using Kopteva.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tourist.Domain.Data;
using Tourist.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// ============================================
// 1. РЕГИСТРАЦИЯ СЕРВИСОВ
// ============================================
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// --------------------------------------------
// 2. СТРОКА ПОДКЛЮЧЕНИЯ
// --------------------------------------------
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// --------------------------------------------
// 3. КОНТЕКСТ ДЛЯ IDENTITY (ApplicationDbContext)
// --------------------------------------------
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// --------------------------------------------
// 4. КОНТЕКСТ ДЛЯ ПРЕДМЕТНОЙ ОБЛАСТИ (AppDbContext)
// --------------------------------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        connectionString,
        sqlOptions => sqlOptions.MigrationsAssembly("Tourist.Domain")
    ));

// --------------------------------------------
// 5. РЕГИСТРАЦИЯ СЕРВИСОВ ПРЕДМЕТНОЙ ОБЛАСТИ
// --------------------------------------------
builder.Services.AddScoped<ITourRouteService, DbTourRouteService>();
builder.Services.AddScoped<IAttractionService, DbAttractionService>();

// ============================================
// 6. ПОСТРОЕНИЕ ПРИЛОЖЕНИЯ
// ============================================
var app = builder.Build();

// Создание/обновление БД при запуске
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // Создаём/обновляем БД для Identity
        var identityContext = services.GetRequiredService<ApplicationDbContext>();
        identityContext.Database.Migrate();

        // Создаём/обновляем БД для предметной области
        var domainContext = services.GetRequiredService<AppDbContext>();
        domainContext.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ошибка при создании/обновлении базы данных.");
    }
}

// ============================================
// 7. MIDDLEWARE
// ============================================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// ============================================
// 8. МАРШРУТЫ
// ============================================
app.MapRazorPages();

app.MapAreaControllerRoute(
    name: "Admin",
    areaName: "Admin",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();