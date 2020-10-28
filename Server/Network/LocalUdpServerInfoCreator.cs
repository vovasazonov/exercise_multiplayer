namespace Network
{
    public class LocalUdpServerInfoCreator : IUdpServerInfoCreator
    {
        public UdpServerInfo Create()
        {
            UdpServerInfo localUdpServerInfo = new UdpServerInfo
            {
                Port = 3000,
                ChannelId = 0, 
                MaxClients = 100, 
                PeerTimeOutLimit = 32, 
                PeerTimeOutMinimum = 1000, 
                PeerTimeOutMaximum = 4000
            };

            return localUdpServerInfo;
        }
    }
}