using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Hosting;
using SaveUp.Services;
using SaveUp.ViewModels;
using SaveUp.Views;
using SkiaSharp.Views.Maui.Controls.Hosting;  // ← für UseSkiaSharp()
using Microcharts.Maui;                      // ← für UseMicrocharts()


namespace SaveUp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()           // App-Typ
            .UseSkiaSharp()              // SkiaSharp-Renderer registrieren
            .UseMicrocharts()            // Microcharts registrieren
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Register Services
        builder.Services.AddSingleton<SavingService>();

        // Register ViewModels
        builder.Services.AddTransient<AddSavingViewModel>();
        builder.Services.AddTransient<SavingsListViewModel>();

        // Register Pages
        builder.Services.AddTransient<AddSavingPage>();
        builder.Services.AddTransient<SavingsListPage>();

        return builder.Build();
    }
}
