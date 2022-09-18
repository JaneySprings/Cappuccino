using System;
using UIKit;

namespace Cappuccino.App.iOS.UI;


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

public static class UINavigationBarStyles {
    public static void ApplyDefaultAppearance(this UINavigationBar navigationBar) {
        var attributes = new UIStringAttributes();
        var appearance = new UINavigationBarAppearance();
        var mock = new UIBarButtonItem {
            TintColor = UIColor.FromName("accent")
        };

        attributes.Font = UIFont.FromName("VKSansDisplay-Bold", 21f);
        attributes.ForegroundColor = UIColor.FromName("text");

        appearance.BackgroundColor = UIColor.FromName("foreground");
        appearance.TitleTextAttributes = attributes;
        appearance.ShadowColor = UIColor.Clear;

        navigationBar.Items[0].BackBarButtonItem = mock;
        navigationBar.StandardAppearance = appearance;
        navigationBar.ScrollEdgeAppearance = appearance;
        navigationBar.CompactAppearance = appearance;
    }
}

public static class UILabelStyles {
    //todo
    public static void ApplyHeader1Appearance(this UILabel label) {
        label.Font = UIFont.SystemFontOfSize(19f, UIFontWeight.Regular);
        label.TextColor = UIColor.FromName("text_gray_light");
    }

    public static void ApplyHeader2Appearance(this UILabel label) {
        label.Font = UIFont.SystemFontOfSize(19f, UIFontWeight.Regular);
        label.TextColor = UIColor.FromName("text_gray_light");
    }

    public static void ApplyCaption1Appearance(this UILabel label) {
        label.Font = UIFont.SystemFontOfSize(17f, UIFontWeight.Medium);
        label.TextColor = UIColor.FromName("text");
    }

    public static void ApplyCaption2Appearance(this UILabel label) {
        label.Font = UIFont.SystemFontOfSize(15f, UIFontWeight.Regular);
        label.TextColor = UIColor.FromName("text_gray_light");
    }

    public static void ApplyDefaultAppearance(this UILabel label) {
        label.Font = UIFont.SystemFontOfSize(16f, UIFontWeight.Regular);
        label.TextColor = UIColor.FromName("text");
    }
}

public static class UIImageViewStyles {
    public static void ApplyRoundedAppearance(this UIImageView imageView) {
        imageView.Layer.CornerRadius = imageView.Bounds.Height / 2;
        imageView.ClipsToBounds = true;
    }
}
