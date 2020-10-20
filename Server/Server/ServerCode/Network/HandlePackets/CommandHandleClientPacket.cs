using System.Collections.Generic;
using Serialization;

namespace Server.Network.HandlePackets
{
    public readonly struct CommandHandleClientPacket : IHandleClientPacket
    {
        private readonly ISerializer _serializer;
        private readonly Queue<byte> _packetCame;
        private Dictionary<int, ClientProxy> ClientProxyDic { get; }

        public CommandHandleClientPacket(Dictionary<int, ClientProxy> clientProxyDic, ISerializer serializer, Queue<byte> packetCame)
        {
            _serializer = serializer;
            _packetCame = packetCame;
            ClientProxyDic = clientProxyDic;
        }

        public void HandlePacket()
        {
            int idClient = _serializer.Deserialize<int>(_packetCame);
            ClientProxyDic[idClient].UnprocessedCommand.Enqueue(_packetCame.ToArray());
        }
    }
}