using System;
using UIKit;
using Foundation;
using Cappuccino.Core.Network;
using Cappuccino.Core.Network.Methods;
using Cappuccino.Core.Network.Handlers;
using Cappuccino.App.iOS.Extensions;
using _Messages = Cappuccino.Core.Network.Methods.Messages;
using Models = Cappuccino.Core.Network.Models;
using Cappuccino.App.iOS.UI.Common;

namespace Cappuccino.App.iOS.UI.Chats {
    [Register("ChatsViewController")]
    public partial class ChatsViewController : UIViewController {
        private readonly ChatsAdapterDelegate adapter = new ChatsAdapterDelegate();

        public ChatsViewController(IntPtr handle) : base (handle) {}

        public override void ViewDidLoad() {
            base.ViewDidLoad();
            LongPollExecutor.HistoryUpdated += OnHistoryUpdated;

            new NavigationBarBuilder()
                .WithDefaultStyle(NavigationController.NavigationBar)
                .WithElement(NavigationItem)
                .WithTitle("Chats")
                .Apply();

            tableView.RegisterNibForCellReuseEx<ChatViewCell>();
            tableView.DataSource = this.adapter;
            tableView.Delegate = this.adapter;

            if (this.adapter.GetItemCountInSection(0) == 0)
                RequestConversations(0);
        }

        public override void ViewDidDisappear(bool animated) {
            base.ViewDidDisappear(animated);
            LongPollExecutor.HistoryUpdated -= OnHistoryUpdated;
        }

        public override void DidReceiveMemoryWarning() {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }


        private void OnHistoryUpdated(object sender, EventArgs args) {
            RequestConversations(0);
        }

        private void RequestConversations(int offset) {
            Api.Get(new _Messages.GetConversations(UserFields.Default, offset, 100, 1), new ApiCallback<Models.Messages.GetConversationsResponse>()
                .OnSuccess(result => {
                    this.adapter.ItemLimit = result.InnerResponse?.Count ?? 0;
                    this.adapter.Replace(result.InnerResponse?.ToChatItems());
                    tableView.ReloadData();
                })
                .OnError(reason => {
                    var t = 2 + 2;
                })
            );
        }
    }
}