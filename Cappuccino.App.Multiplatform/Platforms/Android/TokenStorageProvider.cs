using Cappuccino.Core.Network.Auth;
using Cappuccino.Core.Network.Handlers;

namespace Cappuccino.App.Multiplatform;

public class TokenStorageProvider: ITokenStorageHandler {
    public AccessToken? OnTokenRequested() {
        return null;
    }

    public void OnTokenReceived(AccessToken token) {
    }
}