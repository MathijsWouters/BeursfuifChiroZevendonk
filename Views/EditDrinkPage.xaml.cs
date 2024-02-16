using Beursfuif.Models;
namespace Beursfuif.Views;

public partial class EditDrinkPage : ContentPage
{
    private readonly Drink _currentDrink;
    private readonly DrinksViewModel _viewModel;

    public EditDrinkPage(DrinksViewModel viewModel, Drink drinkToEdit)
    {
        InitializeComponent();
        _viewModel = viewModel;
        _currentDrink = drinkToEdit;

        // Preload drink data into the form fields
        nameEntry.Text = _currentDrink.Name;
        minPriceEntry.Text = _currentDrink.MinPrice.ToString();
        maxPriceEntry.Text = _currentDrink.MaxPrice.ToString();
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        // Validate input and update the drink
        if (decimal.TryParse(minPriceEntry.Text, out var minPrice) && decimal.TryParse(maxPriceEntry.Text, out var maxPrice))
        {
            _currentDrink.Name = nameEntry.Text;
            _currentDrink.MinPrice = minPrice;
            _currentDrink.MaxPrice = maxPrice;

            _viewModel.EditDrink(_currentDrink);
            await DisplayAlert("Success", "Drink updated successfully.", "OK");

            // Assuming you have navigation set up appropriately
            await Navigation.PopAsync(); // Go back to the manage drinks page
        }
        else
        {
            await DisplayAlert("Error", "Please enter valid prices.", "OK");
        }
    }
    private void OnMinPriceDecreaseClicked(object sender, EventArgs e)
    {
        AdjustPrice(minPriceEntry, -0.25m);
    }

    private void OnMinPriceIncreaseClicked(object sender, EventArgs e)
    {
        AdjustPrice(minPriceEntry, 0.25m);
    }

    private void OnMaxPriceDecreaseClicked(object sender, EventArgs e)
    {
        AdjustPrice(maxPriceEntry, -0.25m);
    }

    private void OnMaxPriceIncreaseClicked(object sender, EventArgs e)
    {
        AdjustPrice(maxPriceEntry, 0.25m);
    }

    private void AdjustPrice(Entry priceEntry, decimal adjustment)
    {
        if (decimal.TryParse(priceEntry.Text, out decimal currentPrice))
        {
            decimal newPrice = Math.Max(0, currentPrice + adjustment);
            priceEntry.Text = newPrice.ToString("0.00");
        }
        else
        {
            // If the current value is not a number, reset it to 0.00
            priceEntry.Text = "0.00";
        }
    }


}
