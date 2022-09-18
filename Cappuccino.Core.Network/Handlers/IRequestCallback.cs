using System;

namespace Cappuccino.Core.Network.Handlers {

    public interface IRequestCallback<in TResult> {
        internal void OnSuccess(TResult result);
        internal void OnError(string reason);
    }
}