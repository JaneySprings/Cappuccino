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
            TintColor = Colors.Accent
        };

        this.actionMain.Clicked += clicked;
        this.navigationItem!.RightBarButtonItems = new UIBarButtonItem[] { this.actionMain };

        return this;
    }

    public NavigationBarBuilder WithSubAction(string image, EventHandler clicked) {
        this.actionSub = new UIBarButtonItem {
            Image = UIImage.FromBundle(image),
            TintColor = Colors.Accent
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

    public NavigationBarBuilder WithSearch() {
        var controller = new UISearchController();
        controller.Active = true;
            
        this.navigationItem!.HidesSearchBarWhenScrolling = true;
        this.navigationItem!.SearchController = controller;

        return this;
    }

    public NavigationBarBuilder WithSearchTextChangedEvent(EventHandler<UISearchBarTextChangedEventArgs> textChanged) {
        this.navigationItem!.SearchController!.SearchBar.TextChanged += textChanged;

        return this;
    }

    public NavigationBarBuilder WithSearchCancellEvent(EventHandler textCancelled) {
        this.navigationItem!.SearchController!.SearchBar.CancelButtonClicked += textCancelled;

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
