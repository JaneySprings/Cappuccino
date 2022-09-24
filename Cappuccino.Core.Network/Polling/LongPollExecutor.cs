using System;
using System.Text.Json;
using System.Threading.Tasks;
using Cappuccino.Core.Network.Handlers;
using Cappuccino.Core.Network.Internal;
using Cappuccino.Core.Network.Models;

namespace Cappuccino.Core.Network.Polling {
    public static class LongPollExecutor {
        private static Models.Messages.GetLongPollServerResponse? _serverInfo;
        public static EventHandler? HistoryUpdated;
        public static Action<string>? RequestError;

        public static bool IsActive { get; private set; }


        public static void StartExecution() {
            Api.Get(new Methods.Messages.GetLongPollServer(1), new ApiCallback<Models.Messages.GetLongPollServerResponse>()
                .OnSuccess(result => {
                    _serverInfo = result;
                    ResumeExecution();
                })
                .OnError(reason => RequestError?.Invoke(reason))
            );
        }
        public static void StopExecution() {
            IsActive = false;
        }
        public static void ResumeExecution() {
            IsActive = true;
            StartGlobalLoop();
        }


        private static async void StartGlobalLoop() {
            while (IsActive) {
                try {
                    LongPollResponse response = await Connect();
                    _serverInfo!.InnerResponse!.Ts = response.Ts;

                    if (response.Updates?.Count != 0 && IsActive)
                        HistoryUpdated?.Invoke(response, EventArgs.Empty);

                } catch (Exception e) { RequestError?.Invoke(e.Message); }
            }
        }
        private static async Task<LongPollResponse> Connect() {
            using Executor executor = new Executor();
            string? response = await executor.GetAsync(BuildRequestUri());

            ErrorResponse error = JsonSerializer.Deserialize<ErrorResponse>(response!)!;

            if (error.InnerError == null)
                return JsonSerializer.Deserialize<LongPollResponse>(response!)!;

            TokenExpiredHandler.ValidateError(error);
            throw new Exception($"Api {error.InnerError!.ErrorCode}: {error.InnerError!.ErrorMsg}");
        }
        private static string BuildRequestUri() {
            return $"https://{_serverInfo?.InnerResponse?.Server}?act=a_check&key={_serverInfo?.InnerResponse?.Key}&ts={_serverInfo?.InnerResponse?.Ts}&wait=25&mode=2&version={ApiManager.ApiConfig?.LpVersion}";
        }
    }
}