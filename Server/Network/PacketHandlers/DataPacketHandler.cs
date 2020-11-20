using System.Collections.Generic;

namespace Network.PacketHandlers
{
    public readonly struct DataPacketHandler : IPacketHandler
    {
        private readonly uint _clientId;
        private readonly byte[] _packet;
        private readonly IDictionary<uint, IClientProxy> _clientProxyDic;

        public DataPacketHandler(in uint clientId, byte[] packet, IDictionary<uint, IClientProxy> clientProxyDic)
        {
            _clientId = clientId;
            _packet = packet;
            _clientProxyDic = clientProxyDic;
        }

        public void HandlePacket()
        {
            _clientProxyDic[_clientId].UnprocessedReceivedPacket.FillCombinedData(_packet);
        }
    }
}