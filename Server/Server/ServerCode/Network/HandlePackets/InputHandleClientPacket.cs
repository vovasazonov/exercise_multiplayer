using System.Collections.Generic;

namespace Server.Network.HandlePackets
{
    public struct InputHandleClientPacket : IHandleClientPacket
    {
        public byte[] Response(Queue<byte> packetCame)
        {
            // TODO process input client and put input data to client proxy for server can after tick process it.
            throw new System.NotImplementedException();
        }
    }
}