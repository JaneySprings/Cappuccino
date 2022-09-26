using Cappuccino.Core.Network.Auth;
using Cappuccino.Core.Network.Handlers;
using Xunit;

namespace Cappuccino.Core.Network.Tests;


public class ValidTokenStorage: ITokenStorageHandler {
    public AccessToken? OnTokenRequested() => new AccessToken("1", -1, 1);
    public void OnTokenReceived(AccessToken token) {}
}

public class ExpiredTokenStorage: ITokenStorageHandler {
    public AccessToken? OnTokenRequested() => new AccessToken("1", DateTimeOffset.Now.ToUnixTimeSeconds() - 10000, 1);
    public void OnTokenReceived(AccessToken token) {}
}

public class InvalidTokenStorage: ITokenStorageHandler {
    public AccessToken? OnTokenRequested() => new AccessToken("", DateTimeOffset.Now.ToUnixTimeSeconds() + 10000, 1);
    public void OnTokenReceived(AccessToken token) {}
}

public class AssertionTokenStorage: ITokenStorageHandler {
    public AccessToken? OnTokenRequested() => null;
    public void OnTokenReceived(AccessToken token) {
        Assert.Equal("123", token.Token);
        Assert.Equal(1, token.UserId);
        Assert.True(token.ExpiresIn > DateTimeOffset.Now.ToUnixTimeSeconds());
    }
}