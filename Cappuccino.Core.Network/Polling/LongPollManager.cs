using System;
using Cappuccino.Core.Network.Models.Messages;


namespace Cappuccino.Core.Network.Polling {

    public class LongPollManager {
        private readonly PollingLooper looper = new PollingLooper();
        public Action<Models.LongPollResponse>? MessageReceived { 
            get => looper.UpdateHandler; 
            set => looper.UpdateHandler = value;
        }
        public Action<Exception>? ErrorReceived { 
            get => looper.ErrorHandler; 
            set => looper.ErrorHandler = value;
        }

        private static LongPollManager? instance;
        public static LongPollManager Instance { 
            get => instance ??= new LongPollManager();
        }

        public GetLongPollServerResponse.Response? Credentials => looper.ServerCredentials;
        public bool IsActive => looper.IsActive;

//#if DEBUG
        public Action? CallHandler { 
            get => looper.callHandler; 
            set => looper.callHandler = value;
        }
//endif

        public void StartExecution() => looper.Prepare();
        public void StopExecution() => looper.Interrupt();
    }
}