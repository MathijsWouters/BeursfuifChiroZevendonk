using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beursfuif.Models
{
    public class DrinksViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Drink> Drinks { get; } = new();

        public event PropertyChangedEventHandler PropertyChanged;

        public void AddDrink(string name, Color color, decimal minPrice, decimal maxPrice)
        {
            var newDrink = new Drink
            {
                Number = Drinks.Count + 1,
                Name = name,
                DrinkColor = color,
                MinPrice = minPrice,
                MaxPrice = maxPrice
            };

            Drinks.Add(newDrink);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Drinks)));
        }

        public void EditDrink(Drink drink)
        {
            // Edit logic
        }

        public void DeleteDrink(Drink drink)
        {
            Drinks.Remove(drink);
        }
    }
}

