using System.Text.Json.Serialization;
using Cappuccino.Core.Network.Config;

/* Mark: https://vk.com/dev/messages.getLongPollServer */
namespace Cappuccino.Core.Network.Methods.Messages {
    
    public class GetLongPollServer : ApiMethod<GetLongPollServer.Response> {
        public GetLongPollServer() : base("messages.getLongPollServer") {
            AddParam("lp_version", CredentialsManager.ApiConfig?.LpVersion);
        }

        public int NeedPts { set => AddParam("need_pts", value); }

        public class Response {
            [JsonPropertyName("response")] public InnerResponse? Inner { get; set; }

            public class InnerResponse {
                [JsonPropertyName("server")] public string? Server { get; set; }
                [JsonPropertyName("key")] public string? Key { get; set; }
                [JsonPropertyName("ts")] public int Ts { get; set; }
                [JsonPropertyName("pts")] public int Pts { get; set; }
            }
        }
    }
}