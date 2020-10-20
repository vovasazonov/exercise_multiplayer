using System.Collections.Generic;
using Network;
using Serialization;

namespace Server.Network.HandlePackets
{
    public readonly struct UpdateHandleClientPacket : IHandleClientPacket
    {
        private readonly Dictionary<int, ClientProxy> _clients;
        private readonly ISerializer _serializer;
        private readonly Queue<byte> _packetCame;
        private readonly Queue<byte> _packetResponse;

        public UpdateHandleClientPacket(Dictionary<int, ClientProxy> clients, ISerializer serializer, Queue<byte> packetCame, Queue<byte> packetResponse)
        {
            _clients = clients;
            _serializer = serializer;
            _packetCame = packetCame;
            _packetResponse = packetResponse;
        }

        public void HandlePacket()
        {
            int idClient = _serializer.Deserialize<int>(_packetCame);

            if (_clients.ContainsKey(idClient))
            {
                _packetResponse.Enqueue(_serializer.Serialize(NetworkPacketType.Update));
                _packetResponse.Enqueue(_clients[idClient].NotSentCommand.ToArray());
                _clients[idClient].NotSentCommand.Clear();
            }
        }
    }
}