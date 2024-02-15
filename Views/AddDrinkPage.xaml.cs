namespace Beursfuif.Views;

public partial class AddDrinkPage : ContentPage
{
	public AddDrinkPage()
	{
		InitializeComponent();
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

        if (!saveDrinkButton.IsEnabled)
        {
            return;
        }
        await Navigation.PopModalAsync();
    }
    private void ColorPicker_PickedColorChanged(object sender, Color colorPicked)
    {
        // Use the selected color
        SelectedColorValueLabel.Text = colorPicked.ToHex();
        SelectedColorValueLabel.Background = colorPicked;
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