using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.LifecycleEvents;
using SkiaSharp.Views.Maui.Controls.Hosting;

#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using WinRT.Interop; // Required to get the window handle
#endif

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

            // Service Registrations
            builder.Services.AddSingleton<DrinksDataService>();

            // Logging in Debug mode
#if DEBUG
            builder.Logging.AddDebug();
#endif

            // Repositories (if any can be added here)

            // ViewModels
            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<BeursPageViewModel>();
            builder.Services.AddTransient<AddDrinkPageViewModel>();
            builder.Services.AddTransient<EditDrinkPageViewModel>();
            builder.Services.AddSingleton<ManageDrinksPageViewModel>();
            builder.Services.AddSingleton<SettingsPageViewModel>();

            // Views
            builder.Services.AddSingleton<BeursPage>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransient<AddDrinkPage>();
            builder.Services.AddTransient<EditDrinkPage>();
            builder.Services.AddSingleton<ManageDrinksPage>();
            builder.Services.AddSingleton<SettingsPage>();

            // Fullscreen configuration for Windows
            builder.ConfigureLifecycleEvents(events =>
            {
#if WINDOWS
                events.AddWindows(w =>
                {
                    w.OnWindowCreated(window =>
                    {
                        // Optional: Extend content into title bar (hides minimize/maximize/close buttons)
                        window.ExtendsContentIntoTitleBar = true;

                        // Get window handle (hWnd) and apply fullscreen
                        IntPtr hWnd = WindowNative.GetWindowHandle(window);
                        WindowId windowId = Win32Interop.GetWindowIdFromWindow(hWnd);
                        AppWindow appWindow = AppWindow.GetFromWindowId(windowId);

                        // Set the window to fullscreen
                        appWindow.SetPresenter(AppWindowPresenterKind.FullScreen);
                    });
                });
#endif
            });

            return builder.Build();
        }
    }
}
