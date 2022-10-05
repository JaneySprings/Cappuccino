using System;
using System.Threading.Tasks;
using Cappuccino.Core.Network.Models;
using Cappuccino.Core.Network.Handlers;
using Cappuccino.Core.Network.Internal;
using Cappuccino.Core.Network.Models.Messages;
using Cappuccino.Core.Network.Methods.Messages;


namespace Cappuccino.Core.Network.Polling { 

    internal class PollingLooper : IRequestCallback<GetLongPollServerResponse>, IPollingErrorHandler {
        private GetLongPollServerResponse.Response? _serverCredentials;
        private GetLongPollServerResponse.Response? serverCredentials { 
            get => _serverCredentials;
            set {
                _serverCredentials = value;
                IsActive = true;
                Loop();
            }
        }
        public bool IsActive { get; private set; }
        public Action<Models.LongPollResponse>? updateHandler { get; set; }
        public Action<string>? errorHandler { get; set; }

//#if DEBUG
        public Action? callHandler { get; set; }
        public int _errors_ = 0;
//#endif


        public void Prepare() => Api.Get(new GetLongPollServer { NeedPts = 1 }, this);
        public void Interrupt() => IsActive = false;


        private async void Loop() {
            while (IsActive) {
//#if DEBUG
                callHandler?.Invoke();
//#endif
                try {
                    var request = new LongPollRequest(serverCredentials!, this);
                    var result = await request.Execute();

                    _serverCredentials!.Ts = result.Ts;

                    if (result.Updates?.Count != 0 && IsActive)
                        updateHandler?.Invoke(result);
                } catch (Exception e) {
                    errorHandler?.Invoke(e.Message);      
//#if DEBUG
                    //if (_errors_ > 4) {
                    //    IsActive = false;
                    //}
//#endif
                }
            }
        }

        void IRequestCallback<GetLongPollServerResponse>.OnSuccess(GetLongPollServerResponse result) {
            if (result?.InnerResponse != null && !IsActive) {
                serverCredentials = result.InnerResponse;
            }
        }
        void IRequestCallback<GetLongPollServerResponse>.OnError(string reason) {
            errorHandler?.Invoke(reason);
        } 

        void IPollingErrorHandler.HandleError(LongPollErrorResponse error) {
            IsActive = false;
            Prepare();
        }  
    }
}