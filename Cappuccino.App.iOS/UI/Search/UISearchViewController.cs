using System;
using Cappuccino.App.iOS.UI.Common;
using Foundation;
using UIKit;

namespace Cappuccino.App.iOS.UI.Search {

    public partial class SearchViewController {
        private UITableView? tableView;

        public override void ViewDidLoad() {
            base.ViewDidLoad();

            new NavigationBarBuilder()
                .WithElement(NavigationItem)
                .WithTitle("Global Search")
                .WithSearch(SearchTextChanged)
                .WithSearchIcon("sliders_outline_28", SearchIconClicked)
                .Apply();

            this.tableView = new UITableView(this.View!.Frame, UITableViewStyle.Grouped);
            this.tableView.ApplyDefaultAppearance();

            this.View!.AddSubview(tableView);
        }

        public override void ViewDidUnload() {
            base.ViewDidUnload();

            if (this.tableView != null) {
                this.tableView.Dispose();
                this.tableView = null;
            }
        }
    }
}
