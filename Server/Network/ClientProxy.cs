using Serialization;

namespace Network
{
    public class ClientProxy : IClientProxy
    {
        public uint Id { get; }
        public IMutablePacket NotSentToClientPacket { get; }
        public IMutablePacket UnprocessedReceivedPacket { get; }

        public ClientProxy(uint id, ISerializer serializer)
        {
            Id = id;
            
            NotSentToClientPacket = new MutablePacket(serializer);
            UnprocessedReceivedPacket = new MutablePacket(serializer);
        }
    }
}