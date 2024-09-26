namespace BeursfuifChiroZevendonk.Views;

public partial class BeursPage : ContentPage
{
	public BeursPage(BeursPageViewModel viewmodel)
	{
        InitializeComponent();
		BindingContext = viewmodel;
        viewmodel.ProgressAnimationRequested += StartProgressAnimation;

    }
    private async void StartProgressAnimation(double durationMilliseconds)
    {
        Dispatcher.Dispatch(async () =>
        {
            progressBar.Progress = 0;
            await progressBar.ProgressTo(1, (uint)durationMilliseconds, Easing.Linear);

            progressBar.Progress = 0;
        });

    }
}