using Cappuccino.Core.Network;
using Cappuccino.Core.Network.Polling;
using Cappuccino.Core.Network.Handlers;
using Cappuccino.App.iOS.Extensions;
using Cappuccino.App.iOS.UI.Chats;
using _Messages = Cappuccino.Core.Network.Methods.Messages;
using Models = Cappuccino.Core.Network.Models;

namespace Cappuccino.App.iOS.UI.Messages;


public partial class MessagesViewController : UIViewController {
    private readonly MessagesAdapterDelegate adapter = new MessagesAdapterDelegate();
    private readonly ChatItem chatItem;
    private DataController? dataController;


    public MessagesViewController(ChatItem chatItem) {
        this.chatItem = chatItem;
    }


    private void Initialize() {
        tableView!.RegisterClassForCellReuse(typeof(MessageViewCell), nameof(MessageViewCell));
        tableView.DataSource = this.adapter;
        tableView.Delegate = this.adapter;

        this.dataController = new DataController {
            TableView = tableView,
            Adapter = this.adapter,
            ConversationId = this.chatItem.Id
        };

        this.adapter.ItemClicked = TableViewItemClicked;
        this.bottomPanel!.SendMessageButton!.TouchUpInside += SendButtonClicked;

        LongPollManager.Instance.MessageReceived += RequestHistory;
        BindConversation();

        if (this.adapter.ItemCount == 0)
            RequestMessages();
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

    private void ResetBottomPanel() {
        this.bottomPanel!.MessageBoxField!.Text = string.Empty;
        this.bottomPanel.SetNeedsLayout();
    }

    private void BindConversation() {
        this.chatPhotoButton!.Load(this.chatItem.Photo);
        this.TitleLabel = this.chatItem.Title;
    }


    private void RequestSend() { 
        Api.Get(new _Messages.Send {
            PeerId = this.chatItem.Id,
            RandomId = this.bottomPanel!.MessageBoxField!.Text?.GetHashCode() ?? 0,
            Message = this.bottomPanel!.MessageBoxField!.Text ?? ""
        }, new ApiCallback<_Messages.Send.InnerResponse>()
            .OnSuccess(result => ResetBottomPanel())
            .OnError(reason => {})
        );
    }

    private void RequestMessages() {
        Api.Get(new _Messages.GetHistory {
            Fields = RequestConstants.UserDefaults(),
            PeerId = this.chatItem.Id,
            Offset = 0,
            Extended = 1,
            Count = 20
        }, new ApiCallback<_Messages.GetHistory.Response>()
            .OnSuccess(result => {
                this.adapter.Items.AddRange(result.Inner?.ToMessageItems()!);
                this.adapter.Reorder();
                this.tableView!.ReloadData();
                this.tableView.ScrollDown(false);
            })
            .OnError(reason => {})
        );
    }

    private void RequestHistory(Models.LongPollResponse _) {
        Api.Get(new _Messages.GetLongPollHistory {
            Fields = RequestConstants.UserDefaults(),
            Ts = LongPollManager.Instance.Ts,
            Pts = LongPollManager.Instance.Pts,
        }, new ApiCallback<_Messages.GetLongPollHistory.Response>()
            .OnSuccess(result => {
                this.dataController?.ProcessUpdate(result.Inner!);
                LongPollManager.Instance.Pts = result.Inner?.NewPts ?? 0;
            })
            .OnError(reason => {})
        );
    }
}