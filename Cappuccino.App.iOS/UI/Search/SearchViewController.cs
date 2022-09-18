using System;
using Cappuccino.App.iOS.Extensions;
using Cappuccino.App.iOS.UI.Common;
using Cappuccino.App.iOS.UI.Contacts;
using Cappuccino.Core.Network;
using Cappuccino.Core.Network.Handlers;
using Cappuccino.Core.Network.Methods;
using Models = Cappuccino.Core.Network.Models;
using Users = Cappuccino.Core.Network.Methods.Users;
using Foundation;
using UIKit;

namespace Cappuccino.App.iOS.UI.Search;

public partial class SearchViewController : UIViewController {
    private readonly UsersAdapterDelegate adapter = new UsersAdapterDelegate();
    private readonly SingleRequestManager<Models.Users.SearchResponse> requestManager = new();

    public override void ViewDidAppear(bool animated) {
        base.ViewDidAppear(animated);
            
        tableView!.RegisterClassForCellReuse(typeof(UserViewCell), nameof(UserViewCell));
        tableView.DataSource = this.adapter;
        tableView.Delegate = this.adapter;

        this.requestManager.RequestCallback = new ApiCallback<Models.Users.SearchResponse>()
            .OnSuccess(result => {
                this.adapter.ReplaceItems(result.InnerResponse?.Items!);
                tableView!.ReloadData();
            })
            .OnError(reason => { });
    }

    private void SearchTextChanged(object sender, UISearchBarTextChangedEventArgs args) {
        if (!args.SearchText.Equals(String.Empty)) {
            SearchUsers(args.SearchText);
        }
    }

    private void SearchIconClicked(object sender, EventArgs args) {}


    private void SearchUsers(string q) {
        requestManager.AddRequest(new Users.Search(q, 0, 0, 100, UserFields.Default));
    }
}