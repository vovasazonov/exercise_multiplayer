using System.Collections.Generic;
using Serialization;
namespace Network.ServerPacketHandlers
{
    public readonly struct WelcomeServerPacketHandler : IServerPacketHandler
    {
        private readonly Queue<byte> _packetCame;
        private readonly IClientNetworkInfo _clientNetworkInfo;
        private readonly ISerializer _serializer;

        public WelcomeServerPacketHandler(Queue<byte> packetCame, IClientNetworkInfo clientNetworkInfo, ISerializer serializer)
        {
            _packetCame = packetCame;
            _clientNetworkInfo = clientNetworkInfo;
            _serializer = serializer;
        }
        
        public void HandlePacket()
        {
            _clientNetworkInfo.Id = _serializer.Deserialize<int>(_packetCame);
        }
    }
}