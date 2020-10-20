using Serialization;

namespace Network.PreparePackets
{
    public readonly struct HelloPrepareToServerPacket : IPrepareToServerPacket
    {
        private readonly ISerializer _serializer;

        public HelloPrepareToServerPacket(ISerializer serializer)
        {
            _serializer = serializer;
        }

        public byte[] GetPacket()
        {
            return _serializer.Serialize(NetworkPacketType.Hello);
        }
    }
}