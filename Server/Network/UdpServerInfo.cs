namespace Network
{
    public struct UdpServerInfo
    {
        public ushort Port;
        public int MaxClients;
        public byte ChannelId;
        public uint PeerTimeOutLimit;
        public uint PeerTimeOutMinimum;
        public uint PeerTimeOutMaximum;
    }
}