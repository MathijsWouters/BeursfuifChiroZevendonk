using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beursfuif.Models
{
    public class Drink
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public decimal CurrentPrice { get; set; }

        [System.Text.Json.Serialization.JsonIgnore] 
        public Color DrinkColor { get; set; }

        public string DrinkColorHex
        {
            get => DrinkColor.ToHex();
            set => DrinkColor = Color.FromArgb(value);
        }

        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal StartingPrice { get; private set; }
        public Drink(decimal minPrice, decimal maxPrice)
        {
            MinPrice = minPrice;
            MaxPrice = maxPrice;
            StartingPrice = (MinPrice + MaxPrice) / 2;
            CurrentPrice = StartingPrice;
        }
    }
}

