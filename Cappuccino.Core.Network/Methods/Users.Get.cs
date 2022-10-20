using System.Collections.Generic;
using System.Text.Json.Serialization;
using Cappuccino.Core.Network.Models.Users;

/* Mark: https://vk.com/dev/users.get */
namespace Cappuccino.Core.Network.Methods.Users {

    public class Get : ApiMethod<Get.Response> {
        public Get() : base("users.get") {}


        public IEnumerable<int> UserIds { set => AddParam("user_ids", value); }
        public IEnumerable<string> Fields { set => AddParam("fields", value); }
        public string NameCase { set => AddParam("name_case", value); }


        public class Response {
            [JsonPropertyName("response")] public InnerResponse? Inner { get; set; }

            public class InnerResponse {
                [JsonPropertyName("count")] public int Count { get; set; }
                [JsonPropertyName("items")] public List<User>? Items { get; set; }
            }
        }
    }
}