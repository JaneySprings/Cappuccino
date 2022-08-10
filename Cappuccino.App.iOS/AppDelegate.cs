using System.Collections.Generic;
using Cappuccino.Core.Network.Config;
using Scope = Cappuccino.Core.Network.Auth.Permissions;
using Foundation;
using UIKit;

namespace Cappuccino.App.iOS {

    [Register ("AppDelegate")]
    public class AppDelegate : UIResponder, IUIApplicationDelegate {

        [Export("window")]
        public UIWindow? Window { get; set; }

        [Export ("application:didFinishLaunchingWithOptions:")]
        public bool FinishedLaunching (UIApplication application, NSDictionary launchOptions) {
            ApiConfiguration config = new ApiConfiguration.Builder()
                .SetApiLanguage("en")
                .SetApiVersion("5.131")
                .SetLongPollVersion(3)
                .SetTokenStorageHandler(new KeychainProvider())
                .SetPermissions(new List<int> {Scope.Friends, Scope.Messages})
                .Build();

            CredentialsManager.ApplyConfiguration(config);

            return true;
        }


        // UISceneSession Lifecycle
        [Export ("application:configurationForConnectingSceneSession:options:")]
        public UISceneConfiguration GetConfiguration (UIApplication application, UISceneSession connectingSceneSession, UISceneConnectionOptions options) {
            // Called when a new scene session is being created.
            // Use this method to select a configuration to create the new scene with.
            return UISceneConfiguration.Create ("Default Configuration", connectingSceneSession.Role);
        }

        [Export ("application:didDiscardSceneSessions:")]
        public void DidDiscardSceneSessions (UIApplication application, NSSet<UISceneSession> sceneSessions) {
            // Called when the user discards a scene session.
            // If any sessions were discarded while the application was not running, this will be called shortly after `FinishedLaunching`.
            // Use this method to release any resources that were specific to the discarded scenes, as they will not return.
        }
    }
}


