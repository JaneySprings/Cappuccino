using System;
using System.Threading.Tasks;
using Cappuccino.Core.Network.Handlers;

namespace Cappuccino.Core.Network {

    public static class Api {
        public static async void Get<TResult>(ApiRequest<TResult> request, IRequestCallback<TResult>? callback = null, int retryCount = 3) {
            do {
                try {
                    TResult response = await request.Execute();
                    callback?.OnSuccess(response);
                    break;
                } catch (Exception e) {
                    callback?.OnError(e);
                }
            } while (--retryCount > 0);
        }
    }
}