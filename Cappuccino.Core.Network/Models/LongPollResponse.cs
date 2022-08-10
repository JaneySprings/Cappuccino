using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Cappuccino.Core.Network.Models {
    public class LongPollResponse {
        [JsonPropertyName("ts")] public int Ts { get; set; }
        [JsonPropertyName("updates")] public List<List<object>>? Updates { get; set; }
    }
}