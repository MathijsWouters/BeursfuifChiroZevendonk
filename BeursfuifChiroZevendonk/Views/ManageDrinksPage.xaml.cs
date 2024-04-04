namespace BeursfuifChiroZevendonk.Views;

public partial class ManageDrinksPage : ContentPage
{
	public ManageDrinksPage(ManageDrinksPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}