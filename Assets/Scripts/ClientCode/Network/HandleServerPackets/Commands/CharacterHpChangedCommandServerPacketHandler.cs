using System.Collections.Generic;
using Game;
using Serialization;

namespace Network.HandleServerPackets.Commands
{
    public readonly struct CharacterHpChangedCommandServerPacketHandler : IServerPacketHandler
    {
        private readonly Queue<byte> _packCame;
        private readonly ModelManagerClient _modelManagerClient;
        private readonly ISerializer _serializer;

        public CharacterHpChangedCommandServerPacketHandler(Queue<byte> packCame, ModelManagerClient modelManagerClient, ISerializer serializer)
        {
            _packCame = packCame;
            _modelManagerClient = modelManagerClient;
            _serializer = serializer;
        }

        public void HandlePacket()
        {
            int characterId = _serializer.Deserialize<int>(_packCame);
            int characterHp = _serializer.Deserialize<int>(_packCame);

            _modelManagerClient.CharacterModelDic[characterId].HealthPoint.Points = characterHp;
        }
    }
}