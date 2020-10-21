using Game.HealthPoints.Models;
using Game.Weapons.Models;
using Network;
using Serialization;
using System.Collections.Generic;

namespace Game.Characters.Models
{
    public class CharacterModelClient : ICharacterModel
    {
        private readonly ClientNetworkInfo _clientNetworkInfo;
        private readonly ISerializer _serializer;
        
        private readonly ICharacterModel _characterModel;
        private readonly int _objectId;

        public CharacterModelClient(ICharacterModel characterModel, int objectId, ClientNetworkInfo clientNetworkInfo, ISerializer serializer)
        {
            _characterModel = characterModel;
            _objectId = objectId;
            _clientNetworkInfo = clientNetworkInfo;
            _serializer = serializer;
        }

        public string Id => _characterModel.Id;
        public IHealthPointModel HealthPoint => _characterModel.HealthPoint;
        public void HitMe(IWeaponModel weapon)
        {
            _characterModel.HitMe(weapon);

            PrepareCommandToServer(weapon);
        }

        private void PrepareCommandToServer(IWeaponModel weapon)
        {
            var packet = _clientNetworkInfo.NotSentCommandsToServer;
            packet.Enqueue(_serializer.Serialize(GameCommandType.HitCharacter));
            packet.Enqueue(_serializer.Serialize(_objectId));
            packet.Enqueue(_serializer.Serialize(weapon.Id));
        }
    }
}