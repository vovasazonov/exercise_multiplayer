using System;
using System.Collections.Generic;
using Network;
using Serialization;
using Server.Network.HandlePackets;

namespace Server.Network
{
    public class NetworkManagerServer : IDisposable
    {
        private readonly IServer _server;
        private readonly Dictionary<int, ClientProxy> _clients = new Dictionary<int, ClientProxy>();
        private readonly ISerializer _serializer = new BinaryFormatterSerializer();

        public NetworkManagerServer(IServer server)
        {
            _server = server;

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
                    handleClientPacket = new HelloHandleClientPacket(_clients,_serializer);
                    break;
                case NetworkPacketType.Update:
                    handleClientPacket = new UpdateHandleClientPacket();
                    break;
                case NetworkPacketType.Input:
                    handleClientPacket = new InputHandleClientPacket();
                    break;
                default:
                    handleClientPacket = new ErrorHandleClientPacket(_serializer);
                    break;
            }
            
            packetResponse.Enqueue(handleClientPacket.Response(packetCame));
        }

        public void Dispose()
        {
            RemoveServerListener();
        }
    }
}