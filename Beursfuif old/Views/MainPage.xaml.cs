using Beursfuif.Models.Beursfuif.Models;
using System.Collections.Specialized;
using System.Diagnostics;


namespace Beursfuif.Views;

public partial class MainPage : ContentPage

{
    private bool _isBeursPageOpen = false;
    private DrinksViewModel _viewModel;
    private Receipt _receipt = new Receipt();
    private CountdownTimer _tenSecondTimer;
    private CountdownTimer _fiveMinuteTimer;
    private bool _isFeestjeActive = false;
    private Label _timerLabel;
    private const string SalesDataFile = "sales_data.json";
    private const string FiveMinuteDataFile = "five_minute_drink_data.json";
    private DrinkDataService _drinkDataService = new DrinkDataService();
    private Dictionary<int, Frame> drinkViews = new Dictionary<int, Frame>();


#if WINDOWS
    private KeyboardService _keyboardService;
#endif

    public MainPage()
    {
        InitializeComponent();
        _viewModel = new DrinksViewModel();
        _receipt = new Receipt();
        BindingContext = _receipt;
        _viewModel.Drinks.CollectionChanged += Drinks_CollectionChanged;
        _viewModel.DrinksUpdated += RefreshDrinksDisplay;
        ReceiptListView.ItemsSource = _receipt.Items;
        _tenSecondTimer = new CountdownTimer(Dispatcher);
        _tenSecondTimer.TimeUpdated += OnTenSecondTimeUpdated;
        _tenSecondTimer.CountdownCompleted += OnTenSecondCountdownCompleted;

        _fiveMinuteTimer = new CountdownTimer(Dispatcher);
        _fiveMinuteTimer.TimeUpdated += OnFiveMinuteTimeUpdated;
        _fiveMinuteTimer.CountdownCompleted += OnFiveMinuteCountdownCompleted;
#if WINDOWS
    _keyboardService = new KeyboardService();
    _keyboardService.OnBackspacePressed = RemoveLastItemFromReceipt;
    _keyboardService.OnNumpadPressed = AddDrinkByNumber;
    _keyboardService.OnEnterPressed = OnEnterKeyPressed;
    _keyboardService.Start();
#endif

    }
    private void OnEnterKeyPressed()
    {
        if (_isFeestjeActive)
        {
            // Only finalize the receipt or perform the intended action
            // No toggling of feestje state here
            OnTenSecondCountdownCompleted();
        }
        // Else, you might want to handle a different action when feestje is not active, or do nothing.
    }
    private void OnTenSecondTimeUpdated(int time)
    {
        TenSecondTimerLabel.Text = $"Timer: {time} seconds";
    }
    private async void OnStartFeestjeClicked(object sender, EventArgs e)
    {
        StartFeestjeButton.IsEnabled = false;
        StopFeestjeButton.IsEnabled = false ;
        _isFeestjeActive = !_isFeestjeActive;
        StartFeestjeButton.BackgroundColor = _isFeestjeActive ? Colors.Green : Colors.Transparent;

        if (_isFeestjeActive)
        {
            var drinkSalesDataService = new DrinkDataService();
            await HandleExistingFileAsync(SalesDataFile);
            await drinkSalesDataService.LoadOrCreateSalesDataAsync(SalesDataFile);

            await InitializeFiveMinuteSalesDataWithRandomValues();
            await InitializeCurrentSalesData();
            _fiveMinuteTimer.Start(1 * 20); 
        }
        else
        {
            _fiveMinuteTimer.Stop(); 
        }
        StopFeestjeButton.IsEnabled = true;
        StartFeestjeButton.IsEnabled = true;
        FocusSinkButton.Focus();
    }
    private async Task<bool> PromptUserForFileHandlingAsync()
    {
        bool deleteFile = await DisplayAlert("File Exists", "A sales data file already exists. Do you want to delete it?", "Delete", "Keep and Rename");
        return deleteFile;
    }
    private async Task HandleExistingFileAsync(string filename)
    {
        var folderPath = FileSystem.AppDataDirectory;
        var filePath = Path.Combine(folderPath, filename);

        if (File.Exists(filePath) && _isFeestjeActive)
        {
            bool deleteFile = await PromptUserForFileHandlingAsync();

            if (deleteFile)
            {
                File.Delete(filePath);
            }
            else
            {
                string newFileName = $"sales_data_{DateTime.Now:yyyyMMddHHmmss}.json";
                File.Move(filePath, Path.Combine(folderPath, newFileName));
            }
        }
    }
    private async void OnTenSecondCountdownCompleted()
    {
        if (_isFeestjeActive)
        {
            var drinkSalesDataService = new DrinkDataService();
            foreach (var item in _receipt.Items)
            {
                await drinkSalesDataService.UpdateSalesDataAsync("current_" + FiveMinuteDataFile, item.DrinkName, item.Quantity);
                await drinkSalesDataService.UpdateAndSaveSalesDataAsync(SalesDataFile, item.DrinkName, item.Quantity, item.TotalPrice);
            }
        }
        _receipt.Items.Clear(); 
        _tenSecondTimer.Reset();
        _receipt.NotifyTotalUpdates();
    }

