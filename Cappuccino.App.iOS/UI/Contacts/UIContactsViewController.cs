using System;
using Cappuccino.App.iOS.UI.Common;
using Foundation;
using UIKit;

namespace Cappuccino.App.iOS.UI.Contacts {

    public partial class ContactsViewController {
        private UITableView? tableView;


        public override void ViewDidLoad() {
            base.ViewDidLoad();

            new NavigationBarBuilder()
                .WithElement(NavigationItem)
                .WithTitle("Contacts")
                .WithMainAction("search_outline_28", BarButtonClicked)
                .Apply();

            this.tableView = new UITableView(this.View!.Frame, UITableViewStyle.Grouped);
            this.tableView.ApplyDefaultAppearance();

            this.View!.AddSubview(tableView);
        }
    }
}

