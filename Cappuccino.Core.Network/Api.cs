using System;
using System.Threading;
using Cappuccino.Core.Network.Handlers;

namespace Cappuccino.Core.Network {
    [Obsolete("Use ApiMethod.GetAsync instead")]
    public static class Api {
        public static async void Get<TResult>(ApiRequest<TResult> request, IRequestCallback<TResult>? callback = null, int retryCount = 3) {
            TResult? response = default;
            
            do {
                try {
                    response = await request.ExecuteAsync(CancellationToken.None);
                    break;
                } catch (ApiException e) {
                    callback?.OnError(e);
                    break;
                }
            } while (--retryCount > 0);

            if (response != null) {
                callback?.OnSuccess(response);
            }
        }
    }
}