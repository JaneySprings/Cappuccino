using System.Collections.Generic;
using System.Text.Json.Serialization;

/* Mark: https://vk.com/dev/messages.send */
namespace Cappuccino.Core.Network.Methods.Messages {

    public class Send : ApiMethod<Send.InnerResponse> {
        public Send() : base("messages.send") {}


        public int UserId { set => AddParam("user_id", value); }
        public int RandomId { set => AddParam("random_id", value); }
        public int PeerId { set => AddParam("peer_id", value); }
        public IEnumerable<int> PeerIds { set => AddParam("peer_ids", value); }
        public IEnumerable<int> UserIds { set => AddParam("user_ids", value); }
        public string Domain { set => AddParam("domain", value); }
        public int ChatId { set => AddParam("chat_id", value); }
        public string Message { set => AddParam("message", value); }
        public int Lat { set => AddParam("lat", value); }
        public int Long { set => AddParam("long", value); }
        public IEnumerable<string> Attachments { set => AddParam("attachment", value); }
        public int ReplyTo { set => AddParam("reply_to", value); }
        public IEnumerable<int> ForwardMessages { set => AddParam("forward_messages", value); }
        public string Forward { set => AddParam("forward", value); }
        public int StickerId { set => AddParam("sticker_id", value); }
        public string Payload { set => AddParam("payload", value); }
        public int DontParseLinks { set => AddParam("dont_parse_links", value); }
        public int DisableMentions { set => AddParam("disable_mentions", value); }
        public string Intent { set => AddParam("intent", value); }
        public string SubscribeId { set => AddParam("subscribe_id", value); }


        public class InnerResponse {
            [JsonPropertyName("response")] public int Response { get; set; }
        }
    }
}