using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeursfuifChiroZevendonk.ViewModels
{
    public partial class SettingsPageViewModel : BaseViewModel
    {
        public SettingsPageViewModel()
        {
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

