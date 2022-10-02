using Cappuccino.Core.Network.Polling;

namespace Cappuccino.App.iOS.UI;


public partial class RootViewController {
    public override void ViewDidAppear(bool animated) {
        base.ViewDidAppear(animated);
        LongPollManager.Instance.StartExecution();
    }

    public override void ViewDidDisappear(bool animated) {
        base.ViewDidDisappear(animated);
        LongPollManager.Instance.StopExecution();
    }
 }
