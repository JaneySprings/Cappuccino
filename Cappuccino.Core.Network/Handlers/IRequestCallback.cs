namespace Cappuccino.Core.Network.Handlers {

    public interface IRequestCallback<in TResult> {
        public void OnSuccess(TResult result);
        public void OnError(string reason);
        public void OnBusy(int count);
    }
}