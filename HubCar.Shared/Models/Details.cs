using System.Text.Json.Serialization;

namespace HubCar.Shared.Models
{
    public class Details
    {
        [JsonPropertyName("specification")]
        public Specification? Specification { get; set; }

        [JsonPropertyName("ownership")]
        public Ownership? Ownership { get; set; }

        [JsonPropertyName("equipment")]
        public List<string>? Equipment { get; set; }
    }
}