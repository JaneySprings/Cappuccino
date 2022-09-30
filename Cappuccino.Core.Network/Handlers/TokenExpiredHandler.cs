using System;
using System.Collections.Generic;
using System.Linq;
using Cappuccino.Core.Network.Models;

namespace Cappuccino.Core.Network.Handlers {

    public static class TokenExpiredHandler {
        public static Action<ErrorResponse>? Expired;

        private static readonly IEnumerable<int> Errors = new[] {
            5, 7, 15, 17, 20, 1114, 1116, 3301, 3302, 3609
        };

        internal static void ValidateError(ErrorResponse error) {
            if (Errors.Contains(error.InnerError?.ErrorCode ?? 0))
                Expired?.Invoke(error);
        }
    }
}