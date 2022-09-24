using Cappuccino.App.iOS.UI.Common;

namespace Cappuccino.App.iOS.UI.Chats;


public partial class ChatsViewController {
    private UITableView? tableView;

    public override void ViewDidLoad() {
        base.ViewDidLoad();

        new NavigationBarBuilder()
            .WithElement(NavigationItem)
            .WithTitle("Chats")
            .Apply();

        this.tableView = new UITableView(View!.Frame, UITableViewStyle.Plain);
        this.tableView.ApplyDefaultAppearance();

        this.View.AddSubview(tableView);
    }
}