using System.Text.Json.Serialization;

/* Mark: https://vk.com/dev/account.getCounters */
namespace Cappuccino.Core.Network.Methods.Account {

    public class GetCounters : ApiMethod<GetCounters.Response> {
        public GetCounters() : base("account.getCounters") {}
        
        
        public int UserId { set => AddParam("user_id", value); }
        public string Filter { set => AddParam("filter", value); }



        public class Response {
            [JsonPropertyName("response")] public InnerResponse? Inner { get; set; }

            public class InnerResponse {
                [JsonPropertyName("friends")] public int Friends { get; set; }
                [JsonPropertyName("friends_suggestions")] public int FriendsSuggestions { get; set; }
                [JsonPropertyName("messages")] public int Messages { get; set; }
                [JsonPropertyName("photos")] public int Photos { get; set; }
                [JsonPropertyName("videos")] public int Videos { get; set; }
                [JsonPropertyName("gifts")] public int Gifts { get; set; }
                [JsonPropertyName("friends_recommendations")] public int FriendsRecommendations { get; set; }
                [JsonPropertyName("menu_discover_badge")] public int MenuDiscoverBadge { get; set; }
                [JsonPropertyName("menu_clips_badge")] public int MenuClipBadge { get; set; }
                [JsonPropertyName("menu_superapp_friends_badge")] public int MenuSuperAppFriendsBadge { get; set; }
                [JsonPropertyName("menu_new_clips_badge")] public int MenuNewClipBadgeBadge { get; set; }
                [JsonPropertyName("calls")] public int Calls { get; set; }
            }
        }
    }
}