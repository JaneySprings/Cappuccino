using System.Collections.Generic;
using Cappuccino.Core.Network.Config;

namespace Cappuccino.Core.Network.Methods.Messages {

    /*  
     * Mark: documentation [https://vk.com/dev/messages.getConversations]
     */
    public sealed class GetConversations : ApiRequest<Models.Messages.GetConversationsResponse> {
        public IEnumerable<string> Fields { set => AddParam("fields", value); }
        public int Count { set => AddParam("count", value); }
        public int Offset { set => AddParam("offset", value); }
        public int StartMessageId { set => AddParam("start_message_id", value); }
        public int Extended { set => AddParam("extended", value); }
        public string Filter { set => AddParam("filter", value); }

        public GetConversations() : base("messages.getConversations") {}
    }

    /* 
     * Mark: documentation [https://vk.com/dev/messages.getLongPollServer]
     */
    public class GetLongPollServer : ApiRequest<Models.Messages.GetLongPollServerResponse> {
        public int NeedPts { set => AddParam("need_pts", value); }
        
        public GetLongPollServer() : base("messages.getLongPollServer") {
            AddParam("lp_version", CredentialsManager.ApiConfig?.LpVersion);
        }
    }
}