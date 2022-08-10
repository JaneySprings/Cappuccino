using System;
using Cappuccino.Core.Network.Handlers;

namespace Cappuccino.Core.Network {

    public enum RequestPriority { Immediate, Single }

    public static class Api {
        private static int _requestQueue = 0;

        public static async void Get<TResult>(ApiRequest<TResult> request,
                                              IRequestCallback<TResult>? callback = null,
                                              RequestPriority priority = RequestPriority.Immediate) {

            if (_requestQueue != 0 && priority == RequestPriority.Single) {
                callback?.OnBusy(_requestQueue);
                return;
            }

            _requestQueue++;
            try {
                TResult response = await request.Execute();
                callback?.OnSuccess(response);
            } catch (Exception e) {
                callback?.OnError(e.Message);
            }
            _requestQueue--;
        }
    }
}