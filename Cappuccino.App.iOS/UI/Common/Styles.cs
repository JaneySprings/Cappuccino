using System;
using UIKit;

namespace Cappuccino.App.iOS.UI.Common {

    public static class UITabBarStyles {
        public static void ApplyDefaultAppearance(this UITabBar tabBar) {
            tabBar.TintColor = UIColor.FromName("accent");
            tabBar.UnselectedItemTintColor = UIColor.FromName("text_gray_light");
            tabBar.BackgroundColor = UIColor.FromName("foreground");
        }
    }

    public static class UITableViewStyles {
        public static void ApplyDefaultAppearance(this UITableView tableView) {
            tableView.BackgroundColor = UIColor.FromName("foreground");
            tableView.ShowsHorizontalScrollIndicator = false;
            tableView.BouncesZoom = false;
            tableView.AlwaysBounceVertical = true;
            tableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
            tableView.AllowsMultipleSelection = false;
            tableView.AllowsSelection = false;
        }
    }

}

