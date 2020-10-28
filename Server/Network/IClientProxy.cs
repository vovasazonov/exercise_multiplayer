namespace Network
{
    public interface IClientProxy
    {
        uint Id { get; }
        ICustomPacket UnprocessedReceivedPacket { get; }
        ICustomPacket NotSentToClientPacket { get; }
    }
}