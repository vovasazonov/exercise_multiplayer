namespace Network
{
    public class ClientMessage : IClientMessage
    {
        public uint ClientId { get; }
        public MessageType MessageType { get; }
        public byte[] Packet { get; }

        public ClientMessage(uint clientId, MessageType messageType, byte[] packet = null)
        {
            MessageType = messageType;
            ClientId = clientId;
            Packet = packet;
        }
    }
}