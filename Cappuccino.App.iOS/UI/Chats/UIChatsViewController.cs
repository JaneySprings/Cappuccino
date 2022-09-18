using System;
using Cappuccino.App.iOS.UI.Common;
using UIKit;


namespace Cappuccino.App.iOS.UI.Chats;

public partial class ChatsViewController {
    private UITableView? tableView;

    public override void ViewDidLoad() {
        base.ViewDidLoad();

        new NavigationBarBuilder()
            .WithElement(NavigationItem)
            .WithTitle("Chats")
            .Apply();

        this.tableView = new UITableView(this.View!.Frame, UITableViewStyle.Grouped);
        this.tableView.ApplyDefaultAppearance();

        this.View!.AddSubview(tableView);
    }
}