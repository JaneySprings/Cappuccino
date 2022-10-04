using Cappuccino.App.iOS.UI.Common;

namespace Cappuccino.App.iOS.UI.Chats;


public partial class ChatsViewController {
    private UITableView? tableView;

    public override void ViewDidLoad() {
        base.ViewDidLoad();
        
        InterfaceUtility.LayoutNavigationBar(NavigationItem, new NavigationBarOptions {
            Title = Localization.Instance.GetString("title_page_chats"),
        });

        this.tableView = new UITableView(CGRect.Empty, UITableViewStyle.Plain);
        this.tableView.ApplyDefaultAppearance();

        this.View!.AddSubview(tableView);
        Initialize();
    }

    public override void ViewDidLayoutSubviews() {
        base.ViewDidLayoutSubviews();
        this.tableView!.Frame = View!.Bounds;
    }
}