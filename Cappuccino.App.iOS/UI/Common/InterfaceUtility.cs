namespace Cappuccino.App.iOS.UI.Common;

public static class InterfaceUtility {

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