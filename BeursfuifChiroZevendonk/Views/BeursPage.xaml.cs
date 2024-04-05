namespace BeursfuifChiroZevendonk.Views;

public partial class BeursPage : ContentPage
{
	public BeursPage(BeursPageViewModel viewmodel)
	{
        InitializeComponent();
		BindingContext = viewmodel;
	}
}