using Beursfuif.Models;
using System.Collections.Specialized;
using Beursfuif.Services;
using Beursfuif.Models;
namespace Beursfuif.Views;

public partial class MainPage : ContentPage
{
    private DrinksViewModel _viewModel;
    private Receipt _receipt = new Receipt();
    private readonly IKeyboardService _keyboardService;

    public MainPage()
    {
        InitializeComponent();
        _viewModel = new DrinksViewModel();
        _receipt = new Receipt();
        BindingContext = _receipt;
        _viewModel.Drinks.CollectionChanged += Drinks_CollectionChanged;
        _keyboardService = DependencyService.Get<IKeyboardService>();
        _keyboardService?.RegisterKeyPressHandler(KeyPressed);
        ReceiptListView.ItemsSource = _receipt.Items;
    }

    private void Drinks_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        DrinksGrid.Children.Clear(); // Clear existing buttons

        foreach (var drink in _viewModel.Drinks)
        {
            // Create the frame that will act like a button with rounded corners and a border
            var frame = new Frame
            {
                CornerRadius = 10,
                BorderColor = Colors.White,
                Padding = 0,
                Margin = new Thickness(5),
                WidthRequest = 150,
                HeightRequest = 100,
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
                WidthRequest = 50,
                HeightRequest = 100,
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
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.End,
                Margin = new Thickness(0, 0, 10, 0)
            };
            gridButton.Children.Add(priceLabel);
            Grid.SetColumn(priceLabel, 1);
            Grid.SetRow(priceLabel, 2);

            // Set the grid as the content of the frame
            frame.Content = gridButton;

            // Add TapGestureRecognizer to the frame
            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += (s, args) => DrinkButton_Clicked(drink);
            frame.GestureRecognizers.Add(tapGesture);

            // Determine the row and column based on the drink number
            int row = (drink.Number - 1) / 3;
            int column = (drink.Number - 1) % 3;

            // Set the row and column for the frame
            Grid.SetRow(frame, row);
            Grid.SetColumn(frame, column);

            // Add the frame to the grid
            DrinksGrid.Children.Add(frame);
        }
    }
    private void DrinkButton_Clicked(Drink drink)
    {
        _receipt.AddItem(drink);
        RefreshReceiptDisplay();
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
        // You can directly call Drinks_CollectionChanged here if it already sets up the drinks display correctly
        // Passing null for sender and e since they're not used in the method
        Drinks_CollectionChanged(null, null);
    }
    private void KeyPressed(int keyCode)
    {
        // Assuming keyCode is the drink number
        var drink = _viewModel.Drinks.FirstOrDefault(d => d.Number == keyCode);
        if (drink != null)
        {
            // Logic to add the drink to the receipt
            _receipt.AddItem(drink);
            // Refresh the UI to show the updated receipt
            RefreshReceiptDisplay();
        }
    }
    private void RefreshReceiptDisplay()
    {

    }





}
