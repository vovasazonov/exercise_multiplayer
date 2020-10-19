using System.Collections.Generic;
using Network;
using Serialization;

namespace Server.Network.HandlePackets
{
    public readonly struct ErrorHandleClientPacket : IHandleClientPacket
    {
        private readonly ISerializer _serializer;

        public ErrorHandleClientPacket(ISerializer serializer)
        {
            _serializer = serializer;
        }

        public byte[] Response(Queue<byte> packetCame)
        {
            return _serializer.Serialize(NetworkPacketType.ErrorPacket);
        }
    }
}