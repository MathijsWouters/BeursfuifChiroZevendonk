using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BeursfuifChiroZevendonk.ViewModels
{
    public partial class DrinksViewModel : BaseViewModel
    {
        public ObservableCollection<Drink> Drinks { get; set; } = new ObservableCollection<Drink>();
        public ICommand DrinkSelectedCommand { get; }

        public DrinksViewModel()
        {
            DrinkSelectedCommand = new RelayCommand<Drink>(OnDrinkSelected);
        }

        private void OnDrinkSelected(Drink drink)
        {
            // Handle the drink selection here (e.g., navigation or updating UI)
        }
    }

}
