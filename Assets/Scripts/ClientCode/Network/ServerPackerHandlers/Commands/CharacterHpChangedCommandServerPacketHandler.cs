using System.Collections.Generic;
using Game;
using Serialization;

namespace Network.ServerPackerHandlers.Commands
{
    public readonly struct CharacterHpChangedCommandServerPacketHandler : IServerPacketHandler
    {
        private readonly Queue<byte> _packCame;
        private readonly IModelManager _modelManagerClient;
        private readonly ISerializer _serializer;

        public CharacterHpChangedCommandServerPacketHandler(Queue<byte> packCame, IModelManager modelManagerClient, ISerializer serializer)
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