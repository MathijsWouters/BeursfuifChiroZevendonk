using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeursfuifChiroZevendonk.Models
{
    public partial class Receipt : BaseModel
    {
        private ObservableCollection<ReceiptItem> _items = new ObservableCollection<ReceiptItem>();

        public ObservableCollection<ReceiptItem> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        public decimal TotalPrice => Items.Sum(item => item.TotalPrice);
        public decimal TotalVakjes => TotalPrice / 0.25m;
    }
}
