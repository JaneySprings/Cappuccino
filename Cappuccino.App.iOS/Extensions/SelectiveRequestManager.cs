using Cappuccino.Core.Network.Handlers;
using Cappuccino.Core.Network;

namespace Cappuccino.App.iOS.Extensions;


public class SelectiveRequestManager<TResult>: IRequestCallback<TResult> {
    private List<ApiRequest<TResult>> requestQueue;
    private int lastRequestIndex;
    public IRequestCallback<TResult>? RequestCallback { set; private get; }


    public SelectiveRequestManager() {
        requestQueue = new List<ApiRequest<TResult>>();
    }


    public void AddRequest(ApiRequest<TResult> request) {
        requestQueue.Add(request);

        if (requestQueue.Count == 1)
            Execute();
    }

    private void Execute() {
        var request = BeforeExecute();

        lastRequestIndex = requestQueue.IndexOf(request);
        Api.Get(request, this);
    }


    private ApiRequest<TResult> BeforeExecute() {
            return requestQueue[requestQueue.Count - 1];
        }

    private void AfterExecute() {
        requestQueue.RemoveRange(0, lastRequestIndex + 1);
        if (requestQueue.Count != 0)
            Execute();
    }


    void IRequestCallback<TResult>.OnSuccess(TResult result) {
        RequestCallback?.OnSuccess(result);
        AfterExecute();
        
    }
    void IRequestCallback<TResult>.OnError(Exception exception) {
        RequestCallback?.OnError(exception);
        AfterExecute();
    }  
}