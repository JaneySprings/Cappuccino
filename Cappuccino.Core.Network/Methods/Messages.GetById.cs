using Cappuccino.Core.Network.Models.Messages;
using Cappuccino.Core.Network.Models.Groups;
using Cappuccino.Core.Network.Models.Users;
using System.Text.Json.Serialization;
using System.Collections.Generic;

/* Mark: https://vk.com/dev/messages.getById */
namespace Cappuccino.Core.Network.Methods.Messages {
    public class GetById: ApiMethod<GetById.Response> {
        public GetById(): base("messages.getById") {}


        public IEnumerable<int> MessageIds { set => AddParam("message_ids", value); }
        public int PreviewLength { set => AddParam("preview_length", value); }
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