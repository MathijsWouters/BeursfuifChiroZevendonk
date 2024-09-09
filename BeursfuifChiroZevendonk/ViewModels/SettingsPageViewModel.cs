using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeursfuifChiroZevendonk.ViewModels
{
    public partial class SettingsPageViewModel : BaseViewModel
    {
        public SettingsPageViewModel()
        {
            Title = "Settings";
        }

        // Add settings properties here
        // Example:
        [ObservableProperty]
        private bool isDarkModeEnabled;

        [RelayCommand]
        private void ToggleDarkMode()
        {
            IsDarkModeEnabled = !IsDarkModeEnabled;
        }
    }
}

