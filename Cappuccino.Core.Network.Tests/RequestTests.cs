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

    [Fact]
    public void NormalResponseTest() {
        SetupMockCredentials();
        
        var request = new TestRequest<Models.Account.GetCountersResponse>();
        var response = "{\"response\":{\"messages\":5,\"calls\":2}}";
        var handledMessage = "";
        TokenExpiredHandler.Expired += (error) => handledMessage = error?.InnerError?.ErrorMsg ?? "";

        var model = request.ResultForResponse(response);

        Assert.Equal(5, model.InnerResponse?.Messages);
        Assert.Equal(2, model.InnerResponse?.Calls);
        Assert.Equal(0, model.InnerResponse?.Friends);
        Assert.Equal(string.Empty, handledMessage);
    }

    [Fact]
    public void PassingArgumentsTest() {
        SetupMockCredentials();
        
        var request = new TestRequest<int>();
        var args = request.Arguments;

        Assert.Equal(5, args.Count);

        foreach(var arg in args) {
            switch(arg.Key) {
                case "string":                  Assert.Equal("test",            arg.Value); break;
                case "int":                     Assert.Equal("0",               arg.Value); break;
                case "bool":                    Assert.Equal("false",           arg.Value); break;
                case "IEnumerable<string>":     Assert.Equal("test1,test2",     arg.Value); break;
                case "IEnumerable<int>":        Assert.Equal("1,2",             arg.Value); break;
                case "ref":                     Assert.Fail("Find extra arg \"ref\"");      break;
            }
        }
    }

}