using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeursfuifChiroZevendonk.Models
{
    public partial class BaseModel : ObservableObject
    {
        [ObservableProperty]
        private bool isBusy;
    }
}
