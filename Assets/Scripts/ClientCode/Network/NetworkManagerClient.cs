using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game;
using Network.Clients;
using Network.ServerPacketHandlers;
using Network.ServerPacketPreparers;
using Serialization;

namespace Network
{
    public class NetworkManagerClient : IDisposable
    {
        private readonly int _millisecondsBetweenSendPacket = 500;
        private readonly IClient _client;
        private readonly ISerializer _serializer;
        private readonly ModelManagerClient _modelManagerClient;
        private readonly ClientNetworkInfo _clientNetworkInfo;
        private bool _isNetWorkingRun = true;

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

            while (_isNetWorkingRun)
            {
                IServerPacketPreparer serverPacketPreparer = new MainServerPacketPreparer(_clientNetworkInfo,_serializer);

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
            _isNetWorkingRun = false;
            RemoveClientListener();
        }
    }
}