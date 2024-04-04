using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeursfuifChiroZevendonk.Models
{
    public partial class Drink : BaseModel
    {
        [ObservableProperty]
        private int number;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private decimal currentPrice;

        [ObservableProperty]
        private decimal minPrice;

        [ObservableProperty]
        private decimal maxPrice;

        [ObservableProperty]
        private decimal startingPrice;

        [System.Text.Json.Serialization.JsonIgnore]
        public Color DrinkColor { get; set; }
        public string DrinkColorHex
        {
            get => DrinkColor.ToHex();
            set => DrinkColor = Color.FromArgb(value);
        }
        public Drink(decimal minPrice, decimal maxPrice)
        {
            MinPrice = minPrice;
            MaxPrice = maxPrice;
            StartingPrice = CalculateStartingPrice();
            CurrentPrice = StartingPrice;
        }
        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.PropertyName == nameof(MinPrice) || e.PropertyName == nameof(MaxPrice))
            {
                StartingPrice = CalculateStartingPrice();
                CurrentPrice = StartingPrice; 
            }
        }
        private decimal CalculateStartingPrice()
        {
            return RoundToNearestQuarter((MinPrice + MaxPrice) / 2);
        }

        private static decimal RoundToNearestQuarter(decimal number)
            => Math.Round(number * 4, MidpointRounding.AwayFromZero) / 4;
    }
}
