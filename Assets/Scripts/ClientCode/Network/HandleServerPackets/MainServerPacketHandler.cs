using System;
using System.Collections.Generic;
using Game;
using Network.HandleServerPackets.Commands;
using Serialization;

namespace Network.HandleServerPackets
{
    public readonly struct MainServerPacketHandler : IServerPacketHandler
    {
        private readonly Queue<byte> _packet;
        private readonly ISerializer _serializer;
        private readonly ClientNetworkInfo _clientNetworkInfo;
        private readonly ModelManagerClient _modelManagerClient;

        public MainServerPacketHandler(Queue<byte> packet, ISerializer serializer, ClientNetworkInfo clientNetworkInfo, ModelManagerClient modelManagerClient)
        {
            _packet = packet;
            _serializer = serializer;
            _clientNetworkInfo = clientNetworkInfo;
            _modelManagerClient = modelManagerClient;
        }

        public void HandlePacket()
        {
            NetworkPacketType networkPacketType = _serializer.Deserialize<NetworkPacketType>(_packet);
            IServerPacketHandler serverPacketHandler;

            switch (networkPacketType)
            {
                case NetworkPacketType.Welcome:
                    serverPacketHandler = new WelcomeServerPacketHandler(_packet, _clientNetworkInfo, _serializer);
                    _clientNetworkInfo.NetworkState = NetworkClientState.Welcomed;
                    break;
                case NetworkPacketType.Update:
                    serverPacketHandler = new UpdateServerPacketHandler(_packet, _modelManagerClient, _serializer);
                    break;
                default:
                    throw new ArgumentException();
            }

            serverPacketHandler.HandlePacket();
        }
    }
}