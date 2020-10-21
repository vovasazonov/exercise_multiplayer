using Serialization;

namespace Network.ServerPacketPreparers
{
    public readonly struct HelloPacketToServerPreparer : IPacketToServerPreparer
    {
        private readonly ISerializer _serializer;

        public HelloPacketToServerPreparer(ISerializer serializer)
        {
            _serializer = serializer;
        }

        public byte[] GetPacket()
        {
            return _serializer.Serialize(NetworkPacketType.Hello);
        }
    }
}