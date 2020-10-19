using System.Collections.Generic;

namespace Server.Network.HandlePackets
{
    public interface IHandleClientPacket
    {
        byte[] Response(Queue<byte> packetCame);
    }
}