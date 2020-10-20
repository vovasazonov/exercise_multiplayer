using System.Collections.Generic;
using System.Linq;
using Game;
using Network;
using Serialization;

namespace Server.Network.Commands
{
    public readonly struct HitCharacterHandleCommand : IHandleCommand
    {
        private readonly Queue<byte> _packet;
        private readonly ModelManager _modelManager;
        private readonly Dictionary<int, ClientProxy> _clientProxyDic;
        private readonly int _clientId;
        private readonly ISerializer _serializer;

        public HitCharacterHandleCommand(Queue<byte> packet, ModelManager modelManager, Dictionary<int, ClientProxy> clientProxyDic, int clientId, ISerializer serializer)
        {
            _packet = packet;
            _modelManager = modelManager;
            _clientProxyDic = clientProxyDic;
            _clientId = clientId;
            _serializer = serializer;
        }

        public void HandleCommand()
        {
            int characterId = _serializer.Deserialize<int>(_packet);
            int weaponId = _serializer.Deserialize<int>(_packet);

            var weapon = _modelManager.WeaponModels.First(w => w.Id == weaponId);
            var character = _modelManager.EnemyModel;
            
            character.HitMe(weapon);

            NotifyClients(characterId);
        }

        private void NotifyClients(int characterId)
        {
            foreach (var clientProxy in _clientProxyDic.Values)
            {
                clientProxy.NotSentCommand.Enqueue(_serializer.Serialize(GameCommandType.CharacterHpChanged));
                clientProxy.NotSentCommand.Enqueue(_serializer.Serialize(_modelManager.EnemyModel.Id));
                clientProxy.NotSentCommand.Enqueue(_serializer.Serialize(_modelManager.EnemyModel.HealthPoint.Points));
            }
        }
    }
}