using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeursfuifChiroZevendonk.Services
{
    public class DrinksDataService
    {
        public ObservableCollection<Drink> Drinks { get; private set; } = new ObservableCollection<Drink>();

        // ... Other drink related methods
    }
}
