using System.Text.Json.Serialization;

namespace Descriptions
{
    public class WeaponDescription : IWeaponDescription
    {
        [JsonPropertyName("id")] public string Id { get; set; }
        [JsonPropertyName("damage")] public int Damage { get; set; }
    }
}