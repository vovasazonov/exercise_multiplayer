using System.Collections.Generic;
using Game;
using Network;
using Serialization;

namespace Server.Network.HandlePackets
{
    public readonly struct MainHandleClientPacket : IHandleClientPacket
    {
        private readonly Queue<byte> _packetCame;
        private readonly Queue<byte> _packetResponse;
        private readonly IDictionary<int, IClientProxy> _clientProxyDic;
        private readonly ModelManager _modelManager;
        private readonly ISerializer _serializer;

        public MainHandleClientPacket(Queue<byte> packetCame,Queue<byte> packetResponse,IDictionary<int, IClientProxy> clientProxyDic, ModelManager modelManager, ISerializer serializer)
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
            IHandleClientPacket handleClientPacket;

            switch (networkPacketType)
            {
                case NetworkPacketType.Hello:
                    handleClientPacket = new HelloHandleClientPacket(_clientProxyDic, _serializer, _packetResponse, _modelManager);
                    break;
                case NetworkPacketType.Command:
                    handleClientPacket = new CommandHandleClientPacket(_clientProxyDic, _serializer, _packetCame);
                    break;
                case NetworkPacketType.Update:
                    handleClientPacket = new UpdateHandleClientPacket(_clientProxyDic, _serializer, _packetCame, _packetResponse);
                    break;
                default:
                    handleClientPacket = new ErrorHandleClientPacket();
                    break;
            }

            handleClientPacket.HandlePacket();
        }
    }
}