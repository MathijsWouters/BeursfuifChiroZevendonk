using System.ComponentModel;

namespace Beursfuif.Views;

public partial class BeursPage : ContentPage
{
    private DrinksViewModel _viewModel;

    public BeursPage(DrinksViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        _viewModel.Drinks.CollectionChanged += Drinks_CollectionChanged;
        UpdateDrinkViews();
    }
    private void Drinks_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        UpdateDrinkViews();
    }
    private void Drink_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        UpdateDrinkViews();
    }
    private void UpdateDrinkViews()
    {
        // Clear existing views
        DrinksLayout.Children.Clear();

        foreach (var drink in _viewModel.Drinks)
        {
            drink.PropertyChanged += Drink_PropertyChanged;
            var drinkView = CreateDrinkView(drink);
            DrinksLayout.Children.Add(drinkView);
        }
    }
    private View CreateDrinkView(Drink drink)
    {
        var nameLabel = new Label
        {
            Text = drink.Name,
            TextColor = Colors.White,
            FontAttributes = FontAttributes.Bold,
            FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };
        var priceLabel = new Label
        {
            Text = $"{drink.CurrentPrice:C}",
            TextColor = Colors.White,
            FontAttributes = FontAttributes.Bold,
            FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };

        var screenHeight = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;
        var frameHeight = screenHeight * 0.25 * 0.85;
        var textStackLayout = new StackLayout
        {
            Children = { nameLabel, priceLabel },
            VerticalOptions = LayoutOptions.FillAndExpand,
            Spacing = 5 
        };
        var colorBoxView = new BoxView
        {
            Color = drink.DrinkColor,
            VerticalOptions = LayoutOptions.FillAndExpand,
            HorizontalOptions = LayoutOptions.FillAndExpand
        };
        var grid = new Grid
        {
            RowDefinitions =
        {
            new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }, // 50% for text
            new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }  // 50% for color
        },
            HeightRequest = frameHeight
        };
        grid.Children.Add(textStackLayout);
        Grid.SetRow(textStackLayout, 0); 
        grid.Children.Add(colorBoxView);
        Grid.SetRow(colorBoxView, 1);
        var frame = new Frame
        {
            BorderColor = Colors.White,
            CornerRadius = 10,
            BackgroundColor = Colors.Transparent, 
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.FillAndExpand, 
            WidthRequest = 150,
            HeightRequest = frameHeight,
            Padding = new Thickness(0),
            Margin = new Thickness(5, 2.5), 
            Content = grid
        };

        return frame;
    }


}