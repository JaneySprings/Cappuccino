using System.Text.Json.Serialization;
namespace Cappuccino.Core.Network.Models {

    public class ErrorResponse {
        [JsonPropertyName("error")] public Error? InnerError { get; set; }

        public class Error {
            [JsonPropertyName("error_code")] public int ErrorCode { get; set; }
            [JsonPropertyName("error_msg")] public string? ErrorMsg { get; set; }
        }
    }

    public class LongPollErrorResponse {
        [JsonPropertyName("failed")] public int Failed { get; set; }
        [JsonPropertyName("error")] public string? Error { get; set; }
    }
}