using Cappuccino.Core.Network;
using Cappuccino.Core.Network.Handlers;
using Cappuccino.App.iOS.Extensions;
using Cappuccino.App.iOS.UI.Chats;
using Friends = Cappuccino.Core.Network.Methods.Friends;
using Users = Cappuccino.Core.Network.Methods.Users;

namespace Cappuccino.App.iOS.UI.Contacts;


public partial class ContactsViewController : UIViewController {
    private readonly UsersAdapterDelegate adapter = new ();
    private readonly SelectiveRequestManager<Users.Search.Response> requestManager = new ();
    private readonly FilterDataObject dataObject = new FilterDataObject();
    private bool isSearchingMode = false;


    private void Initialize() {
        tableView!.RegisterClassForCellReuse(typeof(UserViewCell), nameof(UserViewCell));
        tableView.DataSource = this.adapter;
        tableView.Delegate = this.adapter;

        this.adapter.LastItemBind = RequestUsers;
        this.adapter.ItemClicked = (item) => {
            var vc = new Messages.MessagesViewController(new ChatItem {
                Id = item.Id,
                ChatType = ChatItem.TypeUser,
                Title = $"{item.FirstName} {item.LastName}",
                Photo = item.Photo200
            });
            NavigationController?.PushViewController(vc, true);
        };
        this.requestManager.RequestCallback = new ApiCallback<Users.Search.Response>()
            .OnSuccess(result => {
                if (this.isSearchingMode) {
                    this.adapter.Items.Clear();
                    this.adapter.Items.AddRange(result.Inner?.Items!);
                    tableView!.ReloadData();
                }
            })
            .OnError(reason => { });
        
        if (this.adapter.ItemCount == 0)
            RequestUsers(0);
    }


    private void SearchTextChanged(object? sender, UISearchBarTextChangedEventArgs args) {
        this.isSearchingMode = !string.IsNullOrEmpty(args.SearchText);
        this.adapter.ItemLimit = 0;
        RequestSearch(args.SearchText);
    }

    private void SearchCancelled(object? sender, EventArgs args) {
        if (this.isSearchingMode) {
            this.isSearchingMode = false;
            this.adapter.Items.Clear();
            RequestUsers(0);
        }
    }

    private void FilterIconClicked(object? sender, EventArgs args) {
        var modalController = new FilterModalController(this.dataObject, () => RequestSearch(NavigationItem.SearchController?.SearchBar.Text ?? string.Empty));
        if (modalController.PresentationController is UISheetPresentationController sheetController) {
            sheetController.Detents = new [] {
                UISheetPresentationControllerDetent.CreateMediumDetent()
            };
        }
        PresentViewController(modalController, true, null);
    }


    private void RequestUsers(int offset) {
        Api.Get(new Friends.Get {
            Fields = RequestConstants.UserDefaults(),
            Order = "name",
            Count = 50,
            Offset = offset
        }, new ApiCallback<Friends.Get.Response>()    
            .OnSuccess(result => {
                this.adapter.ItemLimit = result.Inner?.Count ?? 0;
                this.adapter.Items.AddRange(result.Inner?.Items!);
                tableView!.ReloadData();
            })
            .OnError(reason => {})
        );
    }

    private void RequestSearch(string query) {
        if (this.isSearchingMode && !string.IsNullOrEmpty(query)) {
            requestManager.AddRequest(new Users.Search {
                Sort = this.dataObject.SearchOrder,
                Hometown = this.dataObject.HomeTown ?? string.Empty,
                Fields = RequestConstants.UserDefaults(),
                Query = query,
                Offset = 0, 
                Count = 80
            });
        }
    }
}
