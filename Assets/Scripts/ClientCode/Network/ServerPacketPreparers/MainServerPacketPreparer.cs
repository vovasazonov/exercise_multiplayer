using System;
using Serialization;

namespace Network.ServerPacketPreparers
{
    public readonly struct MainServerPacketPreparer : IServerPacketPreparer
    {
        private readonly ClientNetworkInfo _clientNetworkInfo;
        private readonly ISerializer _serializer;

        public MainServerPacketPreparer(ClientNetworkInfo clientNetworkInfo, ISerializer serializer)
        {
            _clientNetworkInfo = clientNetworkInfo;
            _serializer = serializer;
        }

        public byte[] GetPacket()
        {
            IServerPacketPreparer serverPacketPreparer;
            switch (_clientNetworkInfo.ClientNetworkState)
            {
                case ClientNetworkState.Welcomed:
                    if (_clientNetworkInfo.NotSentCommandsToServer.Count > 0)
                    {
                        serverPacketPreparer = new CommandServerPacketPreparer(_serializer,_clientNetworkInfo);
                    }
                    else
                    {
                        serverPacketPreparer = new UpdateServerPacketPreparer(_serializer, _clientNetworkInfo.Id);
                    }
                    break;
                case ClientNetworkState.SayingHello:
                    serverPacketPreparer = new HelloServerPacketPreparer(_serializer);
                    break;
                default:
                    throw new ArgumentException();
            }

            return serverPacketPreparer.GetPacket();
        }
    }
}