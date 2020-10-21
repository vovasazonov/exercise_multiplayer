using System.Collections.Generic;
using Serialization;

namespace Network.ServerPacketPreparers
{
    public readonly struct CommandServerPacketPreparer : IServerPacketPreparer
    {
        private readonly ISerializer _serializer;
        private readonly IClientNetworkInfo _clientNetworkInfo;

        public CommandServerPacketPreparer(ISerializer serializer, IClientNetworkInfo clientNetworkInfo)
        {
            _serializer = serializer;
            _clientNetworkInfo = clientNetworkInfo;
        }

        public byte[] GetPacket()
        {
            Queue<byte> bytes = new Queue<byte>();
            bytes.Enqueue(_serializer.Serialize(NetworkPacketType.Command));
            bytes.Enqueue(_serializer.Serialize(_clientNetworkInfo.Id));
            bytes.Enqueue(_clientNetworkInfo.NotSentCommandsToServer.ToArray());

            _clientNetworkInfo.NotSentCommandsToServer.Clear();
            
            return bytes.ToArray();
        }
    }
}