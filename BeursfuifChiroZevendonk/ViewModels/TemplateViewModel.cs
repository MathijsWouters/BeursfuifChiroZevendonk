using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeursfuifChiroZevendonk.ViewModels
{
    public partial class TemplateViewModel : BaseViewModel
    {
        [ObservableProperty]
        string templateString;

        public TemplateViewModel()
        {
            templateString = "";
        }
        [RelayCommand]
        private void TemplateMethod()
        {

        }
    }
}
