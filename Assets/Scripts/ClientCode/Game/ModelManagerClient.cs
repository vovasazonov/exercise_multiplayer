using System.Collections.Generic;
using Game.Characters.Models;
using Game.Weapons.Models;
using Network;
using Serialization;

namespace Game
{
    public class ModelManagerClient
    {
        public readonly Dictionary<int, ICharacterModel> CharacterModelDic;
        public readonly Dictionary<int, IWeaponModel> WeaponModelDic;
        
        public ModelManagerClient(ClientNetworkInfo clientNetworkInfo, ISerializer serializer)
        {
            var modelManager = new ModelManager();

            CharacterModelDic = new Dictionary<int, ICharacterModel>();
            foreach (var keyValuePair in modelManager.CharacterModelDic)
            {
                var model = new CharacterModelClient(keyValuePair.Value, keyValuePair.Key, clientNetworkInfo, serializer);
                CharacterModelDic.Add(keyValuePair.Key,model);
            }

            WeaponModelDic = modelManager.WeaponModelDic;
        }
    }
}