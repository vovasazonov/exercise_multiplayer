using System;
using System.Collections.Generic;
using Game;
using Serialization;

namespace Network.HandleServerPackets.Commands
{
    public readonly struct UpdateHandleServerPacket : IHandleServerPacket
    {
        private readonly Queue<byte> _packetCame;
        private readonly ModelManagerClient _modelManagerClient;
        private readonly ISerializer _serializer;

        public UpdateHandleServerPacket(Queue<byte> packetCame, ModelManagerClient modelManagerClient, ISerializer serializer)
        {
            _packetCame = packetCame;
            _modelManagerClient = modelManagerClient;
            _serializer = serializer;
        }

        public void HandlePacket()
        {
            while (_packetCame.Count > 0)
            {
                GameCommandType gameCommandType = _serializer.Deserialize<GameCommandType>(_packetCame);
                IHandleServerPacket handleServerPacket;
                
                switch (gameCommandType)
                {
                    case GameCommandType.CharacterHpChanged:
                        handleServerPacket = new CharacterHpChangedCommandHandleServerPacket(_packetCame, _modelManagerClient, _serializer);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                handleServerPacket.HandlePacket();
            }
        }
    }
}