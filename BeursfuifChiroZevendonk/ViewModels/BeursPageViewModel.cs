using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeursfuifChiroZevendonk.ViewModels
{
    public partial class BeursPageViewModel : BaseViewModel
    {
        public ObservableCollection<Drink> Drinks => _drinksService.Drinks;

        private readonly DrinksDataService _drinksService;

        public BeursPageViewModel(DrinksDataService drinksService)
        {
            base.Title = "Beurs";
            _drinksService = drinksService;
            // This ViewModel now has access to the same Drinks collection as MainPageViewModel
        }
    }
}
