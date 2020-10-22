using System;
using System.Collections.Generic;

namespace Server.Network
{
    public interface IServer
    {
        event Action<Queue<byte>, Queue<byte>> PacketCame;
        void Start();
        void Stop();
    }
}