using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeursfuifChiroZevendonk.Models
{
    public partial class ReceiptItem : BaseModel
    {
        private int _quantity;
        private decimal _currentPrice;

        public string DrinkName { get; set; }

        public int Quantity
        {
            get => _quantity;
            set
            {
                if (SetProperty(ref _quantity, value))
                {
                    OnPropertyChanged(nameof(TotalPrice));
                }
            }
        }

        public decimal CurrentPrice
        {
            get => _currentPrice;
            set
            {
                if (SetProperty(ref _currentPrice, value))
                {
                    OnPropertyChanged(nameof(TotalPrice));

                }
            }
        }

        public decimal TotalPrice => Quantity * CurrentPrice;
    }
}
