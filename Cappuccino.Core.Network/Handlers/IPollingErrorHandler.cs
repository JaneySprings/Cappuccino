using Cappuccino.Core.Network.Models;

namespace Cappuccino.Core.Network.Handlers { 

    internal interface IPollingErrorHandler {
        void HandleError(LongPollErrorResponse error);
    }
}