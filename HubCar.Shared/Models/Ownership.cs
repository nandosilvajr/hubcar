using System.Text.Json.Serialization;

namespace HubCar.Shared.Models
{
    public class Ownership
    {
        [JsonPropertyName("logBook")]
        public string? LogBook { get; set; }

        [JsonPropertyName("numberOfOwners")]
        public int NumberOfOwners { get; set; }

        [JsonPropertyName("dateOfRegistration")]
        public string? DateOfRegistration { get; set; }
    }
}