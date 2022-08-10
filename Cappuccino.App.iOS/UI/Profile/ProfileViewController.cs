using System;

using UIKit;
using Foundation;

namespace Cappuccino.App.iOS.UI.Profile {

    [Register("ProfileViewController")]
    public partial class ProfileViewController : UIViewController {
        public ProfileViewController(IntPtr handle) : base (handle) {
        }

        public override void ViewDidLoad() {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning() {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}