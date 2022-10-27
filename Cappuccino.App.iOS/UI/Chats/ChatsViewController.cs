using Cappuccino.Core.Network;
using Cappuccino.Core.Network.Handlers;
using Cappuccino.App.iOS.Extensions;
using _Messages = Cappuccino.Core.Network.Methods.Messages;
using Cappuccino.Core.Network.Polling;

namespace Cappuccino.App.iOS.UI.Chats;


public partial class ChatsViewController : UIViewController {
    private readonly ChatsAdapterDelegate adapter = new ChatsAdapterDelegate();
    private readonly DataController dataController = new DataController();


    private void Initialize() {
        tableView!.RegisterClassForCellReuse(typeof(ChatViewCell), nameof(ChatViewCell));
        tableView.DataSource = this.adapter;
        tableView.Delegate = this.adapter;

        this.adapter.LastItemBind = RequestConversations;
        this.adapter.ItemClicked = (item) => {
            var vc = new Messages.MessagesViewController(item.ChatId);
            NavigationController?.PushViewController(vc, true);
        };

        this.dataController.EventUpdateChats = RequestConversationsById;
        this.dataController.EventDeleteChats = DeleteConversationsById;

        if (this.adapter.ItemCount == 0)
            RequestConversations(0);

        LongPollManager.Instance.MessageReceived += dataController.ProcessUpdates;
    }



    private void DeleteConversationsById(IEnumerable<int> ids) {
        this.adapter.RemoveItemsById(ids);
        tableView!.ReloadData();
    }

    private void RequestConversationsById(IEnumerable<int> ids) {
        Api.Get(new GetConversationsById {
            Fields = RequestConstants.UserDefaults(),
            ConversationIds = ids,
            Extended = 1,
        }, new ApiCallback<_Messages.GetConversations.Response>()
            .OnSuccess(result => {
                this.adapter.InsertItems(result.Inner!.ToChatItems());
                this.tableView!.ReloadData();
            })
            .OnError(error => {})
        );
    }

    private void RequestConversations(int offset) {
        Api.Get(new _Messages.GetConversations {
            Fields = RequestConstants.UserDefaults(),
            Count = 50,
            Extended = 1,
            Offset = offset
        }, new ApiCallback<_Messages.GetConversations.Response>()
            .OnSuccess(result => {
                this.adapter.ItemLimit = result.Inner?.Count ?? 0;
                this.adapter.AddItems(result.Inner?.ToChatItems()!);
                tableView!.ReloadData();
            })
            .OnError(reason => {})
        );
    }
}