using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeursfuifChiroZevendonk.Models
{
    public partial class FiveMinuteDrinkSalesData : BaseModel
    {
        public string DrinkName { get; set; }
        public int QuantitySoldLastFiveMinutes { get; set; }
    }
}
