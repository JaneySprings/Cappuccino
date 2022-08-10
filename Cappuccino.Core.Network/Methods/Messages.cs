using System.Collections.Generic;
using Cappuccino.Core.Network.Internal;

namespace Cappuccino.Core.Network.Methods.Messages {
    public enum Filter { all, unread, important, unanswered }

    /*  
     * Mark: documentation [https://vk.com/dev/messages.getConversations]
     */
    public sealed class GetConversations : ApiRequest<Models.Messages.GetConversationsResponse> {
        public GetConversations(IEnumerable<string>? fields = null,
                                int? offset = null,
                                int? count = null,
                                int? extended = null,
                                Filter? filter = null,
                                int? start_message_id = null) : base("messages.getConversations") {

            AddParam(nameof(fields), fields);
            AddParam(nameof(offset), offset);
            AddParam(nameof(count), count);
            AddParam(nameof(extended), extended);
            AddParam(nameof(filter), filter?.ToString());
            AddParam(nameof(start_message_id), start_message_id);
        }
    }

    /* 
     * Mark: documentation [https://vk.com/dev/messages.getLongPollServer]
     */
    public class GetLongPollServer : ApiRequest<Models.Messages.GetLongPollServerResponse> {
        public GetLongPollServer(int need_pts) : base("messages.getLongPollServer") {
            AddParam(nameof(need_pts), need_pts);
            AddParam("lp_version", ApiManager.ApiConfig?.LpVersion);
        }
    }
}