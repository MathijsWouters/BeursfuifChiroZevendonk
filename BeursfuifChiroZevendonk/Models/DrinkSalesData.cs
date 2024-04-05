using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeursfuifChiroZevendonk.Models
{
    public partial class DrinkSalesData : BaseModel
    {
        public string DrinkName { get; set; }
        public int TotalSold { get; set; }
        public decimal TotalIncome { get; set; }
    }
}
