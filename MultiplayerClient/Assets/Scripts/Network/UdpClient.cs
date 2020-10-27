using System;
using ENet;
#if UNITY_EDITOR
using UnityEngine;
#endif
using Event = ENet.Event;
using Task = System.Threading.Tasks.Task;

namespace Network
{
    public class UdpClient : IClient
    {
        public event EventHandler<PacketReceivedEventArgs> ClientConnect;
        public event EventHandler<PacketReceivedEventArgs> ClientDisconnect;
        public event EventHandler<PacketReceivedEventArgs> PacketReceived;

        private readonly Host _client;
        private readonly Peer _peerServer;
        private readonly Task _clientLoopTask;
        private readonly byte _channelId;
        private bool _isRunning;

        public UdpClient(string serverIp = "127.0.0.1", ushort serverPort = 3000, byte channelId = 0)
        {
            ENet.Library.Initialize();
            _channelId = channelId;
            _client = new Host();
            Address address = new Address();
            address.SetHost(serverIp);
            address.Port = serverPort;
            _client.Create();
#if UNITY_EDITOR
            Debug.Log("Connecting");
#endif
            _peerServer = _client.Connect(address);

            _isRunning = true;
            _clientLoopTask = new Task(StartClientLoop);
            _clientLoopTask.Start();
        }

        public void SendPacket(byte[] packetBytes)
        {
            var packet = default(Packet);
            packet.Create(packetBytes);
            _peerServer.Send(_channelId, ref packet);
        }

        private void StartClientLoop()
        {
            while (_isRunning)
            {
                bool hasEventInQueue = _client.CheckEvents(out var netEvent) <= 0;
                bool isGetEvent = hasEventInQueue && _client.Service(15, out netEvent) > 0;

                if (isGetEvent)
                {
                    switch (netEvent.Type)
                    {
                        case ENet.EventType.None:
                            break;

                        case ENet.EventType.Connect:
                            HandleConnectEvent(netEvent);
                            break;

                        case ENet.EventType.Disconnect:
                            HandleDisconnectEvent(netEvent);
                            break;

                        case ENet.EventType.Timeout:
                            HandleTimeoutEvent(netEvent);
                            break;

                        case ENet.EventType.Receive:
                            HandleReceiveEvent(ref netEvent);
                            netEvent.Packet.Dispose();
                            break;
                    }
                }
            }
        }

        private void HandleReceiveEvent(ref Event netEvent)
        {
#if UNITY_EDITOR
            Debug.Log("Packet received from server - Channel ID: " + netEvent.ChannelID + ", Data length: " + netEvent.Packet.Length);
#endif
            byte[] packetBytes = new byte[netEvent.Packet.Length];
            netEvent.Packet.CopyTo(packetBytes);
            var packetReceivedEventArgs = new PacketReceivedEventArgs(packetBytes);
            OnPacketReceived(packetReceivedEventArgs);

            netEvent.Packet.Dispose();
        }

        private void HandleTimeoutEvent(Event netEvent)
        {
#if UNITY_EDITOR
            Debug.Log("Client connection timeout");
#endif
            HandleDisconnectClient(netEvent);
        }

        private void HandleDisconnectEvent(Event netEvent)
        {
#if UNITY_EDITOR
            Debug.Log("Client disconnected from server");
#endif
            HandleDisconnectClient(netEvent);
        }

        private void HandleDisconnectClient(Event netEvent)
        {
            var packetReceivedEventArgs = new PacketReceivedEventArgs();
            OnClientDisconnect(packetReceivedEventArgs);
        }

        private void HandleConnectEvent(Event netEvent)
        {
#if UNITY_EDITOR
            Debug.Log("Client connected to server - ID: " + _peerServer.ID);
#endif
            var packetReceivedEventArgs = new PacketReceivedEventArgs();
            OnClientConnect(packetReceivedEventArgs);
        }

        private void OnPacketReceived(PacketReceivedEventArgs packetReceivedEventArgs)
        {
            PacketReceived?.Invoke(this, packetReceivedEventArgs);
        }

        private void OnClientConnect(PacketReceivedEventArgs e)
        {
            ClientConnect?.Invoke(this, e);
        }

        private void OnClientDisconnect(PacketReceivedEventArgs e)
        {
            ClientDisconnect?.Invoke(this, e);
        }

        public void Dispose()
        {
            _isRunning = false;
            _clientLoopTask.Wait();

            ENet.Library.Deinitialize();
            _client.Dispose();
        }
    }
}