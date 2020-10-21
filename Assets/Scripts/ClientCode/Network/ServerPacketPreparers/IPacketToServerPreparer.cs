namespace Network.ServerPacketPreparers
{
    public interface IPacketToServerPreparer
    {
        byte[] GetPacket();
    }
}