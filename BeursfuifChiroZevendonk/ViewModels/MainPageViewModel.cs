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
        private Color crashButtonColor;
        [ObservableProperty]
        private bool stopFeestjeButtonEnabled = false;
        private readonly IDispatcher dispatcher;
        [ObservableProperty]
        private double progressValue;
        private Timer countdownTimer;
        private Timer updateTimer;
        private Timer crashTimer;
        private Timer partyTimer;
        private double updateIntervalInSeconds;
        private double crashIntervalInSeconds;
        private const string FiveMinuteDataFile = "five_minute_drink_data.json";
        private DateTime lastDrinkAddedTime;
        [ObservableProperty]
        private bool _isBeursPageOpen;
        [ObservableProperty]
        private bool _isCrashActive;

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
        public ICommand CrashCommand { get; }
        public MainPageViewModel(IDispatcher dispatcher, DrinksDataService drinksService)
        {
            DrinkSelectedCommand = new RelayCommand<Drink>(AddDrinkToReceipt);
            CrashCommand = new Command(ExecuteCrashCommand);
            _drinksService = drinksService;
            _isFeestjeActive = false;
            this.dispatcher = dispatcher;
            InitializeTimers();
#if WINDOWS
    var keyboardService = new KeyboardService();
    keyboardService.OnBackspacePressed += () => RemoveLastItemFromReceipt();
    keyboardService.OnNumpadPressed += AddDrinkByNumber;
    keyboardService.OnEnterPressed += OnEnterKeyPressed;
    keyboardService.Start();
#endif
        }
        private void InitializeTimers()
        {
            countdownTimer = new Timer(1000);
            countdownTimer.Elapsed += HandleCountdownTick;
            countdownTimer.AutoReset = true;

            updateTimer = new Timer(); 
            updateTimer.Elapsed += HandleUpdateTick;  

            crashTimer = new Timer(); 
            crashTimer.Elapsed += HandleCrashTick;

            partyTimer = new Timer(500); 
            partyTimer.Elapsed += (sender, e) => PartyTime(); 
            partyTimer.AutoReset = true;

        }
        private void StartProgressBarForUpdate()
        {
            if (updateIntervalInSeconds <= 0) return;


            double durationMilliseconds = updateIntervalInSeconds * 1000;
            MessagingCenter.Send(this, "ProgressUpdateDuration", durationMilliseconds);

        }

        private void StartProgressBarForCrash()
        {
            if (crashIntervalInSeconds <= 0) return;

            double durationMilliseconds = crashIntervalInSeconds * 1000;
            MessagingCenter.Send(this, "ProgressCrashDuration", durationMilliseconds);
        }


        public void SetUpdateInterval(double seconds)
        {
            if (updateTimer != null)
            {
                updateTimer.Stop(); 
                updateTimer.Interval = seconds * 1000;
                updateIntervalInSeconds = seconds;
                if (_isFeestjeActive)
                {
                    updateTimer.Start(); 
                }
            }
        }
        public void SetCrashInterval(double seconds)
        {
            if (crashTimer != null)
            {
                crashTimer.Stop(); 
                crashTimer.Interval = seconds * 1000;
                crashIntervalInSeconds = seconds;   
                if (_isFeestjeActive)
                {
                    crashTimer.Start(); 
                }
            }
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
                dispatcher.Dispatch(() =>
                {
                    TenSecondTimerText = $"Timer: {10 - (int)timeSinceLastDrinkAdded.TotalSeconds} seconds";
                });
            }
        }
        private async void HandleUpdateTick(object sender, ElapsedEventArgs e)
        {
            if (_isCrashActive)
            {
                Debug.WriteLine($"[{DateTime.Now}] Crash detected - switching to crash mode...");
                crashTimer.Start();
                StartProgressBarForCrash();
                await _drinksService.ProcessCrashDataAsync(_drinksService.Drinks.ToList());
                updateTimer.Stop();
                partyTimer.Start();
            }
            else
            {
                Debug.WriteLine($"[{DateTime.Now}] Regular update triggered...");
                StartProgressBarForUpdate();
                await _drinksService.ProcessFiveMinuteSalesDataAsync(_drinksService.Drinks.ToList());
            }

            MessagingCenter.Send<App>((App)Application.Current, "PricesUpdated");

        }

        private async void HandleCrashTick(object sender, ElapsedEventArgs e)
        {
            Debug.WriteLine($"[{DateTime.Now}] Stopping crash mode and resuming normal operation...");

            partyTimer.Stop();
            CrashButtonColor = Colors.DarkSlateGray;
            OnPropertyChanged(nameof(CrashButtonColor));

            Debug.WriteLine($"[{DateTime.Now}] Processing post-crash data...");
            await _drinksService.ProcessPostCrashDataAsync(_drinksService.Drinks.ToList());
            _isCrashActive = false;

            CrashButtonColor = Colors.DarkSlateGray;
            OnPropertyChanged(nameof(CrashButtonColor));
            MessagingCenter.Send<App>((App)Application.Current, "PricesUpdated");

            if (_isFeestjeActive)
            {
                crashTimer.Stop();
                updateTimer.Start();
                StartProgressBarForUpdate();
            }
        }

        private void PartyTime()
        {
            var random = new Random();
            var color = Color.FromRgb(
    random.Next(256) / 255.0,  
    random.Next(256) / 255.0,  
    random.Next(256) / 255.0
            );

            Device.BeginInvokeOnMainThread(() =>
            {
                CrashButtonColor = color;
                OnPropertyChanged(nameof(CrashButtonColor));
            });

                partyTimer.Start(); 
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
            if (!Drinks.Any())
            {
                await Shell.Current.DisplayAlert(
                    "Geen Dranken",
                    "Er zijn geen dranken beschikbaar. Voeg eerst enkele dranken toe.",
                    "OK");
                return; 
            }
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
            if (_isFeestjeActive) 
            {
                await Shell.Current.DisplayAlert("Feestje actief","Het is niet mogelijk drankjes toe te voegen als het feestje actief is.","OK");
                return;
            } 
            if (_drinksService.Drinks.Count >= 9)
            {
                await Shell.Current.DisplayAlert("Maximaal aantal drankjes bereikt", "Er kunnen maximaal 9 drankjes worden toegevoegd. Verwijder een drankje om een nieuwe toe te voegen.", "OK");
                return;
            }
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
        private async Task NavigateToSettings()
        {
            try
            {
                var settingsVm = new SettingsPageViewModel(this);
                var uri = new Uri($"///{nameof(SettingsPage)}?ViewModel={settingsVm.GetType().FullName}", UriKind.Relative);
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
            if (updateTimer == null || updateTimer.Interval == 100 ||
                crashTimer == null || crashTimer.Interval == 100)
            {
                await Shell.Current.DisplayAlert(
                    "Timers Niet Geconfigureerd",
                    "De timers voor updates en crashes moeten zijn ingesteld voordat je het feestje kunt starten.",
                    "OK");
                return; 
            }

            _isFeestjeActive = true;
            StartFeestjeButtonColor = Colors.Green;
            StartFeestjeButtonEnabled = false;
            StopFeestjeButtonEnabled = true;
            await _drinksService.InitializePreviousSalesDataAsync();
            await _drinksService.InitializeCurrentSalesDataAsync();
            updateTimer.Start();
            StartProgressBarForUpdate();
            MessagingCenter.Send<App>((App)Application.Current, "PricesUpdated");
            Debug.WriteLine($"AppDataDirectory: {FileSystem.AppDataDirectory}");

        }

        [RelayCommand]
        private async Task StopFeestje()
        {
            bool confirmStop = await Shell.Current.DisplayAlert("Bevestig", "Ben je zeker dat je wilt stoppen?", "Ja", "Nee");
            if (!confirmStop) return;
            _isFeestjeActive = false;
            StartFeestjeButtonColor = Colors.DarkSlateGray;
            StartFeestjeButtonEnabled = true;
            StopFeestjeButtonEnabled = false;
            countdownTimer.Stop();
            updateTimer.Stop();
            await SaveAndConvertSalesData();
            await _drinksService.DeleteSalesDataAsync("sales_data.json");
            await _drinksService.DeleteFileAsync(FiveMinuteDataFile);
            await _drinksService.DeleteFileAsync("current_" + FiveMinuteDataFile);
            await _drinksService.SaveHistoricalPricesAsync();
            foreach (var drink in _drinksService.Drinks)
            {
                drink.ClearHistoricalPrices();
            }
            await Shell.Current.DisplayAlert("Einde feestje", "Het feestje is gestopt. Je gegevens zitten in je downloads folder.", "OK");

        }
        private async void OnTenSecondCountdownCompleted()
        {
            if (_isFeestjeActive)
            {
                await SaveReceiptData();
                await _drinksService.UpdateCurrentFiveMinuteSalesDataAsync(new List<ReceiptItem>(Items));
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
        private void ExecuteCrashCommand()
        {
            if (_isFeestjeActive)
            {
                _isCrashActive = true;
                CrashButtonColor = Colors.Red;
                OnPropertyChanged(nameof(CrashButtonColor));
            }
            
        }

    }
}
