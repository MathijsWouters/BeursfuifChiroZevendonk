using Beursfuif.Models;
namespace Beursfuif.Views;

public partial class AddDrinkPage : ContentPage
{
    private DrinksViewModel _viewModel;

    // Constructor that takes the ViewModel
    public AddDrinkPage(DrinksViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
    }
    private void ValidateForm()
    {
        bool isNameNotEmpty = !string.IsNullOrWhiteSpace(nameEntry.Text);
        bool isMinPriceLessThanMaxPrice = false;

        if (decimal.TryParse(minPriceEntry.Text, out decimal minPrice) &&
            decimal.TryParse(maxPriceEntry.Text, out decimal maxPrice))
        {
            isMinPriceLessThanMaxPrice = minPrice < maxPrice;
        }
        if (saveDrinkButton != null)
        {
            saveDrinkButton.IsEnabled = isNameNotEmpty && isMinPriceLessThanMaxPrice;
        }
    }
    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        ValidateForm();
    }
    private async void OnSubmitClicked(object sender, EventArgs e)
    {
        // Assuming you have already created and populated a new Drink object...
        // This is where you would call your ViewModel and add the drink.

        // Parse the color from the label's text
        if (Color.TryParse(SelectedColorValueLabel.Text, out var selectedColor))
        {
            decimal minPrice, maxPrice;
            bool isMinPriceValid = decimal.TryParse(minPriceEntry.Text, out minPrice);
            bool isMaxPriceValid = decimal.TryParse(maxPriceEntry.Text, out maxPrice);

            if (isMinPriceValid && isMaxPriceValid && minPrice < maxPrice && !string.IsNullOrWhiteSpace(nameEntry.Text))
            {
                // Use the parsed color and price to add a new drink
                _viewModel.AddDrink(nameEntry.Text, selectedColor, minPrice, maxPrice);
                await Navigation.PopAsync();
            }
            else
            {
                // Handle validation failure
                string validationMessage = "";
                if (string.IsNullOrWhiteSpace(nameEntry.Text))
                    validationMessage += "The drink name is required.\n";
                if (!isMinPriceValid || !isMaxPriceValid)
                    validationMessage += "Please enter valid prices.\n";
                if (minPrice >= maxPrice)
                    validationMessage += "The min price must be less than the max price.\n";

                await DisplayAlert("Validation Error", validationMessage.Trim(), "OK");
            }
        }
        else
        {
            // Handle invalid color format
            await DisplayAlert("Invalid Color", "The selected color is not valid.", "OK");
        }
    }

    private void ColorPicker_PickedColorChanged(object sender, Color colorPicked)
    {
        // Use the selected color
        SelectedColorValueLabel.Text = colorPicked.ToHex();
        SelectedColorValueLabel.BackgroundColor = colorPicked;
        this.BackgroundColor = colorPicked;

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
            priceEntry.Text = "0.00";
        }
    }



}