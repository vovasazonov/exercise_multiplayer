using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Descriptions
{
    [Serializable]
    public class CharacterDescription : ICharacterDescription
    {
        [FormerlySerializedAs("id")] [SerializeField]
        private string _id;
        [FormerlySerializedAs("health_points")] [SerializeField]
        private int _healthPoints;
        
        public string Id => _id;
        public int HealthPoints => _healthPoints;
    }
}