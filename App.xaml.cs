using Beursfuif.Views;
using Beursfuif.Models;

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
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception initializing KeyboardService: {ex.Message}");

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
