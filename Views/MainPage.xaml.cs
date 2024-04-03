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
        _keyboardService.OnEnterPressed = OnTenSecondCountdownCompleted;
        _keyboardService.Start();
#endif
    }

    private void OnTenSecondTimeUpdated(int time)
    {
        TenSecondTimerLabel.Text = $"Timer: {time} seconds";
    }
    private async void OnStartFeestjeClicked(object sender, EventArgs e)
    {
        _isFeestjeActive = !_isFeestjeActive;
        StartFeestjeButton.BackgroundColor = _isFeestjeActive ? Colors.Green : Colors.Transparent;

        if (_isFeestjeActive)
        {
            var drinkSalesDataService = new DrinkDataService();
            await HandleExistingFileAsync(SalesDataFile);
            await drinkSalesDataService.LoadOrCreateSalesDataAsync(SalesDataFile);

            var fiveMinuteDataService = new DrinkDataService();
            await fiveMinuteDataService.LoadOrCreateFiveMinuteSalesDataAsync(FiveMinuteDataFile);

            _fiveMinuteTimer.Start(5 * 60); 
        }
        else
        {
            _fiveMinuteTimer.Stop(); 
        }
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
                await drinkSalesDataService.UpdateAndSaveSalesDataAsync(SalesDataFile, item.DrinkName, item.Quantity, item.TotalPrice);

                await drinkSalesDataService.UpdateAndSaveFiveMinuteSalesDataAsync(FiveMinuteDataFile, item.DrinkName, item.Quantity);
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

    private void Drinks_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        DrinksGrid.Children.Clear(); 

        foreach (var drink in _viewModel.Drinks)
        {
            // Create the frame that will act like a button with rounded corners and a border
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
            };

            // Create the grid to hold the color block and text
            var gridButton = new Grid
            {
                ColumnDefinitions =
            {
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) },
                new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) }
            },
                RowDefinitions =
            {
                new RowDefinition { Height = GridLength.Auto },
                new RowDefinition { Height = GridLength.Auto },
                new RowDefinition { Height = GridLength.Auto }
            },
                BackgroundColor = Colors.Transparent
            };

            // Add the colored box for the button's left side
            var colorBoxView = new BoxView
            {
                Color = drink.DrinkColor,
                WidthRequest = 60,
                HeightRequest = 125,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            gridButton.Children.Add(colorBoxView);
            Grid.SetColumn(colorBoxView, 0);
            Grid.SetRowSpan(colorBoxView, 3);

            // Add the text content for the button's right side
            var nameLabel = new Label
            {
                Text = drink.Name,
                FontAttributes = FontAttributes.Bold,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start
            };
            gridButton.Children.Add(nameLabel);
            Grid.SetColumn(nameLabel, 1);
            Grid.SetRow(nameLabel, 0);

            // Create a Frame for the drink's number with a border and gray background
            var numberFrame = new Frame
            {
                BackgroundColor = Colors.Gray,
                BorderColor = Colors.Black,
                Padding = 0,
                Content = new Label
                {
                    Text = $"{drink.Number}",
                    FontAttributes = FontAttributes.Bold,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    TextColor = Colors.White
                },
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

            // Add a Label for the drink's price at the bottom right
            var priceLabel = new Label
            {
                Text = $"{drink.StartingPrice:C}",
                FontAttributes = FontAttributes.Bold,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.End,
                Margin = new Thickness(0, 0, 10, 0)
            };
            gridButton.Children.Add(priceLabel);
            Grid.SetColumn(priceLabel, 1);
            Grid.SetRow(priceLabel, 2);
            frame.Content = gridButton;
            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += (s, args) => DrinkButton_Clicked(drink);
            frame.GestureRecognizers.Add(tapGesture);
            int row = (drink.Number - 1) / 3;
            int column = (drink.Number - 1) % 3;
            Grid.SetRow(frame, row);
            Grid.SetColumn(frame, column);
            DrinksGrid.Children.Add(frame);
        }
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
    private void RefreshDrinksDisplay()
    {
        Drinks_CollectionChanged(null, null);
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
        StartFeestjeButton.BackgroundColor = Colors.Transparent;
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
            var fiveMinuteDataService = new DrinkDataService();
            await fiveMinuteDataService.ClearFiveMinuteSalesDataAsync(FiveMinuteDataFile);
        }
        if (_isFeestjeActive)
        {
            _fiveMinuteTimer.Start(5 * 60);
        }
    }

    private async Task ProcessFiveMinuteSalesData()
    {
        var fiveMinuteDataService = new DrinkDataService();
        var salesData = await fiveMinuteDataService.LoadOrCreateFiveMinuteSalesDataAsync(FiveMinuteDataFile);
        // Perform your calculations with salesData here...
        Debug.WriteLine("Processing 5-minute sales data...");
        // This is where you would process and use the data. This might involve calculations,
        // updating the UI, or other logic specific to your application.
    }


}