    public void RemoveLastItemFromReceipt()
    {
        _receipt.RemoveLastItem();

    }

    public void AddDrinkByNumber(int number)
    {
        Drink drink = GetDrinkByNumber(number);
        if (drink == null)
        {
            Console.WriteLine($"Numpad {number} pressed, but no corresponding drink found.");
        }
        else
        {
            _receipt.AddItem(drink);
            _tenSecondTimer.Start(10);
        }
    }

    public Drink GetDrinkByNumber(int number)
    {
        return _viewModel.Drinks.FirstOrDefault(drink => drink.Number == number);
    }
    private void RefreshDrinksDisplay()
    {
        foreach (var drink in _viewModel.Drinks)
        {
            // Check if the view already exists
            if (!drinkViews.TryGetValue(drink.Number, out var existingView))
            {
                // Create a new view for the drink if it doesn't exist
                var drinkView = CreateDrinkView(drink);
                DrinksGrid.Children.Add(drinkView);
                drinkViews[drink.Number] = drinkView; // Track the view by drink number
            }
            else
            {
                // Update existing view's context if it already exists
                existingView.BindingContext = drink;
            }
        }

        // Clean up any views that no longer have a corresponding drink
        var currentDrinkNumbers = _viewModel.Drinks.Select(d => d.Number).ToList();
        var viewNumbersToRemove = drinkViews.Keys.Where(k => !currentDrinkNumbers.Contains(k)).ToList();
        foreach (var numberToRemove in viewNumbersToRemove)
        {
            DrinksGrid.Children.Remove(drinkViews[numberToRemove]);
            drinkViews.Remove(numberToRemove);
        }
    }
    private void Drinks_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        // No need to clear all children since we are reusing views now
        foreach (var drink in _viewModel.Drinks)
        {
            if (!drinkViews.TryGetValue(drink.Number, out var frame))
            {
                frame = CreateDrinkView(drink);
                DrinksGrid.Children.Add(frame);
                drinkViews[drink.Number] = frame;
            }
            else
            {
                // If the view already exists, just update its context
                frame.BindingContext = drink;
            }

            // Update the Grid layout position if necessary
            int row = (drink.Number - 1) / 3;
            int column = (drink.Number - 1) % 3;
            Grid.SetRow(frame, row);
            Grid.SetColumn(frame, column);
        }

