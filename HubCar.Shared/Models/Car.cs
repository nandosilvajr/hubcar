using System.Text.Json.Serialization;

namespace HubCar.Shared.Models
{
    public class Car {
        [JsonPropertyName("make")]
        public string? Make { get; set; }

        [JsonPropertyName("model")]
        public string? Model { get; set; }

        [JsonPropertyName("engineSize")]
        public string? EngineSize { get; set; }

        [JsonPropertyName("fuel")]
        public string? Fuel { get; set; }

        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("mileage")]
        public int Mileage { get; set; }

        [JsonPropertyName("auctionDateTime")]
        public DateTime AuctionDateTime { get; set; }

        [JsonPropertyName("startingBid")]
        public int StartingBid { get; set; }

        [JsonPropertyName("favourite")]
        public bool Favourite { get; set; }

        [JsonPropertyName("details")]
        public Details? Details { get; set; }

        public string? AuctionRemaining => ConvertTimeSpan(AuctionDateTime.Subtract(DateTime.Now));

        private string ConvertTimeSpan(TimeSpan timeSpan)
        {
            if(timeSpan.Days < 0)
                return string.Format("Auction was {0} days, {1} hours ago", timeSpan.Days * -1 , timeSpan.Hours * -1);
            
            return string.Format("Auction remaining in {0} Days and {1}", timeSpan.Days, timeSpan.Hours);
            
        }
        
    }
}