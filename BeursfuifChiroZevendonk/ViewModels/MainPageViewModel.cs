using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Timer = System.Timers.Timer;

namespace BeursfuifChiroZevendonk.ViewModels
{
    public partial class MainPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private Color startFeestjeButtonColor = Colors.DarkSlateGray;

        [ObservableProperty]
        private bool startFeestjeButtonEnabled = true;

        [ObservableProperty]
        private bool stopFeestjeButtonEnabled = false;
        private readonly IDispatcher dispatcher;
        private Timer countdownTimer;
        private DateTime lastDrinkAddedTime;
        [ObservableProperty]
        private bool _isBeursPageOpen;

        [ObservableProperty]
        private string tenSecondTimerText = "Timer: 10 seconds";
        [ObservableProperty]
        private ObservableCollection<ReceiptItem> _items = new ObservableCollection<ReceiptItem>();
        private bool _isFeestjeActive;
        public decimal TotalPrice => Items.Sum(item => item.TotalPrice);
        public decimal TotalVakjes => TotalPrice / 0.25m;
        public ObservableCollection<Drink> Drinks => _drinksService.Drinks;
        private readonly DrinksDataService _drinksService;
        public ICommand DrinkSelectedCommand { get; }
        public MainPageViewModel(IDispatcher dispatcher, DrinksDataService drinksService)
        {
            DrinkSelectedCommand = new RelayCommand<Drink>(AddDrinkToReceipt);
            _drinksService = drinksService;
            _isFeestjeActive = false;
            this.dispatcher = dispatcher;
            InitializeCountdownTimer();
#if WINDOWS
    var keyboardService = new KeyboardService();
    keyboardService.OnBackspacePressed += () => RemoveLastItemFromReceipt();
    keyboardService.OnNumpadPressed += AddDrinkByNumber;
    keyboardService.OnEnterPressed += OnEnterKeyPressed;
    keyboardService.Start();
#endif
        }
        private void InitializeCountdownTimer()
        {
            countdownTimer = new Timer(1000);
            countdownTimer.Elapsed += HandleCountdownTick;
            countdownTimer.AutoReset = true;

        }
        private void HandleCountdownTick(object sender, ElapsedEventArgs e)
        {
            var timeSinceLastDrinkAdded = DateTime.Now - lastDrinkAddedTime;
            if (timeSinceLastDrinkAdded.TotalSeconds >= 10)
            {
                dispatcher.Dispatch(() =>
                {
                    OnTenSecondCountdownCompleted();
                });
                countdownTimer.Stop();
            }
            else
            {
                // Optionally update some UI to show the countdown, e.g., a timer label
                dispatcher.Dispatch(() =>
                {
                    TenSecondTimerText = $"Timer: {10 - (int)timeSinceLastDrinkAdded.TotalSeconds} seconds";
                });
            }
        }
        private void ResetTimer()
        {
            countdownTimer.Stop();
            TenSecondTimerText = "Timer: 10 seconds";
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
            lastDrinkAddedTime = DateTime.Now; 
            if (!countdownTimer.Enabled) countdownTimer.Start();
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
            OnTenSecondCountdownCompleted();
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
        private void StartFeestje()
        {
            _isFeestjeActive = true;
            StartFeestjeButtonColor = Colors.Green;
            StartFeestjeButtonEnabled = false;
            StopFeestjeButtonEnabled = true;
        }

        [RelayCommand]
        private async Task StopFeestje()
        {
            bool confirmStop = await Shell.Current.DisplayAlert("Confirm", "Are you sure you want to stop? There is no going back.", "Yes", "No");
            if (!confirmStop) return;
            _isFeestjeActive = false;
            StartFeestjeButtonColor = Colors.DarkSlateGray;
            StartFeestjeButtonEnabled = true;
            StopFeestjeButtonEnabled = false;
            countdownTimer.Stop();
            await SaveAndConvertSalesData();
            await _drinksService.DeleteSalesDataAsync("sales_data.json");
        }
        private async void OnTenSecondCountdownCompleted()
        {
            if (_isFeestjeActive)
            {
                await SaveReceiptData();
            }
            ClearReceipt();
            ResetTimer();
        }


        private async Task SaveReceiptData()
        {
            if (Items.Any())
            {
                await _drinksService.UpdateAndSaveSalesDataAsync("sales_data.json", new List<ReceiptItem>(Items));
            }
        }

        private void ClearReceipt()
        {
            Items.Clear(); 
            UpdateReceiptTotals();
        }

        private async Task SaveAndConvertSalesData()
        {
            if (!_isFeestjeActive)
            {
                await SaveReceiptData(); 
                await _drinksService.ConvertSalesDataToExcelAsync("sales_data.json", "Beursfuif.xlsx");
            }
        }

    }
}
