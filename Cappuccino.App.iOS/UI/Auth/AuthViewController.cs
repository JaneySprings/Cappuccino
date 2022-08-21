using System;
using Cappuccino.Core.Network.Auth;
using Foundation;
using UIKit;

namespace Cappuccino.App.iOS.UI.Auth {
    [Register("AuthViewController")]
    public partial class AuthViewController : UIViewController {
        private readonly AuthManager authManager = new AuthManager();

        public override void ViewDidAppear(bool animated) {
            base.ViewDidAppear(animated);

            this.authManager.Authorized += (sender, args) => {
                SceneDelegate? sceneDelegate = View?.Window.WindowScene?.Delegate as SceneDelegate;
                sceneDelegate?.ChangeViewControllerForHosting();
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
}


