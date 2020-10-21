using System.Collections.Generic;
using Serialization;

namespace Network.ServerPacketPreparers
{
    public readonly struct UpdatePacketToServerPreparer : IPacketToServerPreparer
    {
        private readonly ISerializer _serializer;
        private readonly int _clientId;

        public UpdatePacketToServerPreparer(ISerializer serializer, int clientId)
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