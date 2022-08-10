using System;

namespace Cappuccino.Core.Network.Handlers {

    public class ApiCallback<TResult> : IRequestCallback<TResult> {
        private Action<TResult>? onSuccess;
        private Action<string>? onError;
        private Action<int>? onBusy;

        public ApiCallback<TResult> OnSuccess(Action<TResult> success) {
            this.onSuccess = success;
            return this;
        }
        public ApiCallback<TResult> OnError(Action<string> error) {
            this.onError = error;
            return this;
        }
        public ApiCallback<TResult> OnBusy(Action<int> busy) {
            this.onBusy = busy;
            return this;
        }

        public void OnSuccess(TResult result) { this.onSuccess?.Invoke(result); }
        public void OnError(string reason) { this.onError?.Invoke(reason); }
        public void OnBusy(int count) { this.onBusy?.Invoke(count); }

    }
}