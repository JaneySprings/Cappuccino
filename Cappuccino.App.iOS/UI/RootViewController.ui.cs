﻿namespace Cappuccino.App.iOS.UI;


public partial class RootViewController : UITabBarController {
    public override void ViewDidLoad() {
        base.ViewDidLoad();

        var tabIcons = new string[] {
            "users_outline_32",
            "message_outline_32",
            "settings_outline_32"
        };
        var rootControllers = new UIViewController[] {
            new Contacts.ContactsViewController(),
            new Chats.ChatsViewController(),
            new Profile.ProfileViewController()
        };
        var navControllers = new List<UINavigationController>();


        for (int i = 0; i < 3; i++) {
            var navController = new UINavigationController(); 
            navController.TabBarItem = new UITabBarItem("", UIImage.FromBundle(tabIcons[i]), IntPtr.Zero);
            navController.TabBarItem.BadgeColor = Colors.Accent;
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
        this.SelectedIndex = new IntPtr(1);
        this.View!.BackgroundColor = Colors.Foreground;

        Initialize();
    }
}