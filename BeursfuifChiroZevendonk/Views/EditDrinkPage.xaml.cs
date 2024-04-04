namespace BeursfuifChiroZevendonk.Views;

public partial class EditDrinkPage : ContentPage
{
	public EditDrinkPage(EditDrinkPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}