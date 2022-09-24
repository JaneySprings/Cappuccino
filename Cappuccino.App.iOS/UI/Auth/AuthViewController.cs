using Cappuccino.Core.Network.Auth;

namespace Cappuccino.App.iOS.UI.Auth;


public partial class AuthViewController : UIViewController {
    private readonly AuthManager authManager = new AuthManager();

    public override void ViewDidAppear(bool animated) {
        base.ViewDidAppear(animated);

        this.authManager.Authorized += (sender, args) => {
            AppDelegate? appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
            appDelegate?.ChangeRootViewController(new RootViewController());
        };
        NSUrl url = NSUrl.FromString(this.authManager.BuildAuthorizationUri())!;
        NSUrlRequest request = NSUrlRequest.FromUrl(url);
        webView!.AddObserver(this, "URL", NSKeyValueObservingOptions.New, IntPtr.Zero);
        webView.LoadRequest(request);
    }

    public override void ObserveValue(NSString keyPath, NSObject ofObject, NSDictionary change, IntPtr context) {
        if (keyPath == "URL")
            this.authManager.TryAuthorizeFromUri(webView!.Url?.ToString() ?? "");
    }
}