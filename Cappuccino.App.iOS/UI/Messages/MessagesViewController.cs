using Cappuccino.Core.Network;
using Cappuccino.Core.Network.Handlers;
using Cappuccino.App.iOS.Extensions;
using _Messages = Cappuccino.Core.Network.Methods.Messages;
using Models = Cappuccino.Core.Network.Models;

namespace Cappuccino.App.iOS.UI.Messages;


public partial class MessagesViewController : UIViewController {
    private MessagesAdapterDelegate adapter = new MessagesAdapterDelegate();
    private int conversationId;

    public MessagesViewController(int conversationId) {
        this.conversationId = conversationId;
    }
    

    private void Initialize() {
        tableView!.RegisterClassForCellReuse(typeof(MessageViewCell), nameof(MessageViewCell));
        tableView.DataSource = this.adapter;
        tableView.Delegate = this.adapter;

        //this.adapter.LastItemBind = RequestMessages;

        RequrstConversation();

        if (this.adapter.ItemCount == 0)
            RequestMessages(0);
    }

    public override void ViewDidDisappear(bool animated) {
        base.ViewDidDisappear(animated);
        NSNotificationCenter.DefaultCenter.RemoveObserver(this);
    }



    private void RequestMessages(int offset) { 
        Api.Get(new _Messages.GetHistory {
            Fields = Constants.DefaultUserFields,
            PeerId = this.conversationId,
            Offset = offset,
            Extended = 1,
            Count = 25
        }, new ApiCallback<Models.Messages.GetHistoryResponse>()
            .OnSuccess(result => {
                this.adapter.ItemLimit = result.InnerResponse?.Count ?? 0;
                var data = result.InnerResponse?.ToMessageItems();
                data!.Reverse();
                this.adapter.AddItems(data);
                this.tableView!.ReloadData();
                if (this.adapter.ItemCount != 0)
                    this.tableView.ScrollToRow(NSIndexPath.FromRowSection(new IntPtr(this.adapter.ItemCount - 1), new IntPtr(0)), UITableViewScrollPosition.Bottom, true);
            })
            .OnError(reason => {})
        );
    }
    private void RequrstConversation() {
        Api.Get(new _Messages.GetConversationsById {
            PeerIds = new[] { this.conversationId },
            Fields = Constants.DefaultUserFields,
            Extended = 1
        }, new ApiCallback<Models.Messages.GetConversationsByIdResponse>()
            .OnSuccess(result => {
                var conversation = result.InnerResponse?.Items?[0];
                switch(conversation?.peer?.Type) {
                    case "user":
                        var user = result.InnerResponse?.Profiles?.FirstOrDefault(it => it.Id == this.conversationId);
                        this.chatPhotoButton!.Load(user?.Photo200);
                        TitleLabel = $"{user?.FirstName} {user?.LastName}";
                        break;
                    case "chat":
                        this.chatPhotoButton!.Load(conversation.chatSettings?.Photo?.Photo200);
                        TitleLabel = conversation.chatSettings?.Title; 
                        break;
                    case "group":
                        var group = result.InnerResponse?.Groups?.FirstOrDefault(it => it.Id == -this.conversationId);
                        this.chatPhotoButton!.Load(group?.Photo200);
                        TitleLabel = group?.Name;
                        break;
                }
            })
            .OnError(error => {})
        );
    }
}