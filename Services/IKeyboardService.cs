using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beursfuif.Services
{
    public interface IKeyboardService
    {
        void RegisterKeyPressHandler(Action<int> onKeyPress);
    }
}
