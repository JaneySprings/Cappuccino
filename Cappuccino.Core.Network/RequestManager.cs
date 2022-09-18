using System.Collections.Generic;
using Cappuccino.Core.Network.Handlers;


namespace Cappuccino.Core.Network {

    public abstract class RequestManager<TResult>: IRequestCallback<TResult> {
        protected List<ApiRequest<TResult>> requestQueue = new List<ApiRequest<TResult>>();
        protected int lastRequestIndex;
        public IRequestCallback<TResult>? RequestCallback { set; private get; }


        public void AddRequest(ApiRequest<TResult> request) {
            requestQueue.Add(request);

            if (requestQueue.Count == 1)
                Execute();
        }

        protected void Execute() {
            var request = BeforeExecute();

            lastRequestIndex = requestQueue.IndexOf(request);\
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


    public class CascadeRequestManager<TResult> : RequestManager<TResult> {
        protected override ApiRequest<TResult> BeforeExecute() {
            return this.requestQueue[0];
        }

        protected override void AfterExecute() {
            this.requestQueue.RemoveAt(0);
            if (this.requestQueue.Count != 0)
                Execute();
        }
    }

    public class SingleRequestManager<TResult> : RequestManager<TResult> {
        protected override ApiRequest<TResult> BeforeExecute() {
            return this.requestQueue[this.requestQueue.Count - 1];
        }

        protected override void AfterExecute() {
            this.requestQueue.RemoveRange(0, this.lastRequestIndex + 1);
            if (this.requestQueue.Count != 0)
                Execute();
        }
    }
}

