﻿using System.Collections.Generic;

namespace Network.PacketHandlers
{
    public readonly struct DisconnectPacketHandler : IPacketHandler
    {
        private readonly uint _clientId;
        private readonly IDictionary<uint, IClientProxy> _clientProxyDic;

        public DisconnectPacketHandler(in uint clientId, IDictionary<uint, IClientProxy> clientProxyDic)
        {
            _clientId = clientId;
            _clientProxyDic = clientProxyDic;
        }

        public void HandlePacket()
        {
            _clientProxyDic.Remove(_clientId);
        }
    }
}