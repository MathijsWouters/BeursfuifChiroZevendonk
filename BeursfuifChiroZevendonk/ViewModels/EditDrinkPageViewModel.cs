using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BeursfuifChiroZevendonk.ViewModels
{
    public partial class EditDrinkPageViewModel : BaseViewModel
    {
        private readonly DrinksDataService _drinksService;
        private Drink _originalDrink;
        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private decimal minPrice;

        [ObservableProperty]
        private decimal maxPrice;
        [ObservableProperty]
        private ICommand saveDrinkCommand;
        public EditDrinkPageViewModel(Drink drink, DrinksDataService drinksService)
        {
            _originalDrink = drink;
            _drinksService = drinksService;

            name = drink.Name;
            minPrice = drink.MinPrice;
            maxPrice = drink.MaxPrice;

            SaveDrinkCommand = new RelayCommand(UpdateDrink, CanSaveDrink);
        }
        private bool CanSaveDrink()
        {
            return !string.IsNullOrWhiteSpace(Name) && MinPrice < MaxPrice;
        }

        private async void UpdateDrink()
        {
            _originalDrink.Name = this.Name;
            _originalDrink.MinPrice = this.MinPrice;
            _originalDrink.MaxPrice = this.MaxPrice;

            // Assuming there's a method to update the drink in the data service
            _drinksService.UpdateDrink(_originalDrink);

            await DisplayConfirmation("Success", "Drink updated successfully");
            await Shell.Current.GoToAsync("..");
        }

        private async Task DisplayConfirmation(string title, string message)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, "OK");
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.PropertyName == nameof(Name) || e.PropertyName == nameof(MinPrice) || e.PropertyName == nameof(MaxPrice))
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
    }
}
