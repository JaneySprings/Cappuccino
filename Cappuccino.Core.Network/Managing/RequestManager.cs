using System.Collections.Generic;
using Cappuccino.Core.Network.Handlers;


namespace Cappuccino.Core.Network.Managing {

    public abstract class RequestManager<TResult>: IRequestCallback<TResult> {
        protected List<ApiRequest<TResult>> RequestQueue { get; set; }
        protected int LastRequestIndex { get; set; }
        public IRequestCallback<TResult>? RequestCallback { set; private get; }


        public RequestManager() {
            RequestQueue = new List<ApiRequest<TResult>>();
        }


        public void AddRequest(ApiRequest<TResult> request) {
            RequestQueue.Add(request);

            if (RequestQueue.Count == 1)
                Execute();
        }

        protected void Execute() {
            var request = BeforeExecute();

            LastRequestIndex = RequestQueue.IndexOf(request);
            Api.Get(request, this);
        }


        protected abstract ApiRequest<TResult> BeforeExecute();
        protected abstract void AfterExecute();
        

        void IRequestCallback<TResult>.OnSuccess(TResult result) {
            RequestCallback?.OnSuccess(result);
            AfterExecute();
            
        }
        void IRequestCallback<TResult>.OnError(string reason) {
            RequestCallback?.OnError(reason);
            AfterExecute();
        }  
    }
}

