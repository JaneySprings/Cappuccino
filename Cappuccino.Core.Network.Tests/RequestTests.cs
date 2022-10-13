using Xunit;
using Cappuccino.Core.Network.Handlers;
namespace Cappuccino.Core.Network.Tests;


public class RequestTests: TestFixture {

    [Fact]
    public void TokenErrorHandlingTest() {
        SetupMockCredentials();
        
        const string response = "{\"error\":{\"error_code\":5,\"error_msg\":\"User authorization failed: access_token has expired.\"}}";
        var request = new TestRequest<string>();
        var handledMessage = "";
        TokenExpiredHandler.Expired += (error) => handledMessage = error?.InnerError?.ErrorMsg ?? "";

        Assert.Throws<ApiException>(() => request.ResultForResponse(response));
        Assert.Contains("access_token has expired", handledMessage);
    }

    [Fact]
    public void SingleErrorInvokeTest() {
        SetupEmptyCredentials();
        
        var request = new TestRequest<string>();
        var callback = new ApiCallback<string>();

        Api.Get(request, callback, 3);
        Assert.Contains("Access token incorrect", callback.Message);
        Assert.Equal(1, callback.RiseCount);
    }

    [Fact]
    public void NoTokenInRequestTest() {
        SetupEmptyCredentials();
        Assert.ThrowsAsync<ApiException>(() => new TestRequest<int>().Execute()).Wait();
    }

    [Fact]
    public void DuplicateParamsInRequestTest() {
        SetupEmptyCredentials();
        Assert.ThrowsAsync<ArgumentException>(() => { 
            var request = new TestRequest<int>();
            request.AddRequestParam("param1", "value1");
            request.AddRequestParam("param1", "value2");
            return request.Execute();
        }).Wait();
    }

    [Fact]
    public void NormalResponseTest() {
        SetupMockCredentials();
        
        const string response = "{\"response\":{\"messages\":5,\"calls\":2}}";
        var request = new TestRequest<Models.Account.GetCountersResponse>();
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