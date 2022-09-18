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

namespace Cappuccino.App.iOS.UI.Contacts;


public partial class ContactsViewController : UIViewController {
    private readonly UsersAdapterDelegate adapter = new UsersAdapterDelegate();


    public override void ViewDidAppear(bool animated) {
        base.ViewDidAppear(animated);

        tableView!.RegisterClassForCellReuse(typeof(UserViewCell), nameof(UserViewCell));
        tableView.DataSource = this.adapter;
        tableView.Delegate = this.adapter;

        this.adapter.OnLastItemBind = RequestUsers;

        if (this.adapter.GetItemCount() == 0)
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
                this.adapter.AddItems(result.InnerResponse?.Items!);
                tableView!.ReloadData();
            })
            .OnError(reason => {})
        );
    }
}
