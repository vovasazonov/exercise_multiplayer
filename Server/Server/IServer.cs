using System;

namespace ServerHP
{
    public interface IServer
    {
        Func<string, string> ProcessRequest { set; }
        void Start();
        void Stop();
    }
}