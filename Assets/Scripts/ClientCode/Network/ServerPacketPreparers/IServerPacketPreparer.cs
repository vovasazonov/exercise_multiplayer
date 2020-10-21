namespace Network.ServerPacketPreparers
{
    public interface IServerPacketPreparer
    {
        byte[] GetPacket();
    }
}