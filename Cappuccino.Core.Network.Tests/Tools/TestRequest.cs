namespace Cappuccino.Core.Network.Tests;

public class TestRequest<TResult>: ApiRequest<TResult> {
    public TestRequest() : base("TestRequest") {}

    public TResult ResultForResponse(string response) => base.OnServerResponseReceived(response);
}