using System;

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

        public int Pts { 
            get => looper.ServerCredentials?.Pts ?? 0;
            set => looper.ServerCredentials!.Pts = value;
        }
        public int Ts { 
            get => looper.ServerCredentials?.Ts ?? 0;
            set => looper.ServerCredentials!.Ts = value;
        }

        public bool IsActive => looper.IsActive;



        private static LongPollManager? instance;
        public static LongPollManager Instance { 
            get => instance ??= new LongPollManager();
        }

        public void StartExecution() => looper.Prepare();
        public void StopExecution() => looper.Interrupt();
    }
}