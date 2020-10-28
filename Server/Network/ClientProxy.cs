using Serialization;

namespace Network
{
    public class ClientProxy : IClientProxy
    {
        public uint Id { get; }
        public ICustomPacket UnprocessedReceivedPacket { get; }
        public ICustomPacket NotSentToClientPacket { get; }

        public ClientProxy(uint id, ISerializer serializer)
        {
            Id = id;
            
            UnprocessedReceivedPacket = new CustomPacket(serializer);
            NotSentToClientPacket = new CustomPacket(serializer);
        }
    }
}