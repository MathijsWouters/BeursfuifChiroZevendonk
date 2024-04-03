using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Beursfuif.Models
{
    public class Drink : INotifyPropertyChanged
    {
        private int _number;
        private string _name;
        private decimal _currentPrice;
        private decimal _minPrice;
        private decimal _maxPrice;
        private decimal _startingPrice;
        // Vermoed dat Color een eigenschapstype is dat je elders definieert
        [System.Text.Json.Serialization.JsonIgnore]
        public Color DrinkColor { get; set; }
        public string DrinkColorHex
        {
            get => DrinkColor.ToHex();
            set => DrinkColor = Color.FromArgb(value);
        }
        public int Number
        {
            get => _number;
            set => SetProperty(ref _number, value);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public decimal CurrentPrice
        {
            get => _currentPrice;
            set => SetProperty(ref _currentPrice, value);
        }

        public decimal MinPrice
        {
            get => _minPrice;
            set
            {
                if (SetProperty(ref _minPrice, value))
                {
                    UpdatePrices(MinPrice, MaxPrice);
                }
            }
        }

        public decimal MaxPrice
        {
            get => _maxPrice;
            set
            {
                if (SetProperty(ref _maxPrice, value))
                {
                    UpdatePrices(MinPrice, MaxPrice);
                }
            }
        }

        public decimal StartingPrice
        {
            get => _startingPrice;
            private set => SetProperty(ref _startingPrice, value);
        }

        public Drink(decimal minPrice, decimal maxPrice)
        {
            _minPrice = minPrice;
            _maxPrice = maxPrice;
            UpdatePrices(minPrice, maxPrice);
        }

        public void UpdatePrices(decimal newMinPrice, decimal newMaxPrice)
        {
            _minPrice = newMinPrice;
            _maxPrice = newMaxPrice;

            var newStartingPrice = RoundToNearestQuarter((_minPrice + _maxPrice) / 2);
            if (StartingPrice != newStartingPrice)
            {
                StartingPrice = newStartingPrice;
                CurrentPrice = StartingPrice; 
            }
        }


        private decimal RoundToNearestQuarter(decimal number) => Math.Round(number * 4, MidpointRounding.AwayFromZero) / 4;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
        {
            if (Equals(storage, value))
            {
                return false;
            }
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}

