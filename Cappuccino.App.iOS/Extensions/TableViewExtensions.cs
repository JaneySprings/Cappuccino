using Foundation;
using UIKit;

namespace Cappuccino.App.iOS.Extensions {
    public static class TableViewExtensions {

        public static void RegisterNibForCellReuseEx<TCell>(this UITableView tableView) {
            string reuseIdentifier = typeof(TCell).Name;
            tableView.RegisterNibForCellReuse(UINib.FromName(reuseIdentifier, null), reuseIdentifier);
        }

        public static void RegisterNibForCellHeaderReuseEx<TCell>(this UITableView tableView) {
            string reuseIdentifier = typeof(TCell).Name;
            tableView.RegisterNibForHeaderFooterViewReuse(UINib.FromName(reuseIdentifier, null), reuseIdentifier);
        }

        public static UITableViewCell DequeueReusableCellEx<TCell>(this UITableView tableView, NSIndexPath indexPath) {
            return tableView.DequeueReusableCell(typeof(TCell).Name, indexPath);
        }

        public static UITableViewHeaderFooterView DequeueReusableHeaderFooterViewEx<TCell>(this UITableView tableView) {
            return tableView.DequeueReusableHeaderFooterView(typeof(TCell).Name);
        }
    }
}