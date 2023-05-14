using Cappuccino.Core.Network.Methods.Messages;
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

    public void ProcessUpdate(GetLongPollHistory.Response.InnerResponse response) {
        var messages = response.ToMessageItems();

        for (int i = 0; i < response.History?.Count; i++) {
            if (response.History[i].Count == 4) {
                if (response.History[i][HistoryPositionChatId] == ConversationId) {
                    var message = messages?.Find(it => it.InnerResponse.Id == response.History[i][HistoryPositionMessageId]);

                    if (message == null)
                        continue;

                    switch (response.History[i][HistoryPositionEvent]) {
                        case LongPollEventNewMessage:
                            if (Adapter?.AddItem(message) == true) 
                                TableView?.InsertLastRow();
                            break;

                        case LongPollEventEditMessage:
                            var index = Adapter?.UpdateItem(message);
                            if (index == null || index < 0)
                                continue;

                            TableView?.ReloadRow(index.Value);
                            break;

                        case LongPollEventDeleteMessage:
                            var _index = Adapter?.RemoveItemById(message.MessageId);
                            if (_index == null || _index < 0)
                                continue;

                            TableView?.DeleteRow(_index.Value);
                            break;
                    }
                }
            }
        }
    }
}