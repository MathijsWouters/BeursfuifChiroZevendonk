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
        public Color DrinkColor { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal StartingPrice => (MinPrice + MaxPrice) / 2;
    }
}
