using System;
using System.Threading.Tasks;


namespace Cappuccino.Core.Network.Polling {

    public class LongPollManager {
        private PollingLooper looper = new PollingLooper();
        public Action<Models.LongPollResponse>? HistoryUpdated { 
            get => looper.updateHandler; 
            set => looper.updateHandler = value;
        }
        public Action<string>? ErrorReceived { 
            get => looper.errorHandler; 
            set => looper.errorHandler = value;
        }

        private static LongPollManager? instance;
        public static LongPollManager Instance { 
            get {
                if (instance == null) {
                    instance = new LongPollManager();
                }
                return instance;
            }
        }
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