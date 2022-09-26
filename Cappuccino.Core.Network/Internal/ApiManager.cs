using Cappuccino.Core.Network.Auth;
using Cappuccino.Core.Network.Config;
using Cappuccino.Core.Network.Handlers;

namespace Cappuccino.Core.Network.Internal {

    internal static class ApiManager {
        public static ApiConfiguration? ApiConfig { get; private set; }
        public static AccessToken? AccessToken {
            get => ApiConfig?.TokenStorageHandler?.OnTokenRequested();
            private set => ApiConfig?.TokenStorageHandler?.OnTokenReceived(value!);
        }

        internal static void UpdateApiConfiguration(ApiConfiguration config) {
            ApiConfig = config;
        }

        internal static void UpdateAccessToken(AccessToken token) {
            AccessToken = token;
        }
    }
}
