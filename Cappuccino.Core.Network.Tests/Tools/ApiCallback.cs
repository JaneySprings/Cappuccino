using Cappuccino.Core.Network.Handlers;

namespace Cappuccino.Core.Network.Tests;

public class ApiCallback<TResult>: IRequestCallback<TResult> { 
    public string? Message { get; private set; }
    public int RiseCount { get; private set; }

    public void OnError(ApiException exception) {
        Message = exception.ErrorMessage ?? exception.Message;
        RiseCount++;
    }
    public void OnSuccess(TResult result) {}
}