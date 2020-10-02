using UnityEngine;

namespace Descriptions
{
    [CreateAssetMenu(fileName = "CommonDatabase", menuName = "Database/CommonDatabase", order = 0)]
    public class Database : ScriptableObject
    {
        public DescriptionsWeapon DescriptionsWeapon;
    }
}