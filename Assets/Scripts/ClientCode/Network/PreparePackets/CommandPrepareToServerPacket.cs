using System.Collections.Generic;
using Serialization;

namespace Network.PreparePackets
{
    public readonly struct CommandPrepareToServerPacket : IPrepareToServerPacket
    {
        private readonly ISerializer _serializer;
        private readonly ClientNetworkInfo _clientNetworkInfo;

        public CommandPrepareToServerPacket(ISerializer serializer, ClientNetworkInfo clientNetworkInfo)
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

            return bytes.ToArray();
        }
    }
}