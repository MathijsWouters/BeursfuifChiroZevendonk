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
        private List<decimal> historicalPrices = new List<decimal>();
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
            GenerateFakeHistoricalData();
            HistoricalPrices.Add(currentPrice);
        }
        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.PropertyName == nameof(MinPrice) || e.PropertyName == nameof(MaxPrice))
            {
                StartingPrice = CalculateStartingPrice();
                CurrentPrice = StartingPrice;
                GenerateFakeHistoricalData();
            }
            if (e.PropertyName == nameof(CurrentPrice))
            {
                LogCurrentPrice();
            }
        }
        private decimal CalculateStartingPrice()
        {
            return RoundToNearestQuarter((MinPrice + MaxPrice) / 2);
        }

        private static decimal RoundToNearestQuarter(decimal number)
            => Math.Round(number * 4, MidpointRounding.AwayFromZero) / 4;
        public void ClearHistoricalPrices()
        {
            HistoricalPrices.Clear();
        }
        public void LogCurrentPrice()
        {
            HistoricalPrices.Add(CurrentPrice);
        }
        public void GenerateFakeHistoricalData()
        {
            HistoricalPrices.Clear(); 
            for (int i = 12; i >= 0; i--)
            {
                var fakePrice = GenerateFakePrice(MinPrice, MaxPrice);
                HistoricalPrices.Add(fakePrice);
            }
        }
        private decimal GenerateFakePrice(decimal minPrice, decimal maxPrice)
        {
            var random = new Random();
            var range = (double)(maxPrice - minPrice);
            var fakePrice = minPrice + (decimal)(random.NextDouble() * range);
            return RoundToNearestQuarter(fakePrice);
        }
    }
}
