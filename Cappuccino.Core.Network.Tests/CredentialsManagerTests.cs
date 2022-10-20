using Xunit;
using Cappuccino.Core.Network.Config;

namespace Cappuccino.Core.Network.Tests;

public class CredentialsManagerTests: TestFixture {

    [Fact]
    public void ChangingAccessTokenInRuntimeTest() {
        SetupCredentialsWithTokenStorageHandler(new NormalTokenStorage());

        CredentialsManager.ApplyAccessToken(TokenFactory.ValidUnlimitedTokenForUser(1));
        Assert.Equal(1, CredentialsManager.CurrentUserId);

        CredentialsManager.ApplyAccessToken(TokenFactory.InvalidExpired);
        Assert.Equal(1, CredentialsManager.CurrentUserId);

        CredentialsManager.ApplyAccessToken(TokenFactory.ValidUnlimitedTokenForUser(2));
        Assert.Equal(2, CredentialsManager.CurrentUserId);
    }
}
