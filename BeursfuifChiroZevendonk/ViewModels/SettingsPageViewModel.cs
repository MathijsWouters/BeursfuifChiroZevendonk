using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BeursfuifChiroZevendonk.ViewModels
{
    public partial class SettingsPageViewModel : BaseViewModel
    {
        private readonly MainPageViewModel _mainPageViewModel;

        public SettingsPageViewModel(MainPageViewModel mainPageViewModel)
        {
            _mainPageViewModel = mainPageViewModel;
            UpdateInterval = null;
            CrashInterval = null;
        }

        [ObservableProperty]
        private int? updateInterval; 

        [ObservableProperty]
        private int? crashInterval; 

        [RelayCommand]
        private async Task ApplyTimerSettings()
        {
            try
            {
                if (UpdateInterval == null || UpdateInterval <= 0 || CrashInterval == null || CrashInterval <= 0)
                {
                    await Shell.Current.DisplayAlert("Invalid Input", "Please enter valid positive numbers for both timers.", "OK");
                    return;
                }

                _mainPageViewModel.SetUpdateInterval(UpdateInterval.Value);
                _mainPageViewModel.SetCrashInterval(CrashInterval.Value);

                await Shell.Current.DisplayAlert("Success", "Timer settings have been applied.", "OK");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                await Shell.Current.DisplayAlert("Error", "An error occurred while applying timer settings.", "OK");
            }
        }

        [RelayCommand]
        private async Task NavigateToMainPage()
        {
            try
            {
                var uri = new Uri($"///{nameof(MainPage)}", UriKind.Relative);
                await Shell.Current.GoToAsync(uri);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
    }
}
