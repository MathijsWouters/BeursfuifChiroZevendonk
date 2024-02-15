namespace Beursfuif.Views;

public partial class AddDrinkPage : ContentPage
{
	public AddDrinkPage()
	{
		InitializeComponent();
	}
    private async void OnSubmitClicked(object sender, EventArgs e)
    {
        // Assuming you have already created and populated a new Drink object...
        // This is where you would call your ViewModel and add the drink.

        // Now close the modal
        await Navigation.PopModalAsync();
    }
}