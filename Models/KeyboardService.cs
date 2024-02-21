using System;
using SharpHook;
using SharpHook.Native;
using System.Diagnostics;


namespace Beursfuif.Models
{
#if WINDOWS
    public class KeyboardService
    {
        private IGlobalHook hook;

        public KeyboardService()
        {
            this.hook = new TaskPoolGlobalHook();

            this.hook.KeyPressed += OnKeyPressed;

        }

        private void OnKeyPressed(object sender, KeyboardHookEventArgs e)
        {
            if (e.Data.KeyCode == KeyCode.VcBackspace) 
            {
                Debug.WriteLine("backspace key pressed.");
            }
        }

        public void Start()
        {
            this.hook.Run();
        }

        public void Stop()
        {
            this.hook.Dispose();
        }
    }
#endif
}

