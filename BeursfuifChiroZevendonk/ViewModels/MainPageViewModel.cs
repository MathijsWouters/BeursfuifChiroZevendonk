using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BeursfuifChiroZevendonk.ViewModels
{
    public partial class MainPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private bool _isBeursPageOpen;
        public ObservableCollection<Drink> Drinks => _drinksService.Drinks;
        public ICommand DrinkSelectedCommand { get; }
        private readonly DrinksDataService _drinksService;
        public MainPageViewModel(DrinksDataService drinksService)
        {
            DrinkSelectedCommand = new RelayCommand<Drink>(OnDrinkSelected);
            _drinksService = drinksService;
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
                    return;
                }
                else
                {
                    _isBeursPageOpen = false;
                }
            }

            if (!_isBeursPageOpen)
            {
                var beursVm = new BeursPageViewModel(_drinksService); 
                var beursPage = new BeursPage(beursVm);
                var newWindow = new Window(beursPage);
                Application.Current.OpenWindow(newWindow);
                _isBeursPageOpen = true;
            }
        }
        [RelayCommand]
        private async Task NavigateToAddDrink()
        {
            try
            {
                var addDrinkVm = new AddDrinkPageViewModel(_drinksService);
                var uri = new Uri($"///{nameof(AddDrinkPage)}?ViewModel={addDrinkVm.GetType().FullName}", UriKind.Relative);
                await Shell.Current.GoToAsync(uri);
            }
            catch (Exception ex)
            {
                // Output the exception to your debug window or handle it as necessary
                Debug.WriteLine(ex.ToString());
            }
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
        private void OnDrinkSelected(Drink drink)
        {
            // Handle the drink selection here (e.g., navigation or updating UI)
        }
    }
}
