using System;
using System.Threading.Tasks;
using ENet;
#if UNITY_EDITOR
using UnityEngine;
#endif
using Event = ENet.Event;

namespace Network
{
    public class UdpClient : IClient
    {
        public event EventHandler<PacketReceivedEventArgs> ClientConnected;
        public event EventHandler<PacketReceivedEventArgs> ClientDisconnected;
        public event EventHandler<PacketReceivedEventArgs> PacketReceived;

        private readonly Host _client;
        private readonly Peer _peerServer;
        private readonly UdpClientInfo _udpClientInfo;
        private bool _isLoopTask;
        private readonly Task _loopTask;

        public UdpClient(UdpClientInfo udpClientInfo)
        {
            _udpClientInfo = udpClientInfo;

            ENet.Library.Initialize();
            _client = new Host();
            Address address = new Address();
            address.SetHost(udpClientInfo.ServerIp);
            address.Port = udpClientInfo.ServerPort;
            _client.Create();
#if UNITY_EDITOR
            Debug.Log("Connecting");
#endif
            _peerServer = _client.Connect(address);
            _isLoopTask = true;
            _loopTask = Task.Factory.StartNew(Loop);
        }

        public void SendPacket(byte[] packetBytes)
        {
            var packet = default(Packet);
            packet.Create(packetBytes);
            _peerServer.Send(_udpClientInfo.ChannelId, ref packet);
        }

        private void Loop()
        {
            try
            {
                while (_isLoopTask)
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
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        private void HandleReceiveEvent(ref Event netEvent)
        {
#if UNITY_EDITOR
            Debug.Log("Packet received from server - Channel ID: " + netEvent.ChannelID + ", Data length: " +
                      netEvent.Packet.Length);
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
            ClientConnected?.Invoke(this, e);
        }

        private void OnClientDisconnect(PacketReceivedEventArgs e)
        {
            ClientDisconnected?.Invoke(this, e);
        }

        public void Dispose()
        {
            _client.Dispose();
            _isLoopTask = false;
            _loopTask.Wait();
            ENet.Library.Deinitialize();
        }
    }
}