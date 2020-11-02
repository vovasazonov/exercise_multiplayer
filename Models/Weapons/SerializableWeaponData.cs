using System.Runtime.Serialization;

namespace Models.Weapons
{
    [DataContract]
    public class SerializableWeaponData
    {
        [DataMember] public string Id { get; set; }
        [DataMember] public uint Damage { get; set; }
    }
}