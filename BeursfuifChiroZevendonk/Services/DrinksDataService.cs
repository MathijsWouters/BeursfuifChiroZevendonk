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

        public void UpdateDrink(Drink updatedDrink)
        {
            var drink = Drinks.FirstOrDefault(d => d.Number == updatedDrink.Number); 
            if (drink != null)
            {
                drink.Name = updatedDrink.Name;
                drink.MinPrice = updatedDrink.MinPrice;
                drink.MaxPrice = updatedDrink.MaxPrice;
            }
        }
    }
}
