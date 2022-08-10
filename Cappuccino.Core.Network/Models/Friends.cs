using System.Text.Json.Serialization;
using System.Collections.Generic;
using Cappuccino.Core.Network.Models.Users;
namespace Cappuccino.Core.Network.Models.Friends {

    /* 
     * Mark: documentation [https://vk.com/dev/friends.get]
     */
    public class GetResponse {
        [JsonPropertyName("response")] public Response? InnerResponse { get; set; }

        public class Response {
            [JsonPropertyName("count")] public int Count { get; set; }
            [JsonPropertyName("items")] public List<User>? Items { get; set; }
        }
    }

    /* 
     * Mark: documentation [https://vk.com/dev/friends.search]
     */
    public class SearchResponse {
        [JsonPropertyName("response")] public Response? InnerResponse { get; set; }

        public class Response {
            [JsonPropertyName("count")] public int Count { get; set; }
            [JsonPropertyName("items")] public List<User>? Items { get; set; }
        }
    }

}

