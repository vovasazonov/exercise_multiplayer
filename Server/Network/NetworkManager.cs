using System;
using System.Collections.Generic;
using Models;
using Network.PacketHandlers;
using Serialization;

namespace Network
{
    public class NetworkManager : IDisposable
    {
        private readonly IServer _server;
        private readonly ISerializer _serializer;
        private readonly IDictionary<uint, IClientProxy> _clientProxyDic = new Dictionary<uint, IClientProxy>();
        private readonly ServerGameProcessor _processor;
        
        public int MillisecondsTickServer
        {
            set => _processor.MillisecondsTick = value;
        }
        
        public NetworkManager(IServer server, ISerializer serializer)
        {
            _server = server;
            _serializer = serializer;
            
            _processor = new ServerGameProcessor(_clientProxyDic);
        }

        private void AddServerListener()
        {
            _server.PacketReceived += OnPacketReceived;
            _server.ClientConnect += OnClientConnect;
            _server.ClientDisconnect += OnClientDisconnect;
        }

        private void RemoveServerListener()
        {
            _server.PacketReceived -= OnPacketReceived;
            _server.ClientConnect -= OnClientConnect;
            _server.ClientDisconnect -= OnClientDisconnect;
        }

        private void OnClientDisconnect(object sender, PacketReceivedEventArgs packetReceivedEventArgs)
        {
            IPacketHandler packetHandler = new DisconnectPacketHandler(packetReceivedEventArgs.ClientId, _clientProxyDic);
            packetHandler.HandlePacket();
        }

        private void OnClientConnect(object sender, PacketReceivedEventArgs packetReceivedEventArgs)
        {
            IPacketHandler packetHandler = new ConnectPacketHandler(packetReceivedEventArgs.ClientId, _clientProxyDic, _serializer);
            packetHandler.HandlePacket();
        }

        private void OnPacketReceived(object sender, PacketReceivedEventArgs packetReceivedEventArgs)
        {
            IPacketHandler packetHandler = new CommandPacketHandler(packetReceivedEventArgs.ClientId, packetReceivedEventArgs.Packet, _clientProxyDic);
            packetHandler.HandlePacket();
        }

        public void Dispose()
        {
            _server?.Dispose();
            _processor?.Dispose();
        }
    }
}