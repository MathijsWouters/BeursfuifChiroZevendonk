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

        [ObservableProperty]
        private ObservableCollection<ReceiptItem> _items = new ObservableCollection<ReceiptItem>();

        public decimal TotalPrice => Items.Sum(item => item.TotalPrice);
        public decimal TotalVakjes => TotalPrice / 0.25m;
        public ObservableCollection<Drink> Drinks => _drinksService.Drinks;
        private readonly DrinksDataService _drinksService;
        public ICommand DrinkSelectedCommand { get; }
        
        public MainPageViewModel(DrinksDataService drinksService)
        {
            DrinkSelectedCommand = new RelayCommand<Drink>(AddDrinkToReceipt);
            _drinksService = drinksService;
#if WINDOWS
    var keyboardService = new KeyboardService();
    keyboardService.OnBackspacePressed += () => RemoveLastItemFromReceipt();
    keyboardService.OnNumpadPressed += AddDrinkByNumber;
    keyboardService.OnEnterPressed += OnEnterKeyPressed;
    keyboardService.Start();
#endif
        }
        private void AddDrinkToReceipt(Drink drink)
        {
            var existingItem = Items.FirstOrDefault(i => i.DrinkName == drink.Name);
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                Items.Add(new ReceiptItem
                {
                    DrinkName = drink.Name,
                    Quantity = 1,
                    CurrentPrice = drink.CurrentPrice
                });
            }
            UpdateReceiptTotals();
        }
        private void AddDrinkByNumber(int number)
        {
            var drink = Drinks.FirstOrDefault(d => d.Number == number);
            if (drink != null)
            {
                AddDrinkToReceipt(drink);
            }
        }

        private void OnEnterKeyPressed()
        {
            // Handle Enter key press here...
        }
        private void RemoveLastItemFromReceipt()
        {
            if (Items.Any())
            {
                var lastItem = Items.Last();
                if (lastItem.Quantity > 1)
                {
                    lastItem.Quantity--;
                }
                else
                {
                    Items.Remove(lastItem);
                }
            }
            UpdateReceiptTotals();
        }
        private void UpdateReceiptTotals()
        {
            OnPropertyChanged(nameof(TotalPrice));
            OnPropertyChanged(nameof(TotalVakjes));
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
                Debug.WriteLine(ex.ToString());
            }
        }

        [RelayCommand]
        private async Task NavigateToManageDrinks()
        {
            try
            {
                var manageDrinksVm = new ManageDrinksPageViewModel(_drinksService);
                var uri = new Uri($"///{nameof(ManageDrinksPage)}?ViewModel={manageDrinksVm.GetType().FullName}", UriKind.Relative);
                await Shell.Current.GoToAsync(uri);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
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
