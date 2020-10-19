using System;
using System.Collections.Generic;
using System.Linq;
using Network;
using Serialization;

namespace Server.Network.HandlePackets
{
    public struct HelloHandleClientPacket : IHandleClientPacket
    {
        private readonly Dictionary<int, ClientProxy> _clients;
        private readonly ISerializer _serializer;
        private int _lastSetId;

        public HelloHandleClientPacket(Dictionary<int, ClientProxy> clients, ISerializer serializer)
        {
            _clients = clients;
            _serializer = serializer;
            _lastSetId = Int32.MaxValue;
        }
        
        public byte[] Response(Queue<byte> packetCame)
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