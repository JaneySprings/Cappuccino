using System;
using System.Threading.Tasks;
using Cappuccino.Core.Network.Models.Messages;


namespace Cappuccino.Core.Network.Polling {

    public class LongPollManager {
        private PollingLooper looper = new PollingLooper();
        public Action<Models.LongPollResponse>? MessageReceived { 
            get => looper.updateHandler; 
            set => looper.updateHandler = value;
        }
        public Action<Exception>? ErrorReceived { 
            get => looper.errorHandler; 
            set => looper.errorHandler = value;
        }
        public GetLongPollServerResponse.Response? Credentials => looper.ServerCredentials;


        private static LongPollManager? instance;
        public static LongPollManager Instance { 
            get {
                if (instance == null) {
                    instance = new LongPollManager();
                }
                return instance;
            }
        }
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