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
        public Action OnBackspacePressed { get; set; }

        public KeyboardService()
        {
            this.hook = new TaskPoolGlobalHook();
            this.hook.KeyPressed += OnKeyPressed;
            Debug.WriteLine("KeyboardService initialized and hook attached.");


        }

        private void OnKeyPressed(object sender, KeyboardHookEventArgs e)
        {
            if (e.Data.KeyCode == KeyCode.VcBackspace) 
            {
                Debug.WriteLine("backspace key pressed.");
                OnBackspacePressed?.Invoke();
            }
        }

        public void Start()
        {
            try
            {
                this.hook.RunAsync();
                Debug.WriteLine("Hook started.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error starting hook: {ex.Message}");
            }
        }

        public void Stop()
        {
            try
            {
                this.hook.Dispose();
                Debug.WriteLine("Hook stopped.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error stopping hook: {ex.Message}");
            }
        }

    }
#endif
}

