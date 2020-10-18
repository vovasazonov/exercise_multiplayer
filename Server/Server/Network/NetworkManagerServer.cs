using System;
using System.Collections.Generic;
using System.Linq;
using Network;
using Serialization;

namespace Server.Network
{
    public class NetworkManagerServer
    {
        private readonly IServer _server;
        private int _lastSetId = Int32.MaxValue;
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
            
            switch (networkPacketType)
            {
                case NetworkPacketType.Hello:
                    packetResponse.Enqueue(ResponsePacketFromNewClient());
                    break;
            }
        }

        private byte[] ResponsePacketFromNewClient()
        {
            Queue<byte> responsePacket = new Queue<byte>();
            bool newClientSet = false;
            int fuse = _lastSetId;
            
            while (!newClientSet && ++_lastSetId != fuse)
            {
                if (!_clients.Keys.Contains(_lastSetId))
                {
                    var clientProxy = new ClientProxy(_lastSetId);
                    _clients[_lastSetId] = clientProxy;
                    responsePacket = PrepareWelcomePacket(clientProxy);
                    newClientSet = true;
                }
            }

            return responsePacket.ToArray();
        }

        private Queue<byte> PrepareWelcomePacket(ClientProxy clientProxy)
        {
            Queue<byte> responsePacket = new Queue<byte>();
            responsePacket.Enqueue(_serializer.Serialize(NetworkPacketType.Welcome));
            responsePacket.Enqueue(_serializer.Serialize(clientProxy.IdClient));

            return responsePacket;
        }
    }
}