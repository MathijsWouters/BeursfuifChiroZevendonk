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
        private void SaveDrink()
        {
            var newDrink = new Drink(MinPrice, MaxPrice)
            {
                Name = this.Name,
                DrinkColorHex = this.ColorHex
            };
            _drinksService.Drinks.Add(newDrink);
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

    }
}
