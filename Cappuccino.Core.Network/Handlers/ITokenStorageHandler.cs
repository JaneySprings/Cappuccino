using Cappuccino.Core.Network.Auth;

namespace Cappuccino.Core.Network.Handlers {
    
    public interface ITokenStorageHandler {
        public AccessToken? OnTokenRequested();
        public void OnTokenReceived(AccessToken token);
    }
    
}