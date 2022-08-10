using System;
using Cappuccino.App.iOS.Extensions;
using Cappuccino.App.iOS.UI.Common;
using Cappuccino.Core.Network;
using Cappuccino.Core.Network.Handlers;
using Cappuccino.Core.Network.Methods;
using Models = Cappuccino.Core.Network.Models;
using Friends = Cappuccino.Core.Network.Methods.Friends;
using UIKit;
using Foundation;
using Cappuccino.App.iOS.UI.Search;

namespace Cappuccino.App.iOS.UI.Contacts {

    [Register("ContactsViewController")]
    public partial class ContactsViewController : UIViewController {
        private readonly UsersAdapterDelegate adapter = new UsersAdapterDelegate();

        public ContactsViewController(IntPtr handle) : base (handle) { }

        public override void ViewDidLoad() {
            base.ViewDidLoad();

            new NavigationBarBuilder()
                .WithDefaultStyle(NavigationController.NavigationBar)
                .WithElement(NavigationItem)
                .WithTitle("Contacts")
                .WithMainAction("search_outline_28", BarButtonClicked)
                .Apply();

            tableView.RegisterNibForCellReuseEx<UserViewCell>();
            tableView.RegisterNibForCellHeaderReuseEx<HeaderViewCell>();
            tableView.DataSource = this.adapter;
            tableView.Delegate = this.adapter;

            this.adapter.OnLastItemBind = RequestUsers;

            if (this.adapter.GetItemCountInSection(0) == 0)
                RequestImportantUsers();
            if (this.adapter.GetItemCountInSection(1) == 0)
                RequestUsers(0);
        }

        private void BarButtonClicked(object sender, EventArgs args) {
            if (NavigationController == null)
                return;

            NavigationController.PushViewController(new SearchViewController(), true);
        }

        private void RequestUsers(int offset) {
            Api.Get(new Friends.Get(UserFields.Default, Friends.Order.name, offset), new ApiCallback<Models.Friends.GetResponse>()
                .OnSuccess(result => {
                    this.adapter.ItemLimit = result.InnerResponse?.Count ?? 0;
                    this.adapter.Add(result.InnerResponse?.Items!, 1);
                    tableView.ReloadData();
                })
                .OnError(reason => {})
            );
        }

        private void RequestImportantUsers() {
            Api.Get(new Friends.Get(UserFields.Default, Friends.Order.hints, 0, 5), new ApiCallback<Models.Friends.GetResponse>()
                .OnSuccess(result => {
                    this.adapter.Add(result.InnerResponse?.Items!);
                    tableView.ReloadData();
                })
                .OnError(reason => { })
            );
        }
    }
}