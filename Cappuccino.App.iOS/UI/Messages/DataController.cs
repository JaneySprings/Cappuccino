using Cappuccino.Core.Network.Models.Messages;
using Cappuccino.App.iOS.Extensions;

namespace Cappuccino.App.iOS.UI.Messages;


public class DataController { 
    private const int HistoryPositionChatId = 3;
    private const int HistoryPositionMessageId = 1;
    private const int HistoryPositionEvent = 0;

    private const int LongPollEventNewMessage = 4;
    private const int LongPollEventEditMessage = 5;
    private const int LongPollEventDeleteMessage = 2;


    public UITableView? TableView { get; set; }
    public MessagesAdapterDelegate? Adapter { get; set; }
    public int ConversationId { get; set; }

    public void ProcessUpdate(GetLongPollHistoryResponse.Response response) {
        var messages = response.ToMessageItems();

        for (int i = 0; i < response.History?.Count; i++) {
            if (response.History[i].Count == 4 && response.History[i][HistoryPositionChatId] == ConversationId) {
                var message = messages?.Find(it => it.InnerResponse.Id == response.History[i][HistoryPositionMessageId]);

                if (message == null)
                    continue;

                switch (response.History[i][HistoryPositionEvent]) {
                    case LongPollEventNewMessage:
                        Adapter?.AddItem(message);
                        TableView?.InsertLastRow();
                        break;
                    case LongPollEventEditMessage:
                        break;
                    case LongPollEventDeleteMessage:
                        break;
                }
            }
        }
    }
}