﻿using Cappuccino.Core.Network;
using Cappuccino.Core.Network.Handlers;
using _Messages = Cappuccino.Core.Network.Methods.Messages;
using Models = Cappuccino.Core.Network.Models;
using Cappuccino.Core.Network.Polling;

namespace Cappuccino.App.iOS.UI.Chats;


public partial class ChatsViewController : UIViewController {
    private readonly ChatsAdapterDelegate adapter = new ChatsAdapterDelegate();


    private void Initialize() {
        tableView!.RegisterClassForCellReuse(typeof(ChatViewCell), nameof(ChatViewCell));
        tableView.DataSource = this.adapter;
        tableView.Delegate = this.adapter;

        this.adapter.LastItemBind = RequestConversations;
        this.adapter.ItemClicked = (item) => {
            var vc = new Messages.MessagesViewController(item.ChatId);
            NavigationController?.PushViewController(vc, true);
        };

        if (this.adapter.ItemCount == 0)
            RequestConversations(0);

        LongPollManager.Instance.HistoryUpdated += _ => {
            this.adapter.ClearItems();
            RequestConversations(0);  
        };
        
// //#if DEBUG
//         LongPollManager.Instance.ErrorReceived += (reason) => {
//             NavigationItem.Title = reason;
//         };
// //#endif  
        
    }

    public override void ViewDidAppear(bool animated) {
        base.ViewDidAppear(animated);
        if (this.adapter.ItemCount != 0)
            this.tableView!.ReloadData();
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

                if (this.IsViewLoaded && this.View?.Window != null)
                    tableView!.ReloadData();
            })
            .OnError(reason => {})
        );
    }
}