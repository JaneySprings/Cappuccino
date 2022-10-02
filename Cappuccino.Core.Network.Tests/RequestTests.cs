using Xunit;
using Cappuccino.Core.Network.Config;
using Cappuccino.Core.Network.Handlers;

namespace Cappuccino.Core.Network.Tests;


public class RequestTests: TestFixture {

    [Fact]
    public void TokenErrorHandlingTest() {
        SetupMockCredentials();
        
        var request = new TestRequest<string>();
        var response = "{\"error\":{\"error_code\":5,\"error_msg\":\"User authorization failed: access_token has expired.\"}}";
        var handledMessage = "";
        TokenExpiredHandler.Expired += (error) => handledMessage = error?.InnerError?.ErrorMsg ?? "";

        Assert.Throws<Exception>(() => request.ResultForResponse(response));
        Assert.Contains("access_token has expired", handledMessage);
    }

    [Fact]
    public void NoTokenInRequestTest() {
        SetupEmptyCredentials();
        Assert.ThrowsAsync<Exception>(() => new TestRequest<int>().Execute()).Wait();
    }

}