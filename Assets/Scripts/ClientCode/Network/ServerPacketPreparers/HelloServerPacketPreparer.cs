using Serialization;

namespace Network.ServerPacketPreparers
{
    public readonly struct HelloServerPacketPreparer : IServerPacketPreparer
    {
        private readonly ISerializer _serializer;

        public HelloServerPacketPreparer(ISerializer serializer)
        {
            _serializer = serializer;
        }

        public byte[] GetPacket()
        {
            return _serializer.Serialize(NetworkPacketType.Hello);
        }
    }
}