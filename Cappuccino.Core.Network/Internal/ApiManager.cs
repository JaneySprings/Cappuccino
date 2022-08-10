using Cappuccino.Core.Network.Auth;
using Cappuccino.Core.Network.Config;
using Cappuccino.Core.Network.Handlers;

namespace Cappuccino.Core.Network.Internal {

    internal static class ApiManager {
        public static ApiConfiguration? ApiConfig { get; private set; }
        public static AccessToken? AccessToken { get; private set; }
        public static ITokenStorageHandler? TokenStorageHandler { get; private set; }

        internal static void UpdateApiConfiguration(ApiConfiguration config) {
            ApiConfig = config;
        }

        internal static void UpdateAccessToken(AccessToken token) {
            AccessToken = token;
        }
        
        internal static void UpdateStorageHandler(ITokenStorageHandler handler) {
            TokenStorageHandler = handler;
        }
    }
}
