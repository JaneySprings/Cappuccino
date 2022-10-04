using Cappuccino.Core.Network;
using Cappuccino.Core.Network.Handlers;
using Cappuccino.Core.Network.Polling;
using Account = Cappuccino.Core.Network.Methods.Account;
using Models = Cappuccino.Core.Network.Models;

namespace Cappuccino.App.iOS.UI;


public partial class RootViewController {

//#if DEBUG
    private int totalUpdates = 0;
//    private int lpUpdates = 0;
//#endif

    private void Initialize() {
        RequestBadgeCounters(null);
    }

    public override void ViewDidAppear(bool animated) {
        base.ViewDidAppear(animated);
        LongPollManager.Instance.StartExecution();
        LongPollManager.Instance.HistoryUpdated += RequestBadgeCounters;
// //#if DEBUG
//         LongPollManager.Instance.CallHandler += () => { 
//             lpUpdates++;
//             this.TabBar.Items![0].BadgeValue = lpUpdates.ToString();
//         };
// //#endif
    }

    public override void ViewDidDisappear(bool animated) {
        base.ViewDidDisappear(animated);
        LongPollManager.Instance.HistoryUpdated -= RequestBadgeCounters;
        LongPollManager.Instance.StopExecution();
    }



    private void RequestBadgeCounters(Models.LongPollResponse? _) {
        Api.Get(new Account.GetCounters(), new ApiCallback<Models.Account.GetCountersResponse>()
            .OnSuccess(result => {
//#if DEBUG
                totalUpdates++;
                this.TabBar.Items![2].BadgeValue = totalUpdates.ToString();
//#endif
                var contactsBadgeValue = result.InnerResponse?.Friends ?? 0;
                var chatsBadgeValue = result.InnerResponse?.Messages ?? 0;

                this.TabBar.Items![0].BadgeValue = contactsBadgeValue == 0 ? null : contactsBadgeValue.ToString();
                this.TabBar.Items![1].BadgeValue = chatsBadgeValue == 0 ? null : chatsBadgeValue.ToString();
                UIApplication.SharedApplication.ApplicationIconBadgeNumber = new IntPtr(chatsBadgeValue == 0 ? -1 : chatsBadgeValue);
            })
            .OnError(reason => {})
        );
    }
}
