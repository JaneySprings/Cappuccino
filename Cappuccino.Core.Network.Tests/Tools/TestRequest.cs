namespace Cappuccino.Core.Network.Tests;

public class TestRequest<TResult>: ApiRequest<TResult> {
    public TestRequest() : base("TestRequest") {
        const string? nullData = null;

        ClearParams();
        AddParam("string", "test");
        AddParam("int", 0);
        AddParam("bool", false);
        AddParam("IEnumerable<string>", new List<string> { "test1", "test2" });
        AddParam("IEnumerable<int>", new List<int> { 1, 2 });
        AddParam("ref", nullData);
    }

    public Dictionary<string, string> Arguments => base.Args;
    public TResult ResultForResponse(string response) => base.OnServerResponseReceived(response);
    public void AddRequestParam(string name, string? value) => base.AddParam(name, value);
}