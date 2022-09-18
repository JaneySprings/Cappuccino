using System.Collections.Generic;
using Cappuccino.Core.Network.Config;
using Scope = Cappuccino.Core.Network.Auth.Permissions;
using Foundation;
using UIKit;
using Cappuccino.App.iOS.UI.Auth;
using Cappuccino.Core.Network.Handlers;
using Cappuccino.App.iOS.UI;


namespace Cappuccino.App.iOS;

[Register ("AppDelegate")]
public class AppDelegate : UIResponder, IUIApplicationDelegate {

    [Export("window")]
    public UIWindow? Window { get; set; }

    [Export ("application:didFinishLaunchingWithOptions:")]
    public bool FinishedLaunching (UIApplication application, NSDictionary launchOptions) {
        Window = new UIWindow(UIScreen.MainScreen.Bounds);

        ApiConfiguration config = new ApiConfiguration.Builder()
            .SetApiLanguage("en")
            .SetAppId(7317599)
            .SetApiVersion("5.131")
            .SetLongPollVersion(3)
            .SetTokenStorageHandler(new KeychainProvider())
            .SetPermissions(new List<int> { Scope.Friends, Scope.Messages })
            .Build();

        CredentialsManager.ApplyConfiguration(config);
        TokenExpiredHandler.Expired += (sender, args) => ChangeRootViewController(new AuthViewController());

        ChangeRootViewController(CredentialsManager.IsInternalTokenValid() ?
            new RootViewController() : new AuthViewController());

        return true;
    }

    public void ChangeRootViewController(UIViewController controller) {
        Window!.RootViewController = controller;
        Window.MakeKeyAndVisible();
    }
}
