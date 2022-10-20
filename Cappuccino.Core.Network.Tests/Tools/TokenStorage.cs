using Cappuccino.Core.Network.Auth;
using Cappuccino.Core.Network.Handlers;

namespace Cappuccino.Core.Network.Tests;


public class ValidTokenStorage: ITokenStorageHandler {
    public AccessToken? OnTokenRequested() => TokenFactory.ValidUnlimited;
    public void OnTokenReceived(AccessToken token) {}
}

public class ExpiredTokenStorage: ITokenStorageHandler {
    public AccessToken? OnTokenRequested() => TokenFactory.InvalidExpired;
    public void OnTokenReceived(AccessToken token) {}
}

public class InvalidTokenStorage: ITokenStorageHandler {
    public AccessToken? OnTokenRequested() => TokenFactory.Invalid;
    public void OnTokenReceived(AccessToken token) {}
}

public class NormalTokenStorage: ITokenStorageHandler {
    private AccessToken? savedToken;

    public AccessToken? OnTokenRequested() => savedToken;
    public void OnTokenReceived(AccessToken token) { savedToken = token; }
}