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
        // Label for the name of the drink
        var nameLabel = new Label
        {
            Text = drink.Name,
            TextColor = Colors.White,
            FontAttributes = FontAttributes.Bold,
            FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };

        // Label for the price of the drink
        var priceLabel = new Label
        {
            Text = $"{drink.CurrentPrice:C}",
            TextColor = Colors.White,
            FontAttributes = FontAttributes.Bold,
            FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };

        // StackLayout for the text part of the frame (50% height)
        var textStackLayout = new StackLayout
        {
            Children = { nameLabel, priceLabel },
            VerticalOptions = LayoutOptions.FillAndExpand,
            Spacing = 5 // Adjust the spacing between the labels if needed
        };

        // BoxView for the color part of the frame (50% height)
        var colorBoxView = new BoxView
        {
            Color = drink.DrinkColor,
            VerticalOptions = LayoutOptions.FillAndExpand,
            HorizontalOptions = LayoutOptions.FillAndExpand
        };

        // Grid to hold the textStackLayout and colorBoxView
        var grid = new Grid
        {
            RowDefinitions =
        {
            new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }, // 50% for text
            new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }  // 50% for color
        }
        };

        // Add the text layout and color layout to the grid
        grid.Children.Add(textStackLayout);
        Grid.SetRow(textStackLayout, 0); // Place the textStackLayout in the first row
        grid.Children.Add(colorBoxView);
        Grid.SetRow(colorBoxView, 1); // Place the colorBoxView in the second row

        // Create the frame and set the grid as its content
        var frame = new Frame
        {
            BorderColor = Colors.White,
            CornerRadius = 10,
            BackgroundColor = Colors.Transparent, // No background color; let the child elements dictate the color
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.FillAndExpand, // Let the frame fill 90% of its container's height
            WidthRequest = 150, // Standard width for all frames
            Padding = new Thickness(0),
            Margin = new Thickness(5, 2.5), // Reduced margin for spacing between frames and a margin at the top and bottom
            Content = grid
        };

        return frame;
    }


}