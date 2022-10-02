using Cappuccino.Core.Network;
using Cappuccino.Core.Network.Methods;
using Cappuccino.Core.Network.Handlers;
using _Messages = Cappuccino.Core.Network.Methods.Messages;
using Models = Cappuccino.Core.Network.Models;
using Cappuccino.Core.Network.Polling;

namespace Cappuccino.App.iOS.UI.Chats;


public partial class ChatsViewController : UIViewController {
    private readonly ChatsAdapterDelegate adapter = new ChatsAdapterDelegate();


    public override void ViewDidAppear(bool animated) {
        base.ViewDidAppear(animated);

        tableView!.RegisterClassForCellReuse(typeof(ChatViewCell), nameof(ChatViewCell));
        tableView.DataSource = this.adapter;
        tableView.Delegate = this.adapter;

        this.adapter.OnLastItemBind = RequestConversations;
        
        LongPollManager.Instance.HistoryUpdated += (response) => {
            this.adapter.ClearItems();
            RequestConversations(0);  
        };

        if (this.adapter.GetItemCount() == 0)
            RequestConversations(0);
    }

    private void RequestConversations(int offset) {
        Api.Get(new _Messages.GetConversations {
            Fields = Constants.DefaultUserFields,
            Count = 50,
            Extended = 1,
            Offset = offset
        }, new ApiCallback<Models.Messages.GetConversationsResponse>()
            .OnSuccess(result => {
                this.adapter.ItemLimit = result.InnerResponse?.Count ?? 0;
                this.adapter.AddItems(result.InnerResponse?.ToChatItems()!);
                tableView!.ReloadData();
            })
            .OnError(reason => {})
        );
    }
}