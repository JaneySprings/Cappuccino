using System.Text.Json.Serialization;
namespace Cappuccino.Core.Network.Models.Groups {

    /* 
     * Mark: documentation [https://vk.com/dev/objects/group] 
     */
    public class Group {
        // Base properties
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("name")] public string? Name { get; set; }
        [JsonPropertyName("screen_name")] public string? ScreenName { get; set; }
        [JsonPropertyName("type")] public string? Type { get; set; }
        [JsonPropertyName("is_closed")] public int IsClosed { get; set; }
        [JsonPropertyName("photo_50")] public string? Photo50 { get; set; }
        [JsonPropertyName("photo_100")] public string? Photo100 { get; set; }
        [JsonPropertyName("photo_200")] public string? Photo200 { get; set; }
        // Optional properties
    }
}