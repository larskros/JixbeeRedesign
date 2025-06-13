using JixbeeRedesign.Components;
using JixbeeRedesign.Services;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using MudBlazor.Services;
using System.Globalization;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("nl-NL");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("nl-NL");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);
builder.Services.AddMudServices();

// Services
builder.Services.AddSingleton<NotificationService>();
builder.Services.AddSingleton<RecurringPaymentService>();
builder.Services.AddScoped<WithdrawStateService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
