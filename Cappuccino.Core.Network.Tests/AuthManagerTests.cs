using Xunit;
using Cappuccino.Core.Network.Auth;
using Cappuccino.Core.Network.Config;
using Cappuccino.Core.Network.Handlers;

namespace Cappuccino.Core.Network.Tests;


public class AuthManagerTests: TestFixture {

    public AuthManagerTests() {
        var permissions = new int[] { Permissions.Friends, Permissions.Status, Permissions.Photos };
        CredentialsManager.ApplyConfiguration(new ApiConfiguration.Builder()
            .WithTokenStorageHandler(new AssertionTokenStorage())
            .WithPermissions(permissions)
            .WithAppId(1)
            .Build()
        );
    }


    [Fact]
    public void AuthorizationUriTest() {
        var manager = new ImplicitAuthentificator();
        var missingCount = 4;
        var tokens = manager.BuildAuthorizationUri().Split('?')[1].Split('&');

        foreach (string token in tokens) {
            var pair = token.Split('=');
            switch(pair[0]) {
                case "client_id":       Assert.Equal("1", pair[1]);        missingCount--;  break;
                case "scope":           Assert.Equal("1030", pair[1]);     missingCount--;  break;
                case "response_type":   Assert.Equal("token", pair[1]);    missingCount--;  break;
                case "display":         Assert.Equal("mobile", pair[1]);   missingCount--;  break;
            }
        }

        Assert.Equal(0, missingCount);
    }

    [Fact]
    public void AuthorizationTest() {
        var manager = new ImplicitAuthentificator();
        var handler = new ValidationCallback { DebugLog = true };
        var authorizedCount = 0;
        var credentials = "access_token=123&expires_in=10000&user_id=1";
        var wrong = "access_token=321&expires_in=-10000&user_id=2";

        manager.Authorized += (s, e) => authorizedCount++;

        manager.TryAuthorizeFromUri($"https://test.blank.com#{credentials}", handler);
        Assert.Equal(0, authorizedCount);

        manager.TryAuthorizeFromUri($"https://api.vk.com#{credentials}", handler);
        Assert.Equal(0, authorizedCount);

        manager.TryAuthorizeFromUri($"https://oauth.vk.com/blank.html#{credentials}", handler);
        Assert.Equal(1, authorizedCount);

        manager.TryAuthorizeFromUri("https://oauth.vk.com/blank.html", handler);
        Assert.Equal(1, authorizedCount);

        manager.TryAuthorizeFromUri($"https://oauth.vk.com/blank.html#{wrong}", handler);
        Assert.Equal(1, authorizedCount);
    }

}