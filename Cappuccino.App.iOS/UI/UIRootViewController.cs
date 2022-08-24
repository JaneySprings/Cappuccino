using System;
using UIKit;
using Foundation;
using Cappuccino.Core.Network;
using Cappuccino.App.iOS.UI.Contacts;
using System.Collections.Generic;
using Cappuccino.App.iOS.UI.Profile;
using Cappuccino.App.iOS.UI.Common;

namespace Cappuccino.App.iOS.UI {

    public class RootViewController : UITabBarController {
        public override void ViewDidLoad() {
            base.ViewDidLoad();

            var tabIcons = new string[] {
                "users_outline_32",
                "message_outline_32",
                "settings_outline_32"
            };
            var rootControllers = new UIViewController[] {
                new ContactsViewController(),
                new ContactsViewController(),
                new ProfileViewController()
            };
            var navControllers = new List<UINavigationController>();


            for (int i = 0; i < 3; i++) {
                var navController = new UINavigationController(); 
                navController.TabBarItem = new UITabBarItem("", UIImage.FromBundle(tabIcons[i]), i);
                navController.ViewControllers = new UIViewController[] { rootControllers[i] };
                navController.NavigationBar.ApplyDefaultAppearance();
                navControllers.Add(navController);
            }

            this.ViewControllers = new UIViewController[] {
                navControllers[0],
                navControllers[1],
                navControllers[2]
            };

            this.TabBar.ApplyDefaultAppearance();
        }
    }
}