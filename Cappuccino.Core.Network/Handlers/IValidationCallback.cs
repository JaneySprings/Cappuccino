namespace Cappuccino.Core.Network.Handlers {

    public interface IValidationCallback {
        public void OnValidationFail(string reason);
    }
}