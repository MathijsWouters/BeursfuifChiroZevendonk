using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beursfuif.Models
{
    public class Receipt
    {
        public ObservableCollection<ReceiptItem> Items { get; } = new ObservableCollection<ReceiptItem>();

        public decimal TotalPrice => Items.Sum(item => item.TotalPrice);
        public decimal TotalVakjes => TotalPrice / 0.25m;

        public void AddItem(Drink drink)
        {
            var existingItem = Items.FirstOrDefault(i => i.DrinkName == drink.Name);
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                Items.Add(new ReceiptItem
                {
                    DrinkName = drink.Name,
                    Quantity = 1,
                    CurrentPrice = drink.CurrentPrice 
                });
            }
        }

    }
}
