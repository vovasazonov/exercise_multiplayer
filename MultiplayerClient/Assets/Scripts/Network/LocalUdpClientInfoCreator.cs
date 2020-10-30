namespace Network
{
    public class LocalUdpClientInfoCreator : IUdpClientInfoCreator
    {
        public UdpClientInfo Create()
        {
            UdpClientInfo localUdpClientInfo = new UdpClientInfo
            {
                ServerIp = "127.0.0.1",
                ServerPort = 3000,
                ChannelId = 0
            };

            return localUdpClientInfo;
        }
    }
}