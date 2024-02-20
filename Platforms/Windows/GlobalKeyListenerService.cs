using Beursfuif.Services;

namespace Beursfuif.Platforms.Windows
{
    public class GlobalKeyListenerService : IGlobalKeyListenerService
    {
        private Hook hook;

        public GlobalKeyListenerService()
        {
            this.hook = Hook.GlobalEvents();

            hook.KeyPressed += OnKeyPressed;
        }

        public void StartListening() => hook.Start();

        public void StopListening() => hook.Stop();

        private void OnKeyPressed(object sender, KeyPressedEventArgs e)
        {
            // Handle the key press event, e.KeyCode gives you the pressed key
            Console.WriteLine($"Key Pressed: {e.KeyCode}");
            // Implement your logic here based on the key pressed
        }
    }
}
