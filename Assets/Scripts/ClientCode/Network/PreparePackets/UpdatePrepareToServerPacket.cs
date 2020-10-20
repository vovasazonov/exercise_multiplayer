using System.Collections.Generic;
using Serialization;

namespace Network.PreparePackets
{
    public readonly struct UpdatePrepareToServerPacket : IPrepareToServerPacket
    {
        private readonly ISerializer _serializer;
        private readonly int _clientId;

        public UpdatePrepareToServerPacket(ISerializer serializer, int clientId)
        {
            _serializer = serializer;
            _clientId = clientId;
        }

        public byte[] GetPacket()
        {
            Queue<byte> packet = new Queue<byte>();
            packet.Enqueue(_serializer.Serialize(NetworkPacketType.Update));
            packet.Enqueue(_serializer.Serialize(_clientId));
            return packet.ToArray();
        }
    }
}