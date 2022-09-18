using System;
using System.Threading.Tasks;
using Cappuccino.Core.Network.Handlers;

namespace Cappuccino.Core.Network {

    public class Api {
        public static async void Get<TResult>(ApiRequest<TResult> request,
                              IRequestCallback<TResult>? callback = null) {

            try {
                TResult response = await request.Execute();
                callback?.OnSuccess(response);
            } catch (Exception e) {
                callback?.OnError(e.Message);
            }
        }
    }
}