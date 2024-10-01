using System.Text.Json.Serialization;

namespace HubCar.Shared.Models
{
    public class Specification
    {
        [JsonPropertyName("vehicleType")]
        public string? VehicleType { get; set; }

        [JsonPropertyName("colour")]
        public string? Colour { get; set; }

        [JsonPropertyName("fuel")]
        public string? Fuel { get; set; }

        [JsonPropertyName("transmission")]
        public string? Transmission { get; set; }

        [JsonPropertyName("numberOfDoors")]
        public int NumberOfDoors { get; set; }

        [JsonPropertyName("co2Emissions")]
        public string? Co2Emissions { get; set; }

        [JsonPropertyName("noxEmissions")]
        public int NoxEmissions { get; set; }

        [JsonPropertyName("numberOfKeys")]
        public int NumberOfKeys { get; set; }
    }
}