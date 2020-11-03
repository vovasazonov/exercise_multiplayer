using System;
using System.Threading.Tasks;

namespace Network
{
    public class TickSystem : ITickSystem
    {
        public event EventHandler TickStart;

        private bool _isRunSystem;
        
        public int MillisecondsTick { private get; set; }

        public void Start()
        {
            _isRunSystem = true;
            StartLoopAsync();
        }

        public void Stop()
        {
            _isRunSystem = false;
        }
        
        private async void StartLoopAsync()
        {
            while (_isRunSystem)
            {
                OnTickStart();
                await Task.Delay(MillisecondsTick);
            }
        }

        private void OnTickStart()
        {
            TickStart?.Invoke(this, EventArgs.Empty);
        }
    }
}