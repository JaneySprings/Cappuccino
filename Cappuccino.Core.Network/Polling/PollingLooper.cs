using System;
using Cappuccino.Core.Network.Handlers;
using Cappuccino.Core.Network.Internal;
using Cappuccino.Core.Network.Models.Messages;
using Cappuccino.Core.Network.Methods.Messages;


namespace Cappuccino.Core.Network.Polling { 

    internal class PollingLooper : IRequestCallback<GetLongPollServerResponse> {
        private GetLongPollServerResponse.Response? _serverCredentials;
        public GetLongPollServerResponse.Response? ServerCredentials { 
            get => _serverCredentials;
            private set {
                _serverCredentials = value;
                IsActive = true;
                Loop();
            }
        }

        public bool IsActive { get; private set; }
        public Action<Models.LongPollResponse>? UpdateHandler { get; set; }
        public Action<Exception>? ErrorHandler { get; set; }

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
                    var request = new LongPollRequest(ServerCredentials!);
                    var result = await request.Execute();

                    _serverCredentials!.Ts = result.Ts;

                    if (result.Updates?.Count != 0 && IsActive)
                        UpdateHandler?.Invoke(result);
                    
                } catch (Exception e) {
                    ErrorHandler?.Invoke(e);   
                    IsActive = false;
                    Prepare();
                }
            }
        }

        void IRequestCallback<GetLongPollServerResponse>.OnSuccess(GetLongPollServerResponse result) {
            if (result?.InnerResponse != null && !IsActive) {
                ServerCredentials = result.InnerResponse;
            }
        }
        void IRequestCallback<GetLongPollServerResponse>.OnError(ApiException exception) {
            ErrorHandler?.Invoke(exception);
        } 
    }
}