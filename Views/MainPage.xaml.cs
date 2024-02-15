namespace Beursfuif.Views;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    private async void OnAddDrinkButton_Clicked(object sender, EventArgs e)
    {
        var addDrinkPage = new AddDrinkPage();
        await Navigation.PushModalAsync(addDrinkPage);
    }
}