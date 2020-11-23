namespace Network
{
    public interface IClientProxy
    {
        uint Id { get; }
        bool IsFirstWhole { get; set; }
        IDataMutablePacket NotSentToClientPacket { get; }
        IDataMutablePacket UnprocessedReceivedPacket { get; }
    }
}