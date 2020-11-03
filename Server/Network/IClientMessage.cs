namespace Network
{
    public interface IClientMessage
    {
        uint ClientId { get; }
        MessageType MessageType { get; }
        byte[] Packet { get; }
    }
}