using Xunit;
using Cappuccino.Core.Network.Config;
using Cappuccino.Core.Network.Handlers;

namespace Cappuccino.Core.Network.Tests;


[Collection("Sequential")]
public abstract class TestFixture {
    protected ApiConfiguration EmptyConfiguration => new ApiConfiguration.Builder().Build();

    protected ApiConfiguration MockConfiguration => new ApiConfiguration.Builder()
        .WithTokenStorageHandler(new ValidTokenStorage())
        .WithAppId(1)
        .Build();


    protected void SetupEmptyCredentials() {
        CredentialsManager.ApplyConfiguration(EmptyConfiguration);
    }

    protected void SetupMockCredentials() {
        CredentialsManager.ApplyConfiguration(MockConfiguration);
    }

    protected void SetupCredentialsWithTokenStorageHandler(ITokenStorageHandler handler) {
        CredentialsManager.ApplyConfiguration(new ApiConfiguration.Builder()
            .WithTokenStorageHandler(handler)
            .Build());
    }

}