using Cappuccino.Core.Network;
using Cappuccino.Core.Network.Polling;
using Cappuccino.Core.Network.Handlers;
using Cappuccino.App.iOS.Extensions;
using _Messages = Cappuccino.Core.Network.Methods.Messages;
using Models = Cappuccino.Core.Network.Models;

namespace Cappuccino.App.iOS.UI.Messages;


public partial class MessagesViewController : UIViewController {
    private MessagesAdapterDelegate adapter = new MessagesAdapterDelegate();
    private DataController? dataController;
    private int conversationId;

    public MessagesViewController(int conversationId) {
        this.conversationId = conversationId;
    }
    

    private void Initialize() {
        tableView!.RegisterClassForCellReuse(typeof(MessageViewCell), nameof(MessageViewCell));
        tableView.DataSource = this.adapter;
        tableView.Delegate = this.adapter;

        dataController = new DataController {
            TableView = tableView,
            Adapter = this.adapter,
            ConversationId = conversationId
        };

        this.adapter.ItemClicked = TableViewItemClicked;
        this.bottomPanel!.SendMessageButton!.TouchUpInside += SendButtonClicked;

        LongPollManager.Instance.MessageReceived += RequestHistory;
        

        if (this.adapter.ItemCount == 0) {
            RequrstConversation();
            RequestMessages();
        }
    }

    public override void ViewDidDisappear(bool animated) {
        base.ViewDidDisappear(animated);

        this.adapter.ItemClicked -= TableViewItemClicked;
        this.bottomPanel!.SendMessageButton!.TouchUpInside -= SendButtonClicked;

        LongPollManager.Instance.MessageReceived -= RequestHistory;
        NSNotificationCenter.DefaultCenter.RemoveObserver(this);
    }



    private void TableViewItemClicked(MessageItem item) {
        this.bottomPanel!.MessageBoxField!.EndEditing(true);
    }
    private void SendButtonClicked(object? sender, EventArgs e) {
        RequestSend();
    }



    private void RequestSend() { 
        Api.Get(new _Messages.Send {
            PeerId = this.conversationId,
            RandomId = this.bottomPanel!.MessageBoxField!.Text.GetHashCode(),
            Message = this.bottomPanel!.MessageBoxField!.Text
        }, new ApiCallback<Models.Messages.SendResponse>()
            .OnSuccess(result => {
                this.bottomPanel.MessageBoxField.Text = string.Empty;
            })
            .OnError(reason => {})
        );
    }

    private void RequestMessages() { 
        Api.Get(new _Messages.GetHistory {
            Fields = RequestExtensions.UserDefaults(),
            PeerId = this.conversationId,
            Offset = 0,
            Extended = 1,
            Count = 100
        }, new ApiCallback<Models.Messages.GetHistoryResponse>()
            .OnSuccess(result => {
                this.adapter.AddItems(result.InnerResponse?.ToMessageItems()!);
                this.tableView!.ReloadData();
                this.tableView.ScrollDown(false);
            })
            .OnError(reason => {})
        );
    }

    private void RequestHistory(Models.LongPollResponse _) { 
        Api.Get(new _Messages.GetLongPollHistory {
            Fields = RequestExtensions.UserDefaults(),
            Ts = LongPollManager.Instance.Ts,
            Pts = LongPollManager.Instance.Pts,
        }, new ApiCallback<Models.Messages.GetLongPollHistoryResponse>()
            .OnSuccess(result => {
                dataController?.ProcessUpdate(result.InnerResponse!);
                LongPollManager.Instance.Pts = result.InnerResponse?.NewPts ?? 0;
            })
            .OnError(reason => {})
        );
    }

    private void RequrstConversation() {
        Api.Get(new _Messages.GetConversationsById {
            PeerIds = new[] { this.conversationId },
            Fields = RequestExtensions.UserDefaults(),
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