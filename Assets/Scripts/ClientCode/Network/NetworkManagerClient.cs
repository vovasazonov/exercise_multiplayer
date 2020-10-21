using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game;
using Network.Clients;
using Network.ServerPackerHandlers;
using Network.ServerPacketPreparers;
using Serialization;

namespace Network
{
    public class NetworkManagerClient : IDisposable
    {
        private readonly int _millisecondsBetweenSendPacket = 1000;
        private readonly IClient _client;
        private readonly ISerializer _serializer;
        private readonly ModelManagerClient _modelManagerClient;
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
            IServerPacketHandler serverPacketHandler = new MainServerPacketHandler(packet, _serializer, _clientNetworkInfo, _modelManagerClient);
            serverPacketHandler.HandlePacket();
        }

        private async void StartSendOutgoingPacket()
        {
            Queue<byte> outgoingPacket = new Queue<byte>();

            while (_isNetWorkingWork)
            {
                IServerPacketPreparer serverPacketPreparer;
                switch (_clientNetworkInfo.NetworkState)
                {
                    case NetworkClientState.Welcomed:
                        if (_clientNetworkInfo.NotSentCommandsToServer.Count > 0)
                        {
                            serverPacketPreparer = new CommandServerPacketPreparer(_serializer,_clientNetworkInfo);
                        }
                        else
                        {
                            serverPacketPreparer = new UpdateServerPacketPreparer(_serializer, _clientNetworkInfo.Id);
                        }
                        break;
                    case NetworkClientState.SayingHello:
                        serverPacketPreparer = new HelloServerPacketPreparer(_serializer);
                        break;
                    default:
                        throw new ArgumentException();
                }

                outgoingPacket.Enqueue(serverPacketPreparer.GetPacket());
                
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