using System;
using System.Collections.Generic;
using Serialization;

namespace Network.PacketHandlers
{
    public readonly struct PacketHandler : IPacketHandler
    {
        private readonly IClientMessage _message;
        private readonly ISerializer _serializer;
        private readonly ITrackableDictionary<uint, IClientProxy> _clientProxyDic;

        public PacketHandler(IClientMessage message, ISerializer serializer, ITrackableDictionary<uint, IClientProxy> clientProxyDic)
        {
            _message = message;
            _serializer = serializer;
            _clientProxyDic = clientProxyDic;
        }

        public void HandlePacket()
        {
            IPacketHandler packetHandler;
                
            switch (_message.MessageType)
            {
                case MessageType.Connect:
                    packetHandler = new ConnectPacketHandler(_message.ClientId, _clientProxyDic, _serializer);
                    break;
                case MessageType.Disconnect:
                    packetHandler = new DisconnectPacketHandler(_message.ClientId, _clientProxyDic);
                    break;
                case MessageType.Data:
                    packetHandler = new DataPacketHandler(_message.ClientId, _message.Packet, _clientProxyDic);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
                
            packetHandler.HandlePacket();
        }
    }
}