using System;
using Cappuccino.App.iOS.UI.Styles;
using UIKit;

namespace Cappuccino.App.iOS.UI.Common {
    public class NavigationBarBuilder {
        private UIBarButtonItem? actionMain;
        private UIBarButtonItem? actionSub;
        private UINavigationItem? navigationItem;


        public NavigationBarBuilder WithElement(UINavigationItem element) {
            this.navigationItem = element;
            return this;
        }

        public NavigationBarBuilder WithDefaultStyle(UINavigationBar bar) {      
            var attributes = new UIStringAttributes();
            attributes.Font = UIFont.FromName(
                AppStyle.Instance.TextHeading1Font.Name,
                AppStyle.Instance.TextHeading1Font.Size
            );
            attributes.ForegroundColor = UIColor.FromName(
                AppStyle.Instance.TextHeading1Font.Color
            );

            var appearance = new UINavigationBarAppearance();
            appearance.BackgroundColor = UIColor.FromName(
                AppStyle.Instance.ForegroundColorName
            );
            appearance.TitleTextAttributes = attributes;
            appearance.ShadowColor = UIColor.Clear;

            var mock = new UIBarButtonItem {
                TintColor = UIColor.FromName(AppStyle.Instance.AccentColorName)
            };
            bar.Items[0].BackBarButtonItem = mock;

            bar.StandardAppearance = appearance;
            bar.ScrollEdgeAppearance = appearance;
            bar.CompactAppearance = appearance;
            

            return this;
        }

        public NavigationBarBuilder WithMainAction(string image, EventHandler clicked) {
            this.actionMain = new UIBarButtonItem {
                Image = UIImage.FromBundle(image),
                TintColor = UIColor.FromName(AppStyle.Instance.AccentColorName)
            };

            this.actionMain.Clicked += clicked;
            this.navigationItem!.RightBarButtonItems = new UIBarButtonItem[] { this.actionMain };

            return this;
        }

        public NavigationBarBuilder WithSubAction(string image, EventHandler clicked) {
            this.actionSub = new UIBarButtonItem {
                Image = UIImage.FromBundle(image),
                TintColor = UIColor.FromName(AppStyle.Instance.AccentColorName)
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
            
            this.navigationItem!.HidesSearchBarWhenScrolling = false;
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
}

