using Maui.ColorPicker;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BeursfuifChiroZevendonk.ViewModels
{
    public partial class AddDrinkPageViewModel : BaseViewModel
    {
        private readonly DrinksDataService _drinksService;
        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private decimal minPrice;

        [ObservableProperty]
        private decimal maxPrice;

        [ObservableProperty]
        private string colorHex;
        [ObservableProperty]
        private ICommand saveDrinkCommand;
        public AddDrinkPageViewModel(DrinksDataService drinksService)
        {
            _drinksService = drinksService;
            SaveDrinkCommand = new RelayCommand(SaveDrink, CanSaveDrink);
        }
        private bool CanSaveDrink()
        {
            return !string.IsNullOrWhiteSpace(Name) && MinPrice < MaxPrice && !string.IsNullOrWhiteSpace(ColorHex);
        }
        private async void SaveDrink()
        {
            int drinkCount = _drinksService.Drinks.Count;
            int drinkNumber = drinkCount + 1;
            var newDrink = new Drink(MinPrice, MaxPrice)
            {
                Number = drinkNumber,
                Name = this.Name,
                DrinkColorHex = this.ColorHex
            };
            _drinksService.Drinks.Add(newDrink);
            MessagingCenter.Send<App>((App)Application.Current, "PricesUpdated");
            await DisplayConfirmation();

            Name = string.Empty;
            MinPrice = 0;
            MaxPrice = 0;
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }
        private async Task DisplayConfirmation()
        {
            await Application.Current.MainPage.DisplayAlert("Success", "Drankje succesvol toegevoegd!", "OK");
        }
        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.PropertyName == nameof(Name) || e.PropertyName == nameof(MinPrice) || e.PropertyName == nameof(MaxPrice) || e.PropertyName == nameof(ColorHex))
            {
                ((RelayCommand)SaveDrinkCommand)?.NotifyCanExecuteChanged();
            }
        }
        [RelayCommand]
        private void IncreaseMinPrice()
        {
            MinPrice += 0.25m;
        }

        [RelayCommand]
        private void DecreaseMinPrice()
        {
            MinPrice = Math.Max(0, MinPrice - 0.25m);
        }
        [RelayCommand]
        private void IncreaseMaxPrice()
        {
            MaxPrice += 0.25m;
        }
        [RelayCommand]
        private void DecreaseMaxPrice()
        {
            MaxPrice = Math.Max(0, MaxPrice - 0.25m);
        }
        [RelayCommand]
        private static async Task NavigateToMainPage()
        {
            await Shell.Current.GoToAsync("//MainPage");
        }
    }
}
