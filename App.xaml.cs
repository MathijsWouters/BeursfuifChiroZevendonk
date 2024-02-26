using Beursfuif.Views;
using Beursfuif.Models;
using System.Diagnostics;
using SharpHook.Native;
using SharpHook;

namespace Beursfuif
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
            
        }


        protected override void OnSleep()
        {

        }

    }
}
