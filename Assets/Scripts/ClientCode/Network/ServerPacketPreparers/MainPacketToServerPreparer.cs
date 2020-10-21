using System;
using Serialization;

namespace Network.ServerPacketPreparers
{
    public readonly struct MainPacketToServerPreparer : IPacketToServerPreparer
    {
        private readonly ClientNetworkInfo _clientNetworkInfo;
        private readonly ISerializer _serializer;

        public MainPacketToServerPreparer(ClientNetworkInfo clientNetworkInfo, ISerializer serializer)
        {
            _clientNetworkInfo = clientNetworkInfo;
            _serializer = serializer;
        }

        public byte[] GetPacket()
        {
            IPacketToServerPreparer packetToServerPreparer;
            switch (_clientNetworkInfo.NetworkState)
            {
                case NetworkClientState.Welcomed:
                    if (_clientNetworkInfo.NotSentCommandsToServer.Count > 0)
                    {
                        packetToServerPreparer = new CommandPacketToServerPreparer(_serializer,_clientNetworkInfo);
                    }
                    else
                    {
                        packetToServerPreparer = new UpdatePacketToServerPreparer(_serializer, _clientNetworkInfo.Id);
                    }
                    break;
                case NetworkClientState.SayingHello:
                    packetToServerPreparer = new HelloPacketToServerPreparer(_serializer);
                    break;
                default:
                    throw new ArgumentException();
            }

            return packetToServerPreparer.GetPacket();
        }
    }
}