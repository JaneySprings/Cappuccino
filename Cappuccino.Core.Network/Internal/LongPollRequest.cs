using Cappuccino.Core.Network.Models;
using Cappuccino.Core.Network.Utils;
using Cappuccino.Core.Network.Config;
using Cappuccino.Core.Network.Models.Messages;
using System.Threading.Tasks;
using System.Text.Json;
using System;

namespace Cappuccino.Core.Network.Internal {

    internal class LongPollRequest : ApiRequest<LongPollResponse> {
        private readonly GetLongPollServerResponse.Response credentials;

        public LongPollRequest(GetLongPollServerResponse.Response credentials) : base("") {
            this.credentials = credentials;
            
            ClearParams();
            AddParam("act", "a_check");
            AddParam("key", credentials!.Key);
            AddParam("ts", credentials!.Ts);
            AddParam("pts", credentials!.Pts);
            AddParam("wait", 25);
            AddParam("mode", 2);
            AddParam("version", CredentialsManager.ApiConfig?.LpVersion);
        }

        public override async Task<LongPollResponse> Execute() {
            if (!CredentialsManager.IsInternalTokenValid()) {
                throw new Exception("Access token incorrect");
            }

            using Executor executor = new Executor();
            string request = $"https://{credentials?.Server}?{this.Args.JoinToUrl()}";
            string? response = await executor.GetAsync(request);

            return OnServerResponseReceived(response!);
        }

        protected override LongPollResponse OnServerResponseReceived(string response) {
            LongPollErrorResponse error = JsonSerializer.Deserialize<LongPollErrorResponse>(response)!;

            if (error.Error == null)
                return OnResponseSuccess(response);

            throw new Exception($"Api {error.Failed}: {error.Error}");
        }
    }
}