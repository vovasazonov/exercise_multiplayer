namespace Server.Network.PacketClientHandlers
{
    public readonly struct ErrorClientPacketHandler : IClientPacketHandler
    {
        public void HandlePacket()
        {
            throw new System.NotImplementedException();
        }
    }
}