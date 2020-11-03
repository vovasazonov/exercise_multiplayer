using System;

namespace Network
{
    public interface ITickSystem
    {
        public event EventHandler TickStart;
        public int MillisecondsTick { set; }

        public void Start();
        public void Stop();
    }
}