using Cappuccino.Core.Network.Config;
using Scope = Cappuccino.Core.Network.Auth.Permissions;
using Cappuccino.App.iOS.UI.Auth;
using Cappuccino.Core.Network.Handlers;
using Cappuccino.App.iOS.UI;
using System.Globalization;

namespace Cappuccino.App.iOS;


[Register ("AppDelegate")]
public class AppDelegate : UIResponder, IUIApplicationDelegate {

    [Export("window")]
    public UIWindow? Window { get; set; }


    [Export ("application:didFinishLaunchingWithOptions:")]
    public bool FinishedLaunching (UIApplication application, NSDictionary launchOptions) {
        Window = new UIWindow(UIScreen.MainScreen.Bounds);

        UserNotifications.UNUserNotificationCenter.Current.RequestAuthorization(
            UserNotifications.UNAuthorizationOptions.Badge, (granted, error) => {}
        );

        ApiConfiguration config = new ApiConfiguration.Builder()
            .WithApiLanguage(CultureInfo.CurrentCulture.TwoLetterISOLanguageName)
            .WithAppId(7317599)
            .WithApiVersion("5.131")
            .WithLongPollVersion(12)
            .WithTokenStorageHandler(new KeychainProvider())
            .WithPermissions(new[] { Scope.Friends, Scope.Messages })
            .Build();

        CredentialsManager.ApplyConfiguration(config);
        TokenExpiredHandler.Expired += (error) => ChangeRootViewController(new AuthViewController());

        ChangeRootViewController(
            CredentialsManager.IsInternalTokenValid() ? new RootViewController() : new AuthViewController()
        );

        return true;
    }


    public void ChangeRootViewController(UIViewController controller) {
        Window!.RootViewController = controller;
        Window.MakeKeyAndVisible();
    }
}
