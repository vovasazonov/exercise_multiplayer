using System;
using System.Collections.Generic;
using Game;
using Network.ServerPacketHandlers.Commands;
using Serialization;

namespace Network.ServerPacketHandlers
{
    public readonly struct MainServerPacketHandler : IServerPacketHandler
    {
        private readonly Queue<byte> _packet;
        private readonly ISerializer _serializer;
        private readonly ClientNetworkInfo _clientNetworkInfo;
        private readonly IModelManager _modelManager;

        public MainServerPacketHandler(Queue<byte> packet, ISerializer serializer, ClientNetworkInfo clientNetworkInfo, ModelManagerClient modelManager)
        {
            _packet = packet;
            _serializer = serializer;
            _clientNetworkInfo = clientNetworkInfo;
            _modelManager = modelManager;
        }

        public void HandlePacket()
        {
            NetworkPacketType networkPacketType = _serializer.Deserialize<NetworkPacketType>(_packet);
            IServerPacketHandler serverPacketHandler;

            switch (networkPacketType)
            {
                case NetworkPacketType.Welcome:
                    serverPacketHandler = new WelcomeServerPacketHandler(_packet, _clientNetworkInfo, _serializer);
                    _clientNetworkInfo.ClientNetworkState = ClientNetworkState.Welcomed;
                    break;
                case NetworkPacketType.Update:
                    serverPacketHandler = new UpdateServerPacketHandler(_packet, _modelManager, _serializer);
                    break;
                default:
                    throw new ArgumentException();
            }

            serverPacketHandler.HandlePacket();
        }
    }
}