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
    private void UpdateDrinkViews()
    {
        // Clear existing views
        DrinksLayout.Children.Clear();

        foreach (var drink in _viewModel.Drinks)
        {
            var drinkView = CreateDrinkView(drink);
            DrinksLayout.Children.Add(drinkView);
        }
    }
    private View CreateDrinkView(Drink drink)
    {
        // The separator line
        var separator = new BoxView
        {
            Color = Colors.White,
            HeightRequest = 2,
            WidthRequest = 100, // Or some other appropriate width
            VerticalOptions = LayoutOptions.End,
            Margin = new Thickness(0, 5, 0, 5) // Margin to space out the separator
        };

        // The drink information layout
        var drinkInfoLayout = new StackLayout
        {
            Children =
        {
            new Label
            {
                Text = drink.Name,
                TextColor = Colors.White,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 5) // Spacing above the name
            },
            new Label
            {
                Text = $"{drink.CurrentPrice:C}",
                TextColor = Colors.White,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 0, 0, 5) // Spacing below the price
            },
            separator
        },
            Spacing = 5 // Spacing between elements inside the stack
        };

        // The frame for each drink
        var frame = new Frame
        {
            WidthRequest = 150, // Standard width for all frames
            HeightRequest = 100, // Standard height for all frames
            CornerRadius = 10,
            BorderColor = Colors.White, // White border color
            BackgroundColor = Colors.Black, // Black frame background
            Content = new StackLayout
            {
                Children = { drinkInfoLayout },
                BackgroundColor = drink.DrinkColor // Drink color for the bottom part
            },
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(10) // Space between frames
        };

        return frame;
    }


}