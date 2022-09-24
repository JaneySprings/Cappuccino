namespace Cappuccino.App.iOS.UI.Common;


public class NavigationBarBuilder {
    private UIBarButtonItem? actionMain;
    private UIBarButtonItem? actionSub;
    private UINavigationItem? navigationItem;


    public NavigationBarBuilder WithElement(UINavigationItem element) {
        this.navigationItem = element;
        return this;
    }

    public NavigationBarBuilder WithMainAction(string image, EventHandler clicked) {
        this.actionMain = new UIBarButtonItem {
            Image = UIImage.FromBundle(image),
            TintColor = UIColor.FromName("accent")
        };

        this.actionMain.Clicked += clicked;
        this.navigationItem!.RightBarButtonItems = new UIBarButtonItem[] { this.actionMain };

        return this;
    }

    public NavigationBarBuilder WithSubAction(string image, EventHandler clicked) {
        this.actionSub = new UIBarButtonItem {
            Image = UIImage.FromBundle(image),
            TintColor = UIColor.FromName("accent")
        };

        this.actionSub.Clicked += clicked;
        this.navigationItem!.RightBarButtonItems = new UIBarButtonItem[] {
            this.actionSub, this.actionMain!
        };

        return this;
    }

    public NavigationBarBuilder WithTitle(string text) {
        this.navigationItem!.Title = text;
        return this;
    }

    public NavigationBarBuilder WithSearch(EventHandler<UISearchBarTextChangedEventArgs> textChanged) {
        var controller = new UISearchController();
        controller.SearchBar.TextChanged += textChanged;
        controller.Active = true;
            
        this.navigationItem!.HidesSearchBarWhenScrolling = true;
        this.navigationItem!.SearchController = controller;

        return this;
    }

    public NavigationBarBuilder WithSearchIcon(string image, EventHandler clicked) {
        this.navigationItem!.SearchController!.SearchBar.ShowsBookmarkButton = true;
        this.navigationItem!.SearchController!.SearchBar.SetImageforSearchBarIcon(
            UIImage.FromBundle(image), UISearchBarIcon.Bookmark, UIControlState.Normal
        );
        this.navigationItem!.SearchController!.SearchBar.BookmarkButtonClicked += clicked;

        return this;
    }

    public void Apply() {
        this.actionMain = null;
        this.actionSub = null;
        this.navigationItem = null;
    }
}
