using Microsoft.Extensions.Logging;

namespace BeursfuifChiroZevendonk
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            //Repositories

            //ViewModels
            builder.Services.AddSingleton<TemplateViewModel>();
            builder.Services.AddSingleton<DrinksViewModel>();
            builder.Services.AddSingleton<MainPageViewModel>();

            //views
            //builder.Services.AddSingleton<ViewName>();
            builder.Services.AddSingleton<BeursPage>();
            builder.Services.AddSingleton<MainPage>();

            return builder.Build();
        }
    }
}
