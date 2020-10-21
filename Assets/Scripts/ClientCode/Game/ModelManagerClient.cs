using System.Collections.Generic;
using Game.Characters.Models;
using Game.Weapons.Models;
using Network;
using Serialization;

namespace Game
{
    public class ModelManagerClient
    {
        public readonly Dictionary<int, ICharacterModel> CharacterModelDic = new Dictionary<int, ICharacterModel>();
        public readonly Dictionary<int, IWeaponModel> WeaponModelDic = new Dictionary<int, IWeaponModel>();
        
        public ModelManagerClient(ClientNetworkInfo clientNetworkInfo, ISerializer serializer)
        {
            var modelManager = new ModelManager();

            foreach (var keyValuePair in modelManager.CharacterModelDic)
            {
                var model = new CharacterModelClient(keyValuePair.Value, keyValuePair.Key, clientNetworkInfo, serializer);
                CharacterModelDic.Add(keyValuePair.Key,model);
            }

            WeaponModelDic = modelManager.WeaponModelDic;
        }
    }
}