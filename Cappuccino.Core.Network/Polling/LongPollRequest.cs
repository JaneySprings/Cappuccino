using Cappuccino.Core.Network.Models;
using Cappuccino.Core.Network.Config;
using Cappuccino.Core.Network.Internal;
using Cappuccino.Core.Network.Methods.Messages;
using System.Threading.Tasks;

namespace Cappuccino.Core.Network.Polling {

    internal class LongPollRequest : ApiRequest<LongPollResponse> {
        private readonly GetLongPollServer.Response.InnerResponse credentials;

        public LongPollRequest(GetLongPollServer.Response.InnerResponse credentials) {
            this.credentials = credentials;
        }

        protected override async Task<LongPollResponse> OnExecute() {
            using Executor executor = new Executor();
            string request = $"https://{credentials?.Server}?" 
                + $"version={CredentialsManager.ApiConfig?.LpVersion}"
                + $"ts={credentials!.Ts}"
                + $"pts={credentials!.Pts}"
                + $"key={credentials!.Key}"
                + "act=a_check"
                + "wait=25"
                + "mode=2";
            string? response = await executor.GetAsync(request);

            return OnServerResponseReceived(response!);
        }
    }
}