using System.Collections.Generic;
using System.Text.Json.Serialization;
using Cappuccino.Core.Network.Models.Groups;
using Cappuccino.Core.Network.Models.Users;
using Cappuccino.Core.Network.Models.Messages;

/* Mark: https://vk.com/dev/messages.getConversationsById */
namespace Cappuccino.Core.Network.Methods.Messages {

    public class GetConversationsById : ApiMethod<GetConversationsById.Response> {
        public GetConversationsById() : base("messages.getConversationsById") {}


        public IEnumerable<int> PeerIds { set => AddParam("peer_ids", value); }
        public int Extended { set => AddParam("extended", value); }
        public IEnumerable<string> Fields { set => AddParam("fields", value); }


        public class Response {
            [JsonPropertyName("response")] public InnerResponse? Inner { get; set; }

            public class InnerResponse {
                [JsonPropertyName("count")] public int Count { get; set; }
                [JsonPropertyName("items")] public List<Conversation>? Items { get; set; }
                [JsonPropertyName("profiles")] public List<User>? Profiles { get; set; }
                [JsonPropertyName("groups")] public List<Group>? Groups { get; set; }
            }
        }
    }
}