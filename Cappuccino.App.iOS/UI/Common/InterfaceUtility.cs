namespace Cappuccino.App.iOS.UI.Common;

public static class InterfaceUtility {

    public static CGSize LayoutSegmentedSection(UISegmentedControl control, UIView superview, SegmentedControlOptions options) {
        var header = new UILabel();
        header.Text = options.Title;

        header.ApplyCaption2Appearance();
        control.ApplyDefaultAppearance();

        var headerSize = header.SizeThatFits(options.ContentSize);
        header.Frame = new CGRect(options.HorizontalPadding, options.OriginY + options.VerticalPadding, headerSize.Width, headerSize.Height);

        for (var i = 0; i < options.Options!.Length; i++) {
            control.InsertSegment(options.Options[i], new IntPtr(i), false);
        }

        var controlSize = control!.SizeThatFits(options.ContentSize);
        control.Frame = new CGRect(options.HorizontalPadding, options.OriginY + options.VerticalPadding + headerSize.Height + options.Spacing,
            options.ContentSize.Width - 2 * options.HorizontalPadding, controlSize.Height + 2 * options.HeightAmplifier);

        superview.AddSubview(header);
        superview.AddSubview(control);

        return new CGSize(options.ContentSize.Width, 2 * options.VerticalPadding + headerSize.Height + controlSize.Height + options.Spacing);
    }


    public static void LayoutNavigationBar(UINavigationItem navigationItem, NavigationBarOptions options) {
        navigationItem.Title = options.Title;

        if (options.Actions != null) {
            foreach (var action in options.Actions) {
                var barButton = new UIBarButtonItem {
                    Image = UIImage.FromBundle(action.Image!),
                    TintColor = Colors.Accent
                };

                barButton.Clicked += action.Clicked;

                if (navigationItem.RightBarButtonItems == null) {
                    navigationItem.RightBarButtonItems = new UIBarButtonItem[] { barButton };
                } else {
                    var items = navigationItem.RightBarButtonItems.ToList();
                    items.Add(barButton);
                    navigationItem.RightBarButtonItems = items.ToArray();
                }
            }
        }

        if (!options.Search) 
            return;

        var controller = new UISearchController();
        controller.Active = true;
        
        navigationItem.HidesSearchBarWhenScrolling = true;
        navigationItem.SearchController = controller;

        if (options.SearchTextChanged != null) {
            navigationItem.SearchController!.SearchBar.TextChanged += options.SearchTextChanged;
        }

        if (options.SearchCancelled != null) {
            navigationItem.SearchController!.SearchBar.CancelButtonClicked += options.SearchCancelled;
        }

        if (options.SearchAction != null) {
            navigationItem.SearchController!.SearchBar.ShowsBookmarkButton = true;
            navigationItem.SearchController!.SearchBar.SetImageforSearchBarIcon(
                UIImage.FromBundle(options.SearchAction.Image!), UISearchBarIcon.Bookmark, UIControlState.Normal
            );
            navigationItem.SearchController!.SearchBar.BookmarkButtonClicked += options.SearchAction.Clicked;
        }
    }
}