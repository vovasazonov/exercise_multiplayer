using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game;
using Network.Clients;
using Network.HandleServerPackets;
using Network.HandleServerPackets.Commands;
using Network.PreparePackets;
using Serialization;

namespace Network
{
    public class NetworkManagerClient : IDisposable
    {
        private readonly int _millisecondsBetweenSendPacket = 1000;
        private readonly IClient _client;
        private readonly ISerializer _serializer;
        private readonly ModelManagerClient _modelManagerClient;
        private NetworkClientState _networkState = NetworkClientState.SayingHello;
        private readonly ClientNetworkInfo _clientNetworkInfo;
        private bool _isNetWorkingWork = true;

        public NetworkManagerClient(IClient client, ClientNetworkInfo clientNetworkInfo, ISerializer serializer, ModelManagerClient modelManagerClient)
        {
            _client = client;
            _clientNetworkInfo = clientNetworkInfo;
            _serializer = serializer;
            _modelManagerClient = modelManagerClient;

            AddClientListener();
            StartSendOutgoingPacket();
        }

        private void AddClientListener()
        {
            _client.ServerPacketCame += OnServerPacketCame;
        }

        private void RemoveClientListener()
        {
            _client.ServerPacketCame -= OnServerPacketCame;
        }

        private void OnServerPacketCame(Queue<byte> packet)
        {
            HandlePacket(packet);
        }

        private void HandlePacket(Queue<byte> packet)
        {
            NetworkPacketType networkPacketType = _serializer.Deserialize<NetworkPacketType>(packet);
            IServerPacketHandler serverPacketHandler;
            
            switch (networkPacketType)
            {
                case NetworkPacketType.Welcome:
                    serverPacketHandler = new WelcomeServerPacketHandler(packet,_clientNetworkInfo,_serializer);
                    _networkState = NetworkClientState.Welcomed;
                    break;
                case NetworkPacketType.Update:
                    serverPacketHandler = new UpdateServerPacketHandler(packet,_modelManagerClient,_serializer);
                    break;
                default:
                    throw new ArgumentException();
            }
            
            serverPacketHandler.HandlePacket();
        }

        private async void StartSendOutgoingPacket()
        {
            Queue<byte> outgoingPacket = new Queue<byte>();

            while (_isNetWorkingWork)
            {
                IPrepareToServerPacket prepareToServerPacket;
                switch (_networkState)
                {
                    case NetworkClientState.Welcomed:
                        if (_clientNetworkInfo.NotSentCommandsToServer.Count > 0)
                        {
                            prepareToServerPacket = new CommandPrepareToServerPacket(_serializer,_clientNetworkInfo);
                        }
                        else
                        {
                            prepareToServerPacket = new UpdatePrepareToServerPacket(_serializer, _clientNetworkInfo.Id);
                        }
                        break;
                    case NetworkClientState.SayingHello:
                        prepareToServerPacket = new HelloPrepareToServerPacket(_serializer);
                        break;
                    default:
                        throw new ArgumentException();
                }

                outgoingPacket.Enqueue(prepareToServerPacket.GetPacket());
                
                if (outgoingPacket.Count > 0)
                {
                    _client.SendPacket(outgoingPacket);
                    outgoingPacket.Clear();
                }
                
                await Task.Delay(_millisecondsBetweenSendPacket);
            }
        }

        public void Dispose()
        {
            _isNetWorkingWork = false;
            RemoveClientListener();
        }
    }
}