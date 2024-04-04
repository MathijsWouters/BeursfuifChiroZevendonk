namespace BeursfuifChiroZevendonk.Views;

public partial class BeursPage : ContentPage
{
	public BeursPage(BeursPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}