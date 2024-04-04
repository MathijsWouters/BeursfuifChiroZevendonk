using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeursfuifChiroZevendonk.ViewModels
{
    public partial class MainPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private bool _isBeursPageOpen;
        public ObservableCollection<Drink> Drinks { get; set; }
        public MainPageViewModel()
        {
            Drinks = new ObservableCollection<Drink>();
            // Initialize your commands here, and load drinks if needed
        }
        [RelayCommand]
        private async Task OpenBeursAsync()
        {
            if (_isBeursPageOpen)
            {
                bool response = await Shell.Current.DisplayAlert(
                    "Venster Open",
                    "Het Beurs venster lijkt al open te zijn. Staat het ergens anders open?",
                    "Ja",
                    "Nee"
                );

                if (response)
                {
                    // Gebruiker bevestigt dat het venster ergens open is, doe niets.
                    return;
                }
                else
                {
                    // Gebruiker zegt dat het venster niet open is, probeer opnieuw te openen.
                    _isBeursPageOpen = false;
                }
            }

            if (!_isBeursPageOpen)
            {
                var beursVm = new BeursPageViewModel();
                var beursPage = new BeursPage(beursVm);
                var newWindow = new Window(beursPage);
                Application.Current.OpenWindow(newWindow);
                _isBeursPageOpen = true;
            }
        }


        [RelayCommand]
        private async Task NavigateToAddDrink()
        {
            // Logic to navigate to the add drink page
        }

        [RelayCommand]
        private async Task NavigateToManageDrinks()
        {
            // Logic to navigate to the manage drinks page
        }

        [RelayCommand]
        private async Task StartFeestje()
        {
            // Logic to start "Feestje"
        }

        [RelayCommand]
        private async Task StopFeestje()
        {
            // Logic to stop "Feestje"
        }
    }
}
