using Kopteva.Blazor.Components;
using Kopteva.Blazor.Services;
using Kopteva.UI.Services;
using Tourist.Domain.Entities;
using Tourist.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Добавление сервисов Blazor
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Регистрация HttpClient для работы с API достопримечательностей
builder.Services.AddHttpClient<IAttractionService<Attraction>, ApiAttractionService>(c =>
{
    c.BaseAddress = new Uri("https://localhost:7281/api/AttractionsApi/");
}).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    // Для работы с самоподписанным сертификатом
    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
});

// Регистрация HttpClient для работы с API маршрутов
builder.Services.AddHttpClient<ITourRouteService<TourRoute>, ApiTourRouteService>(c =>
{
    c.BaseAddress = new Uri("https://localhost:7281/api/TourRoutesApi/");
}).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
});

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
