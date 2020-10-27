using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ENet;

namespace Server.Network
{
    public class UdpServer : IServer
    {
        public event EventHandler<PacketReceivedEventArgs> ClientConnect;
        public event EventHandler<PacketReceivedEventArgs> ClientDisconnect;
        public event EventHandler<PacketReceivedEventArgs> PacketReceived;

        private readonly ushort _port;
        private readonly int _maxClients;
        private readonly uint _peerTimeOutLimit;
        private readonly uint _peerTimeOutMinimum;
        private readonly uint _peerTimeOutMaximum;
        private bool _isRunningServerLoop;
        private readonly Task _serverLoopTask;
        private readonly IDictionary<uint, Peer> _clientConnectedDic = new Dictionary<uint, Peer>();

        public UdpServer(ushort port = 3000, int maxClients = 1000, uint peerTimeOutLimit = 32, uint peerTimeOutMinimum = 1000, uint peerTimeOutMaximum = 3000)
        {
            _port = port;
            _maxClients = maxClients;
            _peerTimeOutLimit = peerTimeOutLimit;
            _peerTimeOutMinimum = peerTimeOutMinimum;
            _peerTimeOutMaximum = peerTimeOutMaximum;

            ENet.Library.Initialize();
            _isRunningServerLoop = true;
            _serverLoopTask = new Task(StartServerLoop);
            _serverLoopTask.Start();
        }

        public void SendPacket(uint clientId, byte[] packetBytes)
        {
            Packet packet = default;
            packet.Create(packetBytes);

            _clientConnectedDic[clientId].Send(0, ref packet);
        }

        private void StartServerLoop()
        {
            using Host server = new Host();
            Address address = new Address {Port = _port};
            server.Create(address, _maxClients);

            Console.WriteLine($"Circle ENet Server started on port: {_port}");

            while (_isRunningServerLoop)
            {
                bool polled = false;

                while (!polled)
                {
                    if (server.CheckEvents(out var netEvent) <= 0)
                    {
                        if (server.Service(15, out netEvent) <= 0)
                            break;

                        polled = true;
                    }

                    switch (netEvent.Type)
                    {
                        case EventType.None:
                            break;

                        case EventType.Connect:
                            HandleConnectEvent(netEvent);
                            break;

                        case EventType.Disconnect:
                            HandleDisconnectEvent(netEvent);
                            break;

                        case EventType.Timeout:
                            HandleTimeoutEvent(netEvent);
                            break;

                        case EventType.Receive:
                            HandleReceiveEvent(ref netEvent);
                            break;
                    }
                }
            }

            server.Flush();
        }

        private void HandleReceiveEvent(ref Event netEvent)
        {
            Console.WriteLine("Packet received from - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP + ", Channel ID: " + netEvent.ChannelID + ", Data length: " + netEvent.Packet.Length);

            byte[] packetBytes = new byte[netEvent.Packet.Length];
            netEvent.Packet.CopyTo(packetBytes);
            var packetReceivedEventArgs = new PacketReceivedEventArgs(netEvent.Peer.ID, packetBytes);
            OnPacketReceived(packetReceivedEventArgs);

            netEvent.Packet.Dispose();
        }

        private void HandleTimeoutEvent(Event netEvent)
        {
            Console.WriteLine("Client timeout - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP);

            HandleClientDisconnect(netEvent);
        }

        private void HandleDisconnectEvent(Event netEvent)
        {
            Console.WriteLine("Client disconnected - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP);

            HandleClientDisconnect(netEvent);
        }

        private void HandleClientDisconnect(Event netEvent)
        {
            _clientConnectedDic.Remove(netEvent.Peer.ID);
            var clientEventArgs = new PacketReceivedEventArgs(netEvent.Peer.ID);
            OnClientDisconnect(clientEventArgs);
        }

        private void HandleConnectEvent(Event netEvent)
        {
            Console.WriteLine("Client connected - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP);

            netEvent.Peer.Timeout(_peerTimeOutLimit, _peerTimeOutMinimum, _peerTimeOutMaximum);
            _clientConnectedDic[netEvent.Peer.ID] = netEvent.Peer;
        }

        private void OnPacketReceived(PacketReceivedEventArgs packetReceivedEventArgs)
        {
            PacketReceived?.Invoke(this, packetReceivedEventArgs);
        }

        private void OnClientDisconnect(PacketReceivedEventArgs packetReceivedEventArgs)
        {
            ClientDisconnect?.Invoke(this, packetReceivedEventArgs);
        }

        private void OnClientConnect(PacketReceivedEventArgs packetReceivedEventArgs)
        {
            ClientConnect?.Invoke(this, packetReceivedEventArgs);
        }

        public void Dispose()
        {
            _isRunningServerLoop = false;
            _serverLoopTask.Wait();
            ENet.Library.Deinitialize();
        }
    }
}