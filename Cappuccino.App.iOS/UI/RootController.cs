using System;
using UIKit;
using Foundation;
using Cappuccino.Core.Network;

namespace Cappuccino.App.iOS.UI {

    [Register("RootController")]
    public partial class RootController : UITabBarController {
        public RootController(IntPtr handle) : base(handle) { }

        public override void ViewDidLoad() {
            base.ViewDidLoad();
            LongPollExecutor.StartExecution();
        }

        public override void ViewDidUnload() {
            base.ViewDidUnload();
            LongPollExecutor.StopExecution();
        }

        public override void DidReceiveMemoryWarning() {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}