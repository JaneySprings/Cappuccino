using System;
using Cappuccino.App.iOS.UI.Common;
using Cappuccino.Core.Network.Models.Users;
using Foundation;
using UIKit;

namespace Cappuccino.App.iOS.UI.Contacts {

    public partial class UsersAdapterDelegate : TableViewAdapterBase<User, UserViewCell> {
        public UsersAdapterDelegate(int sections = 1) : base(sections) {}

        [Export("tableView:viewForHeaderInSection:")]
        public UIView GetViewForHeader(UITableView tableView, nint section) {
            UITableViewHeaderFooterView view = tableView.DequeueReusableHeaderFooterView(nameof(HeaderViewCell));
            HeaderViewCell? cell = view as HeaderViewCell;

            cell?.Bind(section == 0 ? "Important" : $"All ({ItemLimit})");
            return cell!;
        }

        [Export("tableView:heightForHeaderInSection:")]
        public nfloat GetHeightForHeader(UITableView tableView, nint section) {
            UITableViewHeaderFooterView view = tableView.DequeueReusableHeaderFooterView(nameof(HeaderViewCell));
            return view.ContentView.Frame.Height;
        }
    }
}