namespace Network
{
    public interface IClientProxy
    {
        uint Id { get; }
        IMutablePacket NotSentToClientPacket { get; }
        IMutablePacket UnprocessedReceivedPacket { get; }
    }
}