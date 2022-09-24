using System.Collections.Generic;

namespace Cappuccino.Core.Network.Managing {
    public class CascadeRequestManager<TResult> : RequestManager<TResult> {
        protected override ApiRequest<TResult> BeforeExecute() {
            return RequestQueue[0];
        }

        protected override void AfterExecute() {
            RequestQueue.RemoveAt(0);
            if (RequestQueue.Count != 0)
                Execute();
        }
    }
}