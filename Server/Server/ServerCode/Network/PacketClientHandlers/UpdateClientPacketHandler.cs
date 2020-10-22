using System.Collections.Generic;
using Network;
using Serialization;

namespace Server.Network.PacketClientHandlers
{
    public readonly struct UpdateClientPacketHandler : IClientPacketHandler
    {
        private readonly IDictionary<int, IClientProxy> _clients;
        private readonly ISerializer _serializer;
        private readonly Queue<byte> _packetCame;
        private readonly Queue<byte> _packetResponse;

        public UpdateClientPacketHandler(IDictionary<int, IClientProxy> clients, ISerializer serializer, Queue<byte> packetCame, Queue<byte> packetResponse)
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
                _packetResponse.Enqueue(_clients[idClient].NotSentCommands.ToArray());
                _clients[idClient].NotSentCommands.Clear();
            }
        }
    }
}