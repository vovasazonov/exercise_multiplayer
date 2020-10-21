using System;
using System.Collections.Generic;
using Game;
using Serialization;

namespace Network.ServerPacketHandlers.Commands
{
    public readonly struct UpdateServerPacketHandler : IServerPacketHandler
    {
        private readonly Queue<byte> _packetCame;
        private readonly IModelManager _modelManager;
        private readonly ISerializer _serializer;

        public UpdateServerPacketHandler(Queue<byte> packetCame, IModelManager modelManager, ISerializer serializer)
        {
            _packetCame = packetCame;
            _modelManager = modelManager;
            _serializer = serializer;
        }

        public void HandlePacket()
        {
            while (_packetCame.Count > 0)
            {
                GameCommandType gameCommandType = _serializer.Deserialize<GameCommandType>(_packetCame);
                IServerPacketHandler serverPacketHandler;
                
                switch (gameCommandType)
                {
                    case GameCommandType.CharacterHpChanged:
                        serverPacketHandler = new CharacterHpChangedCommandServerPacketHandler(_packetCame, _modelManager, _serializer);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                serverPacketHandler.HandlePacket();
            }
        }
    }
}