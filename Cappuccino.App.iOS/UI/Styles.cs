namespace Cappuccino.App.iOS.UI;

public static class Colors {
    public static UIColor Accent => UIColor.FromName("accent")!;

    public static UIColor Background => UIColor.FromName("background")!;
    public static UIColor Foreground => UIColor.FromName("foreground")!;
    public static UIColor Divider => UIColor.FromName("divider")!;

    public static UIColor Text => UIColor.FromName("text")!;
    public static UIColor TextGray => UIColor.FromName("text_gray")!;
}

public static class Dimensions {
    public static nfloat HorizontalContentInsets => 16;
    public static nfloat EditorsHeight => 42;
}


public static class UITabBarStyles {
    public static UITabBar ApplyDefaultAppearance(this UITabBar tabBar) {
        var appearance = new UITabBarAppearance();
        appearance.StackedItemPositioning = UITabBarItemPositioning.Centered;

        tabBar.StandardAppearance = appearance;
        tabBar.TintColor = Colors.Accent;
        tabBar.UnselectedItemTintColor = Colors.TextGray;
        tabBar.BackgroundColor = Colors.Foreground;

        return tabBar;
    }
}

public static class UITableViewStyles {
    public static UITableView ApplyDefaultAppearance(this UITableView tableView) {
        tableView.BackgroundColor = Colors.Foreground;
        tableView.ShowsHorizontalScrollIndicator = false;
        tableView.BouncesZoom = false;
        tableView.AlwaysBounceVertical = true;
        tableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
        tableView.AllowsMultipleSelection = false;
        tableView.AllowsSelection = false;

        return tableView;
    }
}

public static class UINavigationBarStyles {
    public static UINavigationBar ApplyDefaultAppearance(this UINavigationBar navigationBar) {
        var attributes = new UIStringAttributes();
        var appearance = new UINavigationBarAppearance();
        var mock = new UIBarButtonItem {
            TintColor = Colors.Accent
        };

        attributes.Font = UIFont.FromName("VKSansDisplay-DemiBold", 21f);
        attributes.ForegroundColor = Colors.Text;

        appearance.BackgroundColor = Colors.Foreground;
        appearance.TitleTextAttributes = attributes;
        appearance.ShadowColor = UIColor.Clear;

        navigationBar.Items[0].BackBarButtonItem = mock;
        navigationBar.StandardAppearance = appearance;
        navigationBar.ScrollEdgeAppearance = appearance;
        navigationBar.CompactAppearance = appearance;

        return navigationBar;
    }
}
 
public static class UILabelStyles {

    public static UILabel ApplyHeaderAppearance(this UILabel label) {
        label.Font = UIFont.FromName("VKSansDisplay-DemiBold", 21f);
        label.TextColor = Colors.Text;
        return label;
    }

    public static UILabel ApplyCaption1Appearance(this UILabel label) {
        label.Font = UIFont.SystemFontOfSize(17f, UIFontWeight.Medium);
        label.TextColor = Colors.Text;
        return label;
    }

    public static UILabel ApplyCaption2Appearance(this UILabel label) {
        label.Font = UIFont.SystemFontOfSize(15f, UIFontWeight.Regular);
        label.TextColor = Colors.TextGray;
        return label;
    }

    public static UILabel ApplyDefaultAppearance(this UILabel label) {
        label.Font = UIFont.SystemFontOfSize(16f, UIFontWeight.Regular);
        label.TextColor = Colors.Text;
        return label;
    }

    public static UILabel ApplyBadgeAppearance(this UILabel label) {
        label.Font = UIFont.SystemFontOfSize(15f, UIFontWeight.Medium);
        label.TextAlignment = UITextAlignment.Center;
        label.TextColor = UIColor.White;
        return label;
    }

    public static UILabel ApplyRoundedAppearance(this UILabel label) {
        label.Layer.CornerRadius = label.Bounds.Height / 2;
        label.Layer.MasksToBounds = true;
        return label;
    }
}

public static class UIImageViewStyles {
    public static UIImageView ApplyRoundedAppearance(this UIImageView imageView) {
        imageView.Layer.CornerRadius = imageView.Bounds.Height / 2;
        imageView.ClipsToBounds = true;
        return imageView;
    }
}

public static class UISegmentedControlStyles {
    public static UISegmentedControl ApplyDefaultAppearance(this UISegmentedControl segmentedControl) {
        var attrNormal = new UIStringAttributes();
        var attrSelected = new UIStringAttributes();

        attrNormal.Font = UIFont.SystemFontOfSize(15f, UIFontWeight.Medium);
        attrSelected.Font = UIFont.SystemFontOfSize(15f, UIFontWeight.Medium);
        attrNormal.ForegroundColor = Colors.TextGray;
        attrSelected.ForegroundColor = Colors.Text;

        segmentedControl.SetTitleTextAttributes(attrSelected, UIControlState.Selected);
        segmentedControl.SetTitleTextAttributes(attrNormal, UIControlState.Normal);

        return segmentedControl;
    }
}

public static class UITextFieldStyles {
    public static UITextField ApplyDefaultAppearance(this UITextField field) {
        field.BackgroundColor = Colors.Divider;
        field.TextColor = Colors.Text;
        field.TintColor = Colors.Accent;
        field.ClipsToBounds = true;
        field.Layer.CornerRadius = 12;
        field.LeftView = new UIView(new CGRect(0, 0, 16, field.Bounds.Height));
        field.LeftViewMode = UITextFieldViewMode.Always;
        field.Font = UIFont.SystemFontOfSize(16f, UIFontWeight.Regular);

        return field;
    }
}

public static class UIButtonStyles {
    public static UIButton ApplyDefaultAppearance(this UIButton button) {
        button.SetTitleColor(Colors.Accent, UIControlState.Normal);
        return button;
    }

    public static UIButton ApplyActionAppearance(this UIButton button) {
        button.ClipsToBounds = true;
        button.Layer.CornerRadius = 12;
        button.BackgroundColor = Colors.Accent;
        button.SetTitleColor(UIColor.White, UIControlState.Normal);
        button.TitleLabel.Font = UIFont.SystemFontOfSize(17f, UIFontWeight.Medium);
        return button;
    }
}
