using Cappuccino.App.iOS.UI.Common;

namespace Cappuccino.App.iOS.UI.Contacts;


public partial class ContactsViewController {
    private UITableView? tableView;


    public override void ViewDidLoad() {
        base.ViewDidLoad();

        new NavigationBarBuilder()
            .WithElement(NavigationItem)
            .WithTitle("Contacts")
            .WithSearch()
            .WithSearchTextChangedEvent(SearchTextChanged)
            .WithSearchCancellEvent(SearchCancelled)
            .WithSearchIcon("sliders_outline_28", FilterIconClicked)
            .Apply();

        this.tableView = new UITableView(this.View!.Frame, UITableViewStyle.Plain);
        this.tableView.ApplyDefaultAppearance();

        this.View!.AddSubview(tableView);
    }
}