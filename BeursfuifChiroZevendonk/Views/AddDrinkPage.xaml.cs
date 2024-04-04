namespace BeursfuifChiroZevendonk.Views;

public partial class AddDrinkPage : ContentPage
{
	public AddDrinkPage(AddDrinkPageViewModel viewModel)
	{
        InitializeComponent();
        BindingContext = viewModel;
    }
    private void ColorPicker_PickedColorChanged(object sender, Color colorPicked)
    {
        var viewModel = (AddDrinkPageViewModel)this.BindingContext;
        viewModel.ColorHex = colorPicked.ToHex();

        SelectedColorValueLabel.Text = colorPicked.ToHex();
        SelectedColorValueLabel.Background = colorPicked;
    }
}