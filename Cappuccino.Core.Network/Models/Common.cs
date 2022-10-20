using System.Text.Json.Serialization;

namespace Cappuccino.Core.Network.Models {

    public class Photo {
        [JsonPropertyName("photo_200")] public string? Photo200 { get; set; }
        [JsonPropertyName("photo_100")] public string? Photo100 { get; set; }
        [JsonPropertyName("photo_50")] public string? Photo50 { get; set; }
    }
}