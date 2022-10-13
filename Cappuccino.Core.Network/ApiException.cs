using System;
using Cappuccino.Core.Network.Models;

namespace Cappuccino.Core.Network {

    [Serializable]
    public class ApiException : Exception {
        public int ErrorCode { get; }
        public string? ErrorMessage { get; }


        public ApiException(string message, Exception innerException) : base(message, innerException) {}
        public ApiException(string message) : base(message) {}
        public ApiException() {}
        public ApiException(ErrorResponse.Error error) { 
            ErrorCode = error.ErrorCode;
            ErrorMessage = error.ErrorMsg;
        }
    }
}