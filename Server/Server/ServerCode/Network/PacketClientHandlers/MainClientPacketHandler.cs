using System.Collections.Generic;
using Game;
using Network;
using Serialization;

namespace Server.Network.PacketClientHandlers
{
    public readonly struct MainClientPacketHandler : IClientPacketHandler
    {
        private readonly Queue<byte> _packetCame;
        private readonly Queue<byte> _packetResponse;
        private readonly IDictionary<int, IClientProxy> _clientProxyDic;
        private readonly ModelManager _modelManager;
        private readonly ISerializer _serializer;

        public MainClientPacketHandler(Queue<byte> packetCame,Queue<byte> packetResponse,IDictionary<int, IClientProxy> clientProxyDic, ModelManager modelManager, ISerializer serializer)
        {
            _packetCame = packetCame;
            _packetResponse = packetResponse;
            _clientProxyDic = clientProxyDic;
            _modelManager = modelManager;
            _serializer = serializer;
        }

        public void HandlePacket()
        {
            NetworkPacketType networkPacketType = _serializer.Deserialize<NetworkPacketType>(_packetCame);
            IClientPacketHandler clientPacketHandler;

            switch (networkPacketType)
            {
                case NetworkPacketType.Hello:
                    clientPacketHandler = new HelloClientPacketHandler(_clientProxyDic, _serializer, _packetResponse, _modelManager);
                    break;
                case NetworkPacketType.Command:
                    clientPacketHandler = new CommandClientPacketHandler(_clientProxyDic, _serializer, _packetCame);
                    break;
                case NetworkPacketType.Update:
                    clientPacketHandler = new UpdateClientPacketHandler(_clientProxyDic, _serializer, _packetCame, _packetResponse);
                    break;
                default:
                    clientPacketHandler = new ErrorClientPacketHandler();
                    break;
            }

            clientPacketHandler.HandlePacket();
        }
    }
}