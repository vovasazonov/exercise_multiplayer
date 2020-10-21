using System.Collections.Generic;
using Serialization;

namespace Server.Network.PacketClientHandlers
{
    public readonly struct CommandClientPacketHandler : IClientPacketHandler
    {
        private readonly ISerializer _serializer;
        private readonly Queue<byte> _packetCame;
        private IDictionary<int, IClientProxy> ClientProxyDic { get; }

        public CommandClientPacketHandler(IDictionary<int, IClientProxy> clientProxyDic, ISerializer serializer, Queue<byte> packetCame)
        {
            _serializer = serializer;
            _packetCame = packetCame;
            ClientProxyDic = clientProxyDic;
        }

        public void HandlePacket()
        {
            int idClient = _serializer.Deserialize<int>(_packetCame);
            if (ClientProxyDic.ContainsKey(idClient))
            {
                ClientProxyDic[idClient].UnprocessedCommands.Enqueue(_packetCame.ToArray());
            }
        }
    }
}