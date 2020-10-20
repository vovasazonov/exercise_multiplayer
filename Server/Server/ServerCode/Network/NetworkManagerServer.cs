using System;
using System.Collections.Generic;
using Game;
using Network;
using Serialization;
using Server.Network.HandlePackets;

namespace Server.Network
{
    public class NetworkManagerServer : IDisposable
    {
        private readonly IServer _server;
        private readonly Dictionary<int, ClientProxy> _clientProxyDic = new Dictionary<int, ClientProxy>();
        private readonly ISerializer _serializer = new BinaryFormatterSerializer();
        
        private readonly GameProcessor _gameProcessor;

        public NetworkManagerServer(IServer server, ModelManager modelManager)
        {
            _server = server;
            _gameProcessor = new GameProcessor(modelManager, _clientProxyDic, _serializer);

            AddServerListener();
        }

        private void AddServerListener()
        {
            _server.ClientPacketCame += OnClientPacketCame;
        }

        private void RemoveServerListener()
        {
            _server.ClientPacketCame -= OnClientPacketCame;
        }

        private void OnClientPacketCame(Queue<byte> packetCame, Queue<byte> packetResponse)
        {
            ResponseClientPacket(packetCame, packetResponse);
        }

        private void ResponseClientPacket(Queue<byte> packetCame, Queue<byte> packetResponse)
        {
            NetworkPacketType networkPacketType = _serializer.Deserialize<NetworkPacketType>(packetCame);
            IHandleClientPacket handleClientPacket;

            switch (networkPacketType)
            {
                case NetworkPacketType.Hello:
                    handleClientPacket = new HelloHandleClientPacket(_clientProxyDic, _serializer, packetCame, packetResponse);
                    break;
                case NetworkPacketType.Command:
                    handleClientPacket = new CommandHandleClientPacket(_clientProxyDic, _serializer, packetCame);
                    break;
                case NetworkPacketType.Update:
                    handleClientPacket = new UpdateHandleClientPacket(_clientProxyDic, _serializer, packetCame, packetResponse);
                    break;
                default:
                    handleClientPacket = new ErrorHandleClientPacket();
                    break;
            }

            handleClientPacket.HandlePacket();
        }

        public void Dispose()
        {
            RemoveServerListener();
        }
    }
}