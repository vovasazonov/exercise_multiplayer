using System;
using System.Collections.Generic;
using Game;
using Network;
using Serialization;
using Serialization.BinaryFormatterSerialization;
using Server.Network.HandlePackets;

namespace Server.Network
{
    public class NetworkManagerServer : IDisposable
    {
        private readonly IServer _server;
        private readonly ModelManager _modelManager;
        private readonly Dictionary<int, IClientProxy> _clientProxyDic = new Dictionary<int, IClientProxy>();
        private readonly ISerializer _serializer = new BinaryFormatterSerializer();
        private readonly int _millisecondsTick = 500;
        private readonly IClientCommandsProcessor _clientCommandsProcessor;

        public NetworkManagerServer(IServer server, ModelManager modelManager)
        {
            _server = server;
            _modelManager = modelManager;
            _clientCommandsProcessor = new ClientCommandsProcessor(modelManager, _clientProxyDic, _serializer, _millisecondsTick);

            AddServerListener();
            _clientCommandsProcessor.Start();
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
                    handleClientPacket = new HelloHandleClientPacket(_clientProxyDic, _serializer, packetResponse, _modelManager);
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
            _clientCommandsProcessor.Stop();
            RemoveServerListener();
        }
    }
}