using System;
using System.Threading.Tasks;
using Cappuccino.Core.Network.Handlers;
using Cappuccino.Core.Network.Internal;
using Cappuccino.Core.Network.Models.Messages;
using Cappuccino.Core.Network.Methods.Messages;


namespace Cappuccino.Core.Network.Polling { 

    internal class PollingLooper : IRequestCallback<GetLongPollServerResponse> {
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
        public int _errors_ = 0;
//#endif


        public void Prepare() => Api.Get(new GetLongPollServer { NeedPts = 1 }, this);
        public void Interrupt() => IsActive = false;


        private async void Loop() {
            while (IsActive) {
                try {
                    var request = new LongPoolRequest(serverCredentials!);
                    var result = await request.Execute();

                    _serverCredentials!.Ts = result.Ts;

                    if (result.Updates?.Count != 0)
                        updateHandler?.Invoke(result);
                } catch (Exception e) {
                    errorHandler?.Invoke(e.Message);         
//#if DEBUG
                    _errors_++;
                    if (_errors_ > 4) {
                        IsActive = false;
                    }
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
    }
}