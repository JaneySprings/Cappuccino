using Xunit;
using Cappuccino.Core.Network.Config;
using Cappuccino.Core.Network.Handlers;

namespace Cappuccino.Core.Network.Tests;


public class ValidationTests: TestFixture {

    [Fact]
    public void ValidInternalTokenTest() {
        SetupCredentialsWithTokenStorageHandler(new ValidTokenStorage());
        Assert.True(CredentialsManager.IsInternalTokenValid());
    }

    [Fact]
    public void ExpiredInternalTokenTest() {
        SetupCredentialsWithTokenStorageHandler(new ExpiredTokenStorage());

        var handler = new ValidationCallback();
        Assert.False(CredentialsManager.IsInternalTokenValid(handler));
        Assert.Contains("expired", handler.Message);
    }

    [Fact]
    public void InvalidInternalTokenTest() {
        SetupCredentialsWithTokenStorageHandler(new InvalidTokenStorage());

        var handler = new ValidationCallback();
        Assert.False(CredentialsManager.IsInternalTokenValid(handler));
        Assert.Contains("empty", handler.Message);
    }

    [Fact]
    public void NoTokenStorageHandlerTest() {
        SetupEmptyCredentials();

        var handler = new ValidationCallback();
        Assert.False(CredentialsManager.IsInternalTokenValid(handler));
        Assert.Contains(nameof(ITokenStorageHandler), handler.Message);
    }

    [Fact]
    public void ApplyingNullTokenTest() {
        SetupMockCredentials();

        var handler = new ValidationCallback();
        CredentialsManager.ApplyAccessToken(null, handler);
        Assert.Contains("null", handler.Message);
    }
}