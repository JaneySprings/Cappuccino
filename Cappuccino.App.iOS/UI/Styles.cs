namespace Cappuccino.App.iOS.UI;

public static class Colors {
    public static UIColor Accent => UIColor.FromName("accent")!;

    public static UIColor Background => UIColor.FromName("background")!;
    public static UIColor Foreground => UIColor.FromName("foreground")!;
    public static UIColor Divider => UIColor.FromName("divider")!;

    public static UIColor Text => UIColor.FromName("text")!;
    public static UIColor TextGray => UIColor.FromName("text_gray")!;
}


public static class UITabBarStyles {
    public static void ApplyDefaultAppearance(this UITabBar tabBar) {
        var appearance = new UITabBarAppearance();
        appearance.StackedItemPositioning = UITabBarItemPositioning.Centered;

        tabBar.StandardAppearance = appearance;
        tabBar.TintColor = Colors.Accent;
        tabBar.UnselectedItemTintColor = Colors.TextGray;
        tabBar.BackgroundColor = Colors.Foreground;
    }
}

public static class UITableViewStyles {
    public static void ApplyDefaultAppearance(this UITableView tableView) {
        tableView.BackgroundColor = Colors.Foreground;
        tableView.ShowsHorizontalScrollIndicator = false;
        tableView.BouncesZoom = false;
        tableView.AlwaysBounceVertical = true;
        tableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
        tableView.AllowsMultipleSelection = false;
        tableView.AllowsSelection = false;
    }
}

public static class UINavigationBarStyles {
    public static void ApplyDefaultAppearance(this UINavigationBar navigationBar) {
        var attributes = new UIStringAttributes();
        var appearance = new UINavigationBarAppearance();
        var mock = new UIBarButtonItem {
            TintColor = Colors.Accent
        };

        attributes.Font = UIFont.FromName("VKSansDisplay-DemiBold", 21f);
        attributes.ForegroundColor = Colors.Text;;

        appearance.BackgroundColor = Colors.Foreground;
        appearance.TitleTextAttributes = attributes;
        appearance.ShadowColor = UIColor.Clear;

        navigationBar.Items[0].BackBarButtonItem = mock;
        navigationBar.StandardAppearance = appearance;
        navigationBar.ScrollEdgeAppearance = appearance;
        navigationBar.CompactAppearance = appearance;
    }
}
 
public static class UILabelStyles {

    public static void ApplyHeaderAppearance(this UILabel label) {
        label.Font = UIFont.SystemFontOfSize(19f, UIFontWeight.Regular);
        label.TextColor = Colors.TextGray;
    }

    public static void ApplyCaption1Appearance(this UILabel label) {
        label.Font = UIFont.SystemFontOfSize(17f, UIFontWeight.Medium);
        label.TextColor = Colors.Text;
    }

    public static void ApplyCaption2Appearance(this UILabel label) {
        label.Font = UIFont.SystemFontOfSize(15f, UIFontWeight.Regular);
        label.TextColor = Colors.TextGray;
    }

    public static void ApplyDefaultAppearance(this UILabel label) {
        label.Font = UIFont.SystemFontOfSize(16f, UIFontWeight.Regular);
        label.TextColor = Colors.Text;
    }

    public static void ApplyBadgeAppearance(this UILabel label) {
        label.Font = UIFont.SystemFontOfSize(15f, UIFontWeight.Medium);
        label.TextAlignment = UITextAlignment.Center;
        label.TextColor = UIColor.White;
    }

    public static void ApplyRoundedAppearance(this UILabel label) {
        label.Layer.CornerRadius = label.Bounds.Height / 2;
        label.Layer.MasksToBounds = true;
    }
}

public static class UIImageViewStyles {
    public static void ApplyRoundedAppearance(this UIImageView imageView) {
        imageView.Layer.CornerRadius = imageView.Bounds.Height / 2;
        imageView.ClipsToBounds = true;
    }
}
