using Beursfuif.Views;
using Beursfuif.Models;
using System.Diagnostics;
using SharpHook.Native;
using SharpHook;

namespace Beursfuif
{
    public partial class App : Application
    {
#if WINDOWS
        private KeyboardService keyboardService;
#endif
#if WINDOWS
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
            try
            {

                this.keyboardService = new KeyboardService();
                this.keyboardService.Start();
               Debug.WriteLine("KeyboardService started.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception initializing KeyboardService: {ex}");

            }
        }
#endif

        protected override void OnSleep()
        {

#if WINDOWS
            this.keyboardService?.Stop();
            base.OnSleep();
#endif

        }

    }
}
