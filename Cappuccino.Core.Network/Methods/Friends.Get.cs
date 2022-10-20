using System.Collections.Generic;
using System.Text.Json.Serialization;
using Cappuccino.Core.Network.Models.Users;

/* Mark: https://vk.com/dev/friends.get */
namespace Cappuccino.Core.Network.Methods.Friends {

    public sealed class Get : ApiMethod<Get.Response> {
        public Get() : base("friends.get") {}


        public IEnumerable<string> Fields { set => AddParam("fields", value); }
        public string Order { set => AddParam("order", value); }
        public int Count { set => AddParam("count", value); }
        public int Offset { set => AddParam("offset", value); }
        public string NameCase { set => AddParam("name_case", value); }
        public int UserId { set => AddParam("user_id", value); }
        public int ListId { set => AddParam("list_id", value); }


        public class Response {
            [JsonPropertyName("response")] public InnerResponse? Inner { get; set; }

            public class InnerResponse {
                [JsonPropertyName("count")] public int Count { get; set; }
                [JsonPropertyName("items")] public List<User>? Items { get; set; }
            }
        }
    }
}