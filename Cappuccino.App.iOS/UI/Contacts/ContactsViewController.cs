using Cappuccino.Core.Network;
using Cappuccino.Core.Network.Handlers;
using Cappuccino.Core.Network.Methods;
using Cappuccino.Core.Network.Managing;
using Models = Cappuccino.Core.Network.Models;
using Friends = Cappuccino.Core.Network.Methods.Friends;
using Users = Cappuccino.Core.Network.Methods.Users;

namespace Cappuccino.App.iOS.UI.Contacts;


public partial class ContactsViewController : UIViewController {
    private readonly UsersAdapterDelegate adapter = new ();
    private readonly SingleRequestManager<Models.Users.SearchResponse> requestManager = new ();
    private bool isSearchingMode = false;


    public override void ViewDidAppear(bool animated) {
        base.ViewDidAppear(animated);

        tableView!.RegisterClassForCellReuse(typeof(UserViewCell), nameof(UserViewCell));
        tableView.DataSource = this.adapter;
        tableView.Delegate = this.adapter;

        this.adapter.OnLastItemBind = RequestUsers;
        this.requestManager.RequestCallback = new ApiCallback<Models.Users.SearchResponse>()
            .OnSuccess(result => {
                if (this.isSearchingMode) {
                    this.adapter.ReplaceItems(result.InnerResponse?.Items!);
                    tableView!.ReloadData();
                }
            })
            .OnError(reason => { });

        if (this.adapter.GetItemCount() == 0)
            RequestUsers(0);
    }

    private void SearchTextChanged(object? sender, UISearchBarTextChangedEventArgs args) {
        this.isSearchingMode = !args.SearchText.Equals(String.Empty);

        if (this.isSearchingMode) {
            requestManager.AddRequest(new Users.Search(args.SearchText, 0, 0, 80, UserFields.Default));
        }
    }

    private void FilterIconClicked(object? sender, EventArgs args) {}


    private void RequestUsers(int offset) {
        if (!this.isSearchingMode) {
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
}