        // Remove views for drinks that are no longer present
        var drinksInViewModel = _viewModel.Drinks.Select(d => d.Number).ToList();
        var numbersToRemove = drinkViews.Keys.Where(k => !drinksInViewModel.Contains(k)).ToList();
        foreach (var number in numbersToRemove)
        {
            DrinksGrid.Children.Remove(drinkViews[number]);
            drinkViews.Remove(number);
        }
    }

    private Frame CreateDrinkView(Drink drink)
    {
        var nameLabel = new Label
        {
            Text = drink.Name,
            FontAttributes = FontAttributes.Bold,
            FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Start
        };

        var priceLabel = new Label
        {
            FontAttributes = FontAttributes.Bold,
            FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.End,
            Margin = new Thickness(0, 0, 10, 0)
        };
        priceLabel.SetBinding(Label.TextProperty, "CurrentPrice", stringFormat: "{0:C}");

        var colorBoxView = new BoxView
        {
            Color = drink.DrinkColor,
            WidthRequest = 60,
            HeightRequest = 125,
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand
        };

        var gridButton = new Grid
        {
            ColumnDefinitions = { new ColumnDefinition { Width = GridLength.Auto }, new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) } },
            RowDefinitions = { new RowDefinition { Height = GridLength.Auto }, new RowDefinition { Height = GridLength.Auto }, new RowDefinition { Height = GridLength.Auto } },
            BackgroundColor = Colors.Transparent
        };

        gridButton.Children.Add(colorBoxView);
        Grid.SetColumn(colorBoxView, 0);
        Grid.SetRowSpan(colorBoxView, 3);

        gridButton.Children.Add(nameLabel);
        Grid.SetColumn(nameLabel, 1);
        Grid.SetRow(nameLabel, 0);

        var numberFrame = new Frame
        {
            BackgroundColor = Colors.Gray,
            BorderColor = Colors.Black,
            Padding = 0,
            Content = new Label { Text = $"{drink.Number}", FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, TextColor = Colors.White },
            CornerRadius = 5,
            WidthRequest = 30,
            HeightRequest = 30,
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Center,
            Margin = new Thickness(10, 0, 0, 0)
        };
        gridButton.Children.Add(numberFrame);
        Grid.SetColumn(numberFrame, 1);
        Grid.SetRow(numberFrame, 1);

        gridButton.Children.Add(priceLabel);
        Grid.SetColumn(priceLabel, 1);
        Grid.SetRow(priceLabel, 2);

        var frame = new Frame
        {
            CornerRadius = 10,
            BorderColor = Colors.White,
            Padding = 0,
            Margin = new Thickness(2),
            WidthRequest = 200,
            HeightRequest = 125,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Content = gridButton,
            BindingContext = drink // Ensure that the frame's context is the drink for binding to work
        };

        var tapGesture = new TapGestureRecognizer();
        tapGesture.Tapped += (s, args) => DrinkButton_Clicked(drink);
        frame.GestureRecognizers.Add(tapGesture);

        return frame;
    }
    private void DrinkButton_Clicked(Drink drink)
    {
        _receipt.AddItem(drink);
        _tenSecondTimer.Start(10);
    }
    private async void OnAddDrinkButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddDrinkPage(_viewModel));
    }
    private async void OnManageDrinksButton_Clicked(object sender, EventArgs e)
    {
        var manageDrinksPage = new ManageDrinksPage(_viewModel);
        await Navigation.PushAsync(manageDrinksPage);
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        RefreshDrinksDisplay();
    }
    private async void OnOpenBeursClicked(object sender, EventArgs e)
    {
        if (_isBeursPageOpen)
        {
            bool response = await DisplayAlert("Venster Open", "Het Beurs venster lijkt al open te zijn. Staat het ergens anders open?", "Ja", "Nee");
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
            var newWindow = new Window(new BeursPage(_viewModel));
            Application.Current.OpenWindow(newWindow);
            _isBeursPageOpen = true;
        }
    }
    private async void OnStopFeestjeClicked(object sender, EventArgs e)
    {
        bool confirmStop = await DisplayAlert("Confirm", "Are you sure you want to stop? There is no going back.", "Yes", "No");
        if (!confirmStop) return;
        _isFeestjeActive = false;
        StartFeestjeButton.BackgroundColor = Colors.DarkSlateGray;
        string filename = "sales_data.json";
        var folderPath = FileSystem.AppDataDirectory;
        var jsonFilePath = Path.Combine(folderPath, filename);

        if (_receipt.Items.Any()) 
        {
            var drinkSalesDataService = new DrinkDataService();
            foreach (var item in _receipt.Items)
            {
                await drinkSalesDataService.UpdateAndSaveSalesDataAsync(filename, item.DrinkName, item.Quantity, item.TotalPrice);
            }
        }
        var downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
        string excelFilePath = Path.Combine(downloadsPath, "sales_data.xlsx");
        await ConvertJsonToExcelAndHandleFileAsync(jsonFilePath, excelFilePath);
        _receipt.Items.Clear();
        _tenSecondTimer.Reset();
        _fiveMinuteTimer.Stop();
        var fiveMinuteDataFilePath = Path.Combine(folderPath, FiveMinuteDataFile);
        if (File.Exists(fiveMinuteDataFilePath))
        {
            File.Delete(fiveMinuteDataFilePath);
        }
        var currentFiveMinuteDataFilePath = Path.Combine(folderPath, "current_" + FiveMinuteDataFile);
        if (File.Exists(currentFiveMinuteDataFilePath))
        {
            File.Delete(currentFiveMinuteDataFilePath);
        }
    }
    private async Task ConvertJsonToExcelAndHandleFileAsync(string jsonFilePath, string excelFilePath)
    {
        try
        {
            var jsonString = await File.ReadAllTextAsync(jsonFilePath);
            var drinks = JsonSerializer.Deserialize<List<DrinkSalesData>>(jsonString);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Sales Data");
                worksheet.Cell(1, 1).Value = "Drink Name";
                worksheet.Cell(1, 2).Value = "Total Sold";
                worksheet.Cell(1, 3).Value = "Total Income";

                int currentRow = 2;
                decimal totalIncome = 0;
                foreach (var drink in drinks)
                {
                    worksheet.Cell(currentRow, 1).Value = drink.DrinkName;
                    worksheet.Cell(currentRow, 2).Value = drink.TotalSold;
                    worksheet.Cell(currentRow, 3).Value = drink.TotalIncome;
                    totalIncome += drink.TotalIncome;
                    currentRow++;
                }
                worksheet.Cell(currentRow + 1, 2).Value = "Total Income";
                worksheet.Cell(currentRow + 1, 3).Value = totalIncome;

                workbook.SaveAs(excelFilePath);
            }
            File.Delete(jsonFilePath);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Failed to save Excel file.", "OK");
        }
    }
    private void OnFiveMinuteTimeUpdated(int timeRemaining)
    {
        Debug.WriteLine($"5-minute timer: {timeRemaining} seconds remaining");
    }
    private async void OnFiveMinuteCountdownCompleted()
    {
        if (_isFeestjeActive)
        {
            await ProcessFiveMinuteSalesData();
            await ResetCurrentSalesData();
            _fiveMinuteTimer.Start(1 * 20);
        }
    }

    private decimal CalculatePercentageChange(int previousSales, int currentSales)
    {
        if (previousSales == 0) return 100; 
        return ((currentSales - previousSales) / (decimal)previousSales) * 100;
    }

    private decimal DetermineAdjustmentMagnitude(decimal percentageChange)
    {
        var random = new Random();
        var chance = random.Next(100); 

        if (percentageChange >= 20) 
        {
            if (chance < 50) 
            {
                return 0.25m; 
            }
            else if (chance > 75) 
            {
                return 0.50m; 
            }
            else 
            {
                return 0; 
            }
        }
        else if (percentageChange <= -20) 
        {
            if (chance < 50) 
            {
                return -0.25m; 
            }
            else if (chance > 75) 
            {
                return -0.50m; 
            }
            else 
            {
                return 0; 
            }
        }

        return 0; 
    }
    private async Task InitializeFiveMinuteSalesDataWithRandomValues()
    {
        var initialData = _viewModel.Drinks.Select(drink => new FiveMinuteDrinkSalesData
        {
            DrinkName = drink.Name,
            QuantitySoldLastFiveMinutes = new Random().Next(5, 15) 
        }).ToList();

        var filePath = Path.Combine(FileSystem.AppDataDirectory, FiveMinuteDataFile);
        var json = JsonSerializer.Serialize(initialData);
        await File.WriteAllTextAsync(filePath, json);
    }

    private async Task ProcessFiveMinuteSalesData()
    {
        var previousSalesDataPath = Path.Combine(FileSystem.AppDataDirectory, FiveMinuteDataFile);
        var previousSalesData = await _drinkDataService.LoadSalesDataAsync(previousSalesDataPath);
        var currentSalesDataPath = Path.Combine(FileSystem.AppDataDirectory, "current_" + FiveMinuteDataFile);
        var currentSalesData = await _drinkDataService.LoadSalesDataAsync(currentSalesDataPath);
        var adjustments = new Dictionary<string, decimal>();
        foreach (var currentSale in currentSalesData)
        {
            var previousSale = previousSalesData.FirstOrDefault(p => p.DrinkName == currentSale.DrinkName);
            if (previousSale == null) continue;

            var percentageChange = CalculatePercentageChange(previousSale.QuantitySoldLastFiveMinutes, currentSale.QuantitySoldLastFiveMinutes);
            var adjustment = DetermineAdjustmentMagnitude(percentageChange);

            adjustments[currentSale.DrinkName] = adjustment; 
        }
        foreach (var drink in _viewModel.Drinks)
        {
            if (adjustments.TryGetValue(drink.Name, out var adjustment))
            {
                var newPrice = Math.Max(drink.MinPrice, Math.Min(drink.MaxPrice, drink.CurrentPrice + adjustment));
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    drink.CurrentPrice = newPrice;
                });
            }
        }
        await File.WriteAllTextAsync(previousSalesDataPath, JsonSerializer.Serialize(currentSalesData));
        await ResetCurrentSalesData();
    }


    private async Task ResetCurrentSalesData()
    {
        var resetData = _viewModel.Drinks.Select(drink => new FiveMinuteDrinkSalesData
        {
            DrinkName = drink.Name,
            QuantitySoldLastFiveMinutes = 0
        }).ToList();

        var filePath = Path.Combine(FileSystem.AppDataDirectory, "current_" + FiveMinuteDataFile);
        await File.WriteAllTextAsync(filePath, JsonSerializer.Serialize(resetData));
    }
    private async Task InitializeCurrentSalesData()
    {
        var currentSalesData = _viewModel.Drinks.Select(drink => new FiveMinuteDrinkSalesData
        {
            DrinkName = drink.Name,
            QuantitySoldLastFiveMinutes = 0 
        }).ToList();

        var filePath = Path.Combine(FileSystem.AppDataDirectory, "current_" + FiveMinuteDataFile);
        var json = JsonSerializer.Serialize(currentSalesData);
        await File.WriteAllTextAsync(filePath, json);
    }
}
