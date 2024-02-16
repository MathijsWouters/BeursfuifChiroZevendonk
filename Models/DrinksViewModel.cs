using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Beursfuif.Models
{
    public class DrinksViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Drink> Drinks { get; } = new();

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
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

        public void EditDrink(Drink drinkToEdit)
        {
            // Find the drink in the collection
            var drink = Drinks.FirstOrDefault(d => d.Number == drinkToEdit.Number);
            if (drink != null)
            {
                drink.Name = drinkToEdit.Name;
                drink.DrinkColor = drinkToEdit.DrinkColor;
                drink.MinPrice = drinkToEdit.MinPrice;
                drink.MaxPrice = drinkToEdit.MaxPrice;

                // Notify UI of changes
                OnPropertyChanged(nameof(Drinks));
            }
        }

        public void DeleteDrink(Drink drinkToDelete)
        {
            Drinks.Remove(drinkToDelete); // Removes the drink from the ObservableCollection

            // Update the Number for remaining drinks
            int number = 1;
            foreach (var drink in Drinks)
            {
                drink.Number = number++;
            }

            // Inform the UI that the collection has changed
            OnPropertyChanged(nameof(Drinks));
        }
    }
}

