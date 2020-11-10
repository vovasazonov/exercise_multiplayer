using System.Collections.Generic;
using Models;

namespace Network.PacketHandlers
{
    public readonly struct DisconnectPacketHandler : IPacketHandler
    {
        private readonly uint _clientId;
        private readonly IDictionary<uint, IClientProxy> _clientProxyDic;
        private readonly IModelManager _modelManager;

        public DisconnectPacketHandler(in uint clientId, IDictionary<uint, IClientProxy> clientProxyDic, IModelManager modelManager)
        {
            _clientId = clientId;
            _clientProxyDic = clientProxyDic;
            _modelManager = modelManager;
        }

        public void HandlePacket()
        {
            _clientProxyDic.Remove(_clientId);
            _modelManager.PlayerModelDic.Remove((int)_clientId);
            
            
            foreach (var clientProxy in _clientProxyDic.Values)
            {
                clientProxy.NotSentToClientPacket.Fill(GameCommandType.PlayerDisconnected);
                clientProxy.NotSentToClientPacket.Fill(_clientId);
            }
        }
    }
}