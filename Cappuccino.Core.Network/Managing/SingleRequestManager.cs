using System.Collections.Generic;

namespace Cappuccino.Core.Network.Managing {
    public class SingleRequestManager<TResult> : RequestManager<TResult> {
        protected override ApiRequest<TResult> BeforeExecute() {
            return RequestQueue[RequestQueue.Count - 1];
        }

        protected override void AfterExecute() {
            RequestQueue.RemoveRange(0, LastRequestIndex + 1);
            if (RequestQueue.Count != 0)
                Execute();
        }
    }
}