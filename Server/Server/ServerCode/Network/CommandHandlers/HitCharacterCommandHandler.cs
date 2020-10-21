using System.Collections.Generic;
using System.Linq;
using Game;
using Network;
using Serialization;

namespace Server.Network.CommandHandlers
{
    public readonly struct HitCharacterCommandHandler : ICommandHandler
    {
        private readonly Queue<byte> _packetWithCommands;
        private readonly ModelManager _modelManager;
        private readonly IDictionary<int, IClientProxy> _clientProxyDic;
        private readonly ISerializer _serializer;

        public HitCharacterCommandHandler(Queue<byte> packetWithCommands, ModelManager modelManager, IDictionary<int, IClientProxy> clientProxyDic, ISerializer serializer)
        {
            _packetWithCommands = packetWithCommands;
            _modelManager = modelManager;
            _clientProxyDic = clientProxyDic;
            _serializer = serializer;
        }

        public void HandleCommand()
        {
            int characterId = _serializer.Deserialize<int>(_packetWithCommands);
            string weaponId = _serializer.Deserialize<string>(_packetWithCommands);

            var character = _modelManager.CharacterModelDic[characterId];
            var weapon = _modelManager.WeaponModelDic.Values.FirstOrDefault(w=>w.Id == weaponId);

            if (weapon != null)
            {
                character.HitMe(weapon);
            }
            
            NotifyClients(characterId);
        }

        private void NotifyClients(int characterId)
        {
            foreach (var clientProxy in _clientProxyDic.Values)
            {
                clientProxy.NotSentPacketCommands.Enqueue(_serializer.Serialize(GameCommandType.CharacterHpChanged));
                clientProxy.NotSentPacketCommands.Enqueue(_serializer.Serialize(characterId));
                clientProxy.NotSentPacketCommands.Enqueue(_serializer.Serialize(_modelManager.CharacterModelDic[characterId].HealthPoint.Points));
            }
        }
    }
}