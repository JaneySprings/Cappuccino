using System.Collections.Generic;
using System.Text.Json.Serialization;
using Cappuccino.Core.Network.Models.Groups;
using Cappuccino.Core.Network.Models.Users;
using Cappuccino.Core.Network.Models.Messages;
using Cappuccino.Core.Network.Config;

/* Mark: https://vk.com/dev/messages.getLongPollHistory */
namespace Cappuccino.Core.Network.Methods.Messages {

    public class GetLongPollHistory : ApiMethod<GetLongPollHistory.Response> {
        public GetLongPollHistory() : base("messages.getLongPollHistory") {
            AddParam("lp_version", CredentialsManager.ApiConfig?.LpVersion);
        }


        public int Ts { set => AddParam("ts", value); }
        public int Pts { set => AddParam("pts", value); }
        public string PreviewLength { set => AddParam("preview_length", value); }
        public string Onlines { set => AddParam("onlines", value); }
        public IEnumerable<string> Fields { set => AddParam("fields", value); }
        public string EventsLimit { set => AddParam("events_limit", value); }
        public string MsgsLimit { set => AddParam("msgs_limit", value); }
        public string MaxMsgId { set => AddParam("max_msg_id", value); }
        public string LastN { set => AddParam("last_n", value); }
        public string Credentials { set => AddParam("credentials", value); }
        
        
        public class Response {
            [JsonPropertyName("response")] public InnerResponse? Inner { get; set; }

            public class InnerResponse {
                [JsonPropertyName("history")] public List<List<int>>? History { get; set; }
                [JsonPropertyName("messages")] public Messages? Messages { get; set; }
                [JsonPropertyName("profiles")] public List<User>? Profiles { get; set; }
                [JsonPropertyName("groups")] public List<Group>? Groups { get; set; }
                [JsonPropertyName("new_pts")] public int NewPts { get; set; }
            }

            public class Messages {
                [JsonPropertyName("count")] public int Count { get; set; }
                [JsonPropertyName("items")] public List<Message>? Items { get; set; }
            }
        }
    }
}