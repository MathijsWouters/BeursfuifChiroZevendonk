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

        // Keep the Color property for internal use
        [System.Text.Json.Serialization.JsonIgnore] // Ignore this property for JSON serialization
        public Color DrinkColor { get; set; }

        // Use this property for serialization
        public string DrinkColorHex
        {
            get => DrinkColor.ToHex();
            set => DrinkColor = Color.FromArgb(value);
        }

        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal StartingPrice => (MinPrice + MaxPrice) / 2;
        public Drink()
        {
            CurrentPrice = StartingPrice;
        }
    }
}

