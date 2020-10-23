using System.Collections.Generic;
using UnityEngine;

namespace Descriptions
{
    public class DescriptionManagerClient : IDescriptionManager
    {
        private readonly string _pathWithDescriptions = @"JsonDescriptions/";
        private List<CharacterDescription> _characterDescriptionsList;
        private List<WeaponDescription> _weaponDescriptionsList;

        public IEnumerable<ICharacterDescription> CharacterDescriptionsList => _characterDescriptionsList;
        public IEnumerable<IWeaponDescription> WeaponDescriptionsList => _weaponDescriptionsList;

        public DescriptionManagerClient()
        {
            Load();
        }
        
        private void Load()
        {
            LoadCharacterDescriptions();
            LoadWeaponDescriptions();
        }

        private void LoadCharacterDescriptions()
        {
            var jsonString = Resources.Load<TextAsset>(_pathWithDescriptions + "DatabaseCharacterDescription");
            var deserializableJsonString = GetJsonStringToDeserialize(jsonString.ToString());
            _characterDescriptionsList = JsonUtility.FromJson<List<CharacterDescription>>(deserializableJsonString);
        }

        private void LoadWeaponDescriptions()
        {
            var jsonString = Resources.Load<TextAsset>(_pathWithDescriptions + "DatabaseWeaponDescription");
            _weaponDescriptionsList = JsonUtility.FromJson<List<WeaponDescription>>(jsonString.ToString());
        }

        private string GetJsonStringToDeserialize(string jsonString)
        {
            return "{\"CharacterDescription\": " + jsonString + "}";
        }
    }
}