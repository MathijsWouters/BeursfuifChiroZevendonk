using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace BeursfuifChiroZevendonk
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder

                .UseMauiApp<App>()
                .UseSkiaSharp()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            //service
            builder.Services.AddSingleton<DrinksDataService>();
#if DEBUG
            builder.Logging.AddDebug();
#endif
            //Repositories

            //ViewModels
            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<BeursPageViewModel>();
            builder.Services.AddTransient<AddDrinkPageViewModel>();

            //views
            //builder.Services.AddSingleton<ViewName>();
            builder.Services.AddSingleton<BeursPage>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransient<AddDrinkPage>();


            

            return builder.Build();
        }
    }
}
