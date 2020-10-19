using System;
using System.Collections.Generic;

namespace Server.Network
{
    public interface IServer
    {
        event Action<Queue<byte>, Queue<byte>> ClientPacketCame;
        void Start();
        void Stop();
    }
}