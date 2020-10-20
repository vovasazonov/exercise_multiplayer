using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game;
using Network.Clients;
using Network.HandlePackets;
using Network.PreparePackets;
using Serialization;

namespace Network
{
    public class NetworkManagerClient : IDisposable
    {
        private readonly int _millisecondsBetweenSendPacket = 500;
        private readonly IClient _client;
        private readonly ISerializer _serializer;
        private readonly ModelManagerClient _modelManagerClient;
        private NetworkClientState _networkState = NetworkClientState.SayingHello;
        private readonly ClientNetworkInfo _clientNetworkInfo;

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
            IHandleServerPacket handleServerPacket;
            
            switch (networkPacketType)
            {
                case NetworkPacketType.Welcome:
                    handleServerPacket = new WelcomeHandleServerPacket(packet,_clientNetworkInfo,_serializer);
                    _networkState = NetworkClientState.Welcomed;
                    break;
                case NetworkPacketType.Update:
                    handleServerPacket = new CommandHandleServerPacket(packet,_modelManagerClient,_serializer);
                    break;
                default:
                    handleServerPacket = new ErrorHandleServerPacket();
                    break;
            }
            
            handleServerPacket.HandlePacket();
        }

        private async void StartSendOutgoingPacket()
        {
            Queue<byte> outgoingPacket = new Queue<byte>();

            while (true)
            {
                IPrepareToServerPacket prepareToServerPacket;
                switch (_networkState)
                {
                    case NetworkClientState.Welcomed:
                        prepareToServerPacket = new CommandPrepareToServerPacket(_serializer,_clientNetworkInfo);
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
            RemoveClientListener();
        }
    }
}