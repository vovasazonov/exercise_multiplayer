using System.Text.Json.Serialization;

namespace Descriptions
{
    public class CharacterDescription : ICharacterDescription
    {
        [JsonPropertyName("id")] public string Id { get; set; }
        [JsonPropertyName("health_points")] public int HealthPoints { get; set; }
    }
}