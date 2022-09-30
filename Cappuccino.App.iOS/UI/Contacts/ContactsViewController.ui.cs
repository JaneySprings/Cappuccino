using Cappuccino.App.iOS.UI.Common;

namespace Cappuccino.App.iOS.UI.Contacts;


public partial class ContactsViewController {
    private UITableView? tableView;


    public override void ViewDidLoad() {
        base.ViewDidLoad();

        InterfaceUtility.LayoutNavigationBar(NavigationItem, new NavigationBarOptions {
            Title = Localization.Instance.GetString("title_page_contacts"),
            Search = true,
            SearchTextChanged = SearchTextChanged,
            SearchCancelled = SearchCancelled,
            SearchAction = new BarButtonItemOptions {
                Image = "sliders_outline_28",
                Clicked = FilterIconClicked
            }
        });

        this.tableView = new UITableView(this.View!.Frame, UITableViewStyle.Plain);
        this.tableView.ApplyDefaultAppearance();

        this.View!.AddSubview(tableView);
    }
}