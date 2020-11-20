using System.Collections.Generic;
using Models;
using Serialization;

namespace Network.PacketHandlers
{
    public readonly struct ConnectPacketHandler : IPacketHandler
    {
        private readonly uint _clientId;
        private readonly IDictionary<uint, IClientProxy> _clientProxyDic;
        private readonly ISerializer _serializer;

        public ConnectPacketHandler(in uint clientId, IDictionary<uint, IClientProxy> clientProxyDic, ISerializer serializer)
        {
            _clientId = clientId;
            _clientProxyDic = clientProxyDic;
            _serializer = serializer;
        }

        public void HandlePacket()
        {
            var newClientProxy = new ClientProxy(_clientId, _serializer);
            _clientProxyDic.Add(_clientId, newClientProxy);
        }
    }
}