using System;
using Cappuccino.Core.Network.Auth;
using Cappuccino.Core.Network.Handlers;
using Cappuccino.Core.Network.Internal;

namespace Cappuccino.Core.Network.Config {

    public class CredentialsManager {
        public int? CurrentUserId => ApiManager.AccessToken?.UserId;

        public static void ApplyConfiguration(ApiConfiguration config) {
            ApiManager.UpdateApiConfiguration(config);
        }

        public static void ApplyAccessToken(AccessToken? token, IValidationCallback? callback = null) {
            if (IsTokenValid(ApiManager.AccessToken, callback))
                ApiManager.UpdateAccessToken(token!);
        }

        public static bool IsInternalTokenValid(IValidationCallback? callback = null) {
            if (ApiManager.ApiConfig?.TokenStorageHandler == null) {
                callback?.OnValidationFail($"Implementation of {nameof(ITokenStorageHandler)} does not find");
                return false;
            }

            if (ApiManager.AccessToken != null)
                return IsTokenValid(ApiManager.AccessToken, callback);

            return false;
        }

        

        internal static bool IsTokenValid(AccessToken? token, IValidationCallback? callback = null) {
            if (token == null) {
                callback?.OnValidationFail("Instance of token is null");
                return false;
            }
            
            if (token.ExpiresIn < DateTimeOffset.Now.ToUnixTimeSeconds() && token.ExpiresIn != -1) {
                callback?.OnValidationFail("Token lifetime is expired. Re-sign required");
                return false;
            }

            if (token.Token.Equals(string.Empty)) {
                callback?.OnValidationFail("Access token is empty");
                return false;
            }

            return true;
        }
    }
}