using System;
using System.Collections.Generic;

namespace Network.Clients
{
    public interface IClient
    {
        event Action<Queue<byte>> ServerPacketCame;
        void SendPacket(Queue<byte> packet);
    }
}