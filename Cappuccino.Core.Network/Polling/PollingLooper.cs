using System;
using System.Threading;
using Cappuccino.Core.Network.Handlers;
using Cappuccino.Core.Network.Methods.Messages;


namespace Cappuccino.Core.Network.Polling { 

    internal class PollingLooper : IRequestCallback<GetLongPollServer.Response> {
        private GetLongPollServer.Response.InnerResponse? _serverCredentials;
        public GetLongPollServer.Response.InnerResponse? ServerCredentials { 
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


        public void Prepare() {
            if (_serverCredentials == null)
                Api.Get(new GetLongPollServer { NeedPts = 1 }, this);
        }
        public void Interrupt() {
            IsActive = false;
        }


        private async void Loop() {
            while (IsActive) {
                Models.LongPollResponse? result = null;

                try {
                    var request = new LongPollRequest(ServerCredentials!);
                    result = await request.ExecuteAsync(CancellationToken.None);

                    _serverCredentials!.Ts = result.Ts;
                } catch (Exception e) {
                    ErrorHandler?.Invoke(e);   
                    IsActive = false;
                    Prepare();
                }

                if (result!.Updates?.Count != 0 && IsActive)
                    UpdateHandler?.Invoke(result);
            }
        }

        void IRequestCallback<GetLongPollServer.Response>.OnSuccess(GetLongPollServer.Response result) {
            if (result?.Inner != null && !IsActive) {
                ServerCredentials = result.Inner;
            }
        }
        void IRequestCallback<GetLongPollServer.Response>.OnError(ApiException exception) {
            ErrorHandler?.Invoke(exception);
        } 
    }
}