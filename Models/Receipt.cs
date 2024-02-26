using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Beursfuif.Models
{
    public class Receipt : INotifyPropertyChanged
    {
        private ObservableCollection<ReceiptItem> _items = new ObservableCollection<ReceiptItem>();

        public ObservableCollection<ReceiptItem> Items
        {
            get => _items;
            set
            {
                if (_items != value)
                {
                    _items = value;
                    OnPropertyChanged();
                    UpdateTotalPrice();
                }
            }
        }

        public decimal TotalPrice
        {
            get => Items.Sum(item => item.TotalPrice);
        }

        public decimal TotalVakjes
        {
            get => TotalPrice / 0.25m;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddItem(Drink drink)
        {
            var existingItem = Items.FirstOrDefault(i => i.DrinkName == drink.Name);
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                var newItem = new ReceiptItem
                {
                    DrinkName = drink.Name,
                    Quantity = 1,
                    CurrentPrice = drink.CurrentPrice
                };
                newItem.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(ReceiptItem.TotalPrice))
                    {
                        UpdateTotalPrice();
                    }
                };
                Items.Add(newItem);
            }
            UpdateTotalPrice();
        }
        public void RemoveLastItem()
        {
            if (Items.Any())
            {
                var lastItem = Items.Last();
                if (lastItem.Quantity > 1)
                {
                    lastItem.Quantity--;

                }
                else
                {
                    Items.Remove(lastItem);
                }
                OnPropertyChanged(nameof(TotalPrice));
                OnPropertyChanged(nameof(TotalVakjes));
            }
        }


        private void UpdateTotalPrice()
        {
            OnPropertyChanged(nameof(TotalPrice));
            OnPropertyChanged(nameof(TotalVakjes));
        }
    }
}
