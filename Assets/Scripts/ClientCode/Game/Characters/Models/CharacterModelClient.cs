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

        public CharacterModelClient(ICharacterModel characterModel, ClientNetworkInfo clientNetworkInfo, ISerializer serializer)
        {
            _characterModel = characterModel;
            _clientNetworkInfo = clientNetworkInfo;
            _serializer = serializer;
        }

        public int Id => _characterModel.Id;
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
            packet.Enqueue(_serializer.Serialize(Id));
            packet.Enqueue(_serializer.Serialize(weapon.Id));
        }
    }
}