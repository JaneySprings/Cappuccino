namespace Cappuccino.App.iOS.UI.Auth;


public partial class AuthViewController {
    private WebKit.WKWebView? webView;

    public override void ViewDidLoad() {
        base.ViewDidLoad();

        this.webView = new WebKit.WKWebView(CGRect.Empty, new WebKit.WKWebViewConfiguration());
        this.View!.AddSubview(webView);

        SetNeedsStatusBarAppearanceUpdate();
        Initialize();
    }

    public override void ViewDidLayoutSubviews() {
        base.ViewDidLayoutSubviews();
        this.webView!.Frame = View!.Bounds;
    }

    public override UIStatusBarStyle PreferredStatusBarStyle() => UIStatusBarStyle.DarkContent;
}
