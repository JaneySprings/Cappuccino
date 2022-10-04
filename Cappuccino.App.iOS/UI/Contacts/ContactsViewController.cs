using Cappuccino.Core.Network;
using Cappuccino.Core.Network.Handlers;
using Cappuccino.App.iOS.Extensions;
using Models = Cappuccino.Core.Network.Models;
using Friends = Cappuccino.Core.Network.Methods.Friends;
using Users = Cappuccino.Core.Network.Methods.Users;

namespace Cappuccino.App.iOS.UI.Contacts;


public partial class ContactsViewController : UIViewController {
    private readonly UsersAdapterDelegate adapter = new ();
    private readonly SelectiveRequestManager<Models.Users.SearchResponse> requestManager = new ();
    private readonly FilterDataObject dataObject = new FilterDataObject();
    private bool isSearchingMode = false;


    private void Initialize() {
        tableView!.RegisterClassForCellReuse(typeof(UserViewCell), nameof(UserViewCell));
        tableView.DataSource = this.adapter;
        tableView.Delegate = this.adapter;

        this.adapter.LastItemBind = RequestUsers;
        this.adapter.ItemClicked = (item) => {
            var vc = new Messages.MessagesViewController(item.Id);
            NavigationController?.PushViewController(vc, true);
        };
        this.requestManager.RequestCallback = new ApiCallback<Models.Users.SearchResponse>()
            .OnSuccess(result => {
                if (this.isSearchingMode) {
                    this.adapter.ReplaceItems(result.InnerResponse?.Items!);
                    tableView!.ReloadData();
                }
            })
            .OnError(reason => { });
        
        if (this.adapter.ItemCount == 0)
            RequestUsers(0);
    }


    private void SearchTextChanged(object? sender, UISearchBarTextChangedEventArgs args) {
        this.isSearchingMode = !string.IsNullOrEmpty(args.SearchText);
        RequestSearch(args.SearchText);
    }

    private void SearchCancelled(object? sender, EventArgs args) {
        if (this.isSearchingMode) {
            this.isSearchingMode = false;
            this.adapter.ClearItems();
            RequestUsers(0);
        }
    }

    private void FilterIconClicked(object? sender, EventArgs args) {
        var modalController = new FilterModalController(this.dataObject, () => {
            RequestSearch(NavigationItem.SearchController?.SearchBar.Text ?? string.Empty);
        });
        PresentViewController(modalController, true, null);
    }


    private void RequestUsers(int offset) {
        if (!this.isSearchingMode) {
            Api.Get(new Friends.Get {
                Fields = Constants.DefaultUserFields,
                Order = "name",
                Count = 50,
                Offset = offset
            }, new ApiCallback<Models.Friends.GetResponse>()    
                .OnSuccess(result => {
                    this.adapter.ItemLimit = result.InnerResponse?.Count ?? 0;
                    this.adapter.AddItems(result.InnerResponse?.Items!);
                    tableView!.ReloadData();
                })
                .OnError(reason => {})
            );
        }
    }

    private void RequestSearch(string query) {
        if (this.isSearchingMode && !string.IsNullOrEmpty(query)) {
            requestManager.AddRequest(new Users.Search {
                Sort = this.dataObject.SearchOrder,
                Hometown = this.dataObject.HomeTown ?? string.Empty,
                Query = query,
                Fields = Constants.DefaultUserFields,
                Offset = 0, 
                Count = 80
            });
        }
    }
}
