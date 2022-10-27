using Cappuccino.Core.Network;
using Cappuccino.Core.Network.Handlers;
using Cappuccino.Core.Network.Polling;
using Account = Cappuccino.Core.Network.Methods.Account;
using Models = Cappuccino.Core.Network.Models;

namespace Cappuccino.App.iOS.UI;


public partial class RootViewController {
    private void Initialize() {
        RequestBadgeCounters(null);
    }

    public override void ViewDidAppear(bool animated) {
        base.ViewDidAppear(animated);
        LongPollManager.Instance.StartExecution();
        LongPollManager.Instance.MessageReceived += RequestBadgeCounters;
//#if DEBUG
        LongPollManager.Instance.ErrorReceived += exception => {
            var alert = new UIAlertView(exception.Message, exception.StackTrace, null, "OK", null);
            alert.Show();
        };
//#endif
    }

    public override void ViewDidDisappear(bool animated) {
        base.ViewDidDisappear(animated);
        LongPollManager.Instance.MessageReceived -= RequestBadgeCounters;
        LongPollManager.Instance.StopExecution();
    }



    private void RequestBadgeCounters(Models.LongPollResponse? _) {
        Api.Get(new Account.GetCounters(), new ApiCallback<Account.GetCounters.Response>()
            .OnSuccess(result => {
                var contactsBadgeValue = result.Inner?.Friends ?? 0;
                var chatsBadgeValue = result.Inner?.Messages ?? 0;

                this.TabBar.Items![0].BadgeValue = contactsBadgeValue == 0 ? null : contactsBadgeValue.ToString();
                this.TabBar.Items![1].BadgeValue = chatsBadgeValue == 0 ? null : chatsBadgeValue.ToString();
                UIApplication.SharedApplication.ApplicationIconBadgeNumber = new IntPtr(chatsBadgeValue == 0 ? -1 : chatsBadgeValue);
            })
            .OnError(reason => {})
        );
    }
}
