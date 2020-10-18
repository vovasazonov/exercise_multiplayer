using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Network.Clients;
using Serialization;
using UnityEngine;

namespace Network
{
    public class NetworkManagerClient : IDisposable
    {
        private readonly IClient _client;
        private readonly ISerializer _serializer = new BinaryFormatterSerializer();
        private NetworkClientState _networkState = NetworkClientState.SayingHello;
        private int _clientId;

        public NetworkManagerClient(IClient client)
        {
            _client = client;
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
            ProcessPacket(packet);
        }

        private void ProcessPacket(Queue<byte> packet)
        {
            NetworkPacketType networkPacketType = _serializer.Deserialize<NetworkPacketType>(packet);

            switch (networkPacketType)
            {
                case NetworkPacketType.Welcome:
                    HandleWelcomePacket(packet);
                    break;
            }
        }

        private void HandleWelcomePacket(Queue<byte> packet)
        {
            if (_networkState == NetworkClientState.SayingHello)
            {
                _clientId = _serializer.Deserialize<int>(packet);
                _networkState = NetworkClientState.Welcomed;
                
                #if UNITY_EDITOR
                Debug.Log($"Was welcomed as client id: {_clientId}");
                #endif
            }
        }

        private async void StartSendOutgoingPacket()
        {
            Queue<byte> outgoingPacket = new Queue<byte>();
            
            while (true)
            {
                switch (_networkState)
                {
                    case NetworkClientState.SayingHello:
                        outgoingPacket.Enqueue(PrepareHelloPacket());
                        break;
                    default:
                        throw new ArgumentException();
                }

                if (outgoingPacket.Count > 0)
                {
                    _client.SendPacket(outgoingPacket);
                    outgoingPacket.Clear();
                }
                
                await Task.Delay(5000);
            }
        }

        private byte[] PrepareHelloPacket()
        {
            return _serializer.Serialize(NetworkPacketType.Hello);
        }

        public void Dispose()
        {
            RemoveClientListener();
        }
    }
}