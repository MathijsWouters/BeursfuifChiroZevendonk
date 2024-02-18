using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beursfuif.Services;
using YourAppNamespace.Platforms.Windows;

[assembly: Dependency(typeof(KeyboardService))]
namespace YourAppNamespace.Platforms.Windows
{
    public class KeyboardService : IKeyboardService
    {
        public void RegisterKeyPressHandler(Action<int> onKeyPress)
        {
            // Implementation for Windows
            // You would need to add event handlers that listen to keypress events
            // and invoke the onKeyPress callback with the corresponding key code.
        }
    }
}
