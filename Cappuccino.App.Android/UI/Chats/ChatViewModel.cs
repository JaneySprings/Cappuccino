using System.Collections.Generic;
using AndroidX.Lifecycle;
using Cappuccino.App.Android.Extensions;
using Cappuccino.Core.Network;
using Cappuccino.Core.Network.Handlers;
using Cappuccino.Core.Network.Methods;
using Cappuccino.Core.Network.Models.Response;

namespace Cappuccino.App.Android.UI {
    public class ChatViewModel: ViewModel {
        public Observable<List<ChatItem>> Chats = new Observable<List<ChatItem>>();
        private const int RequestCount = 100;

        public void RequestConversations(int offset) {
            Api.Get(new Messages.GetConversations(RequestCount, offset), new ApiCallback<MessagesGetConversationsResponse>()
                .OnSuccess(result => {
                    this.Chats.PostValue(result.Response!.ToChatItems());
                })
                .OnError(reason => { })
            );
        }
    }
}