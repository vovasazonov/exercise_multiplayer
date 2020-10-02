using System.Collections.Generic;
using UnityEngine;

namespace Descriptions
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Database/DatabaseWeapon", order = 0)]
    public class DescriptionsWeapon : ScriptableObject
    {
        public List<DescriptionWeapon> Descriptions;
    }
}