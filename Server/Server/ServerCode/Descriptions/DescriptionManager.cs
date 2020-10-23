using System.Collections.Generic;
using System.IO;
using Descriptions;
using System.Text.Json;

namespace Server.Descriptions
{
    public class DescriptionManager : IDescriptionManager
    {
        private readonly string _pathWithDescriptions = @"..\..\..\JsonDescriptions\";
        private List<CharacterDescription> _characterDescriptionsList;
        private List<WeaponDescription> _weaponDescriptionsList;

        public IEnumerable<ICharacterDescription> CharacterDescriptionsList => _characterDescriptionsList;
        public IEnumerable<IWeaponDescription> WeaponDescriptionsList => _weaponDescriptionsList;

        public DescriptionManager()
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
            string jsonString = File.ReadAllText(_pathWithDescriptions + "DatabaseCharacterDescription.json");
            _characterDescriptionsList = JsonSerializer.Deserialize<List<CharacterDescription>>(jsonString);
        }

        private void LoadWeaponDescriptions()
        {
            string jsonString = File.ReadAllText(_pathWithDescriptions + "DatabaseWeaponDescription.json");
            _weaponDescriptionsList = JsonSerializer.Deserialize<List<WeaponDescription>>(jsonString);
        }
    }
}