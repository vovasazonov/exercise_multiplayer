using Serialization;

namespace Network
{
    public class ClientProxy : IClientProxy
    {
        public uint Id { get; }
        public bool IsFirstWhole { get; set; } = true;
        public IDataMutablePacket NotSentToClientPacket { get; }
        public IDataMutablePacket UnprocessedReceivedPacket { get; }

        public ClientProxy(uint id, ISerializer serializer)
        {
            Id = id;
            
            NotSentToClientPacket = new DataMutablePacket(serializer);
            UnprocessedReceivedPacket = new DataMutablePacket(serializer);
        }
    }
}