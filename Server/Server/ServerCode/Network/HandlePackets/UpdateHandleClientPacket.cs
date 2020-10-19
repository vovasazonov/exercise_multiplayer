using System.Collections.Generic;

namespace Server.Network.HandlePackets
{
    public struct UpdateHandleClientPacket : IHandleClientPacket
    {
        public byte[] Response(Queue<byte> packetCame)
        {
            // TODO: if server tick was send the data.
            throw new System.NotImplementedException();
        }
    }
}