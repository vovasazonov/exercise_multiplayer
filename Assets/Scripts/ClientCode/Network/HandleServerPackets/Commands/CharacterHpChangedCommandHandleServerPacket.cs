using System.Collections.Generic;
using Game;
using Serialization;

namespace Network.HandlePackets
{
    public readonly struct CharacterHpChangedCommandHandleServerPacket : IHandleServerPacket
    {
        private readonly Queue<byte> _packCame;
        private readonly ModelManagerClient _modelManager;
        private readonly ISerializer _serializer;

        public CharacterHpChangedCommandHandleServerPacket(Queue<byte> packCame, ModelManagerClient modelManager, ISerializer serializer)
        {
            _packCame = packCame;
            _modelManager = modelManager;
            _serializer = serializer;
        }

        public void HandlePacket()
        {
            int characterId = _serializer.Deserialize<int>(_packCame);
            int characterHp = _serializer.Deserialize<int>(_packCame);

            _modelManager.EnemyModel.HealthPoint.Points = characterHp;
        }
    }
}