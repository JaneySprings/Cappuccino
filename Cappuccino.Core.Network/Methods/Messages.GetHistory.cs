using System.Collections.Generic;
using System.Text.Json.Serialization;
using Cappuccino.Core.Network.Models.Groups;
using Cappuccino.Core.Network.Models.Users;
using Cappuccino.Core.Network.Models.Messages;

/* Mark: https://vk.com/dev/messages.getHistory */
namespace Cappuccino.Core.Network.Methods.Messages {

    public class GetHistory : ApiMethod<GetHistory.Response> {
        public GetHistory() : base("messages.getHistory") {}


        public int Offset { set => AddParam("offset", value); }
        public int Count { set => AddParam("count", value); }
        public int UserId { set => AddParam("user_id", value); }
        public int PeerId { set => AddParam("peer_id", value); }
        public int StartMessageId { set => AddParam("start_message_id", value); }
        public int Rev { set => AddParam("rev", value); }
        public int Extended { set => AddParam("extended", value); }
        public IEnumerable<string> Fields { set => AddParam("fields", value); }
        
        
        public class Response {
            [JsonPropertyName("response")] public InnerResponse? Inner { get; set; }

            public class InnerResponse {
                [JsonPropertyName("count")] public int Count { get; set; }
                [JsonPropertyName("items")] public List<Message>? Items { get; set; }
                [JsonPropertyName("profiles")] public List<User>? Profiles { get; set; }
                [JsonPropertyName("groups")] public List<Group>? Groups { get; set; }
            }
        }
    }
}