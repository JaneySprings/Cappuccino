using System;
using System.Linq;
using System.Collections.Generic;
using Cappuccino.Core.Network.Config;
using Cappuccino.Core.Network.Handlers;
using Cappuccino.Core.Network.Internal;
using Cappuccino.Core.Network.Utils;

namespace Cappuccino.Core.Network.Auth {

    public class AuthManager {
        public EventHandler? Authorized { get; set; }


        public string BuildAuthorizationUri() {
            if (ApiManager.ApiConfig?.ApplicationId == null)
                throw new Exception("Application Id does not exist in configuration");

            string[] uriParams = {
                "client_id=" + ApiManager.ApiConfig.ApplicationId,
                "redirect_uri=" + EndPoints.RedirectUri,
                "scope=" + ApiManager.ApiConfig.Permissions.Sum(),
                "response_type=token",
                "display=mobile",
                "revoke=1"
            };

            return $"{EndPoints.AuthorizeUri}?{String.Join("&", uriParams)}";
        }

        public void TryAuthorizeFromUri(string uri, IValidationCallback? callback = null) {
            if (!uri.Contains(EndPoints.RedirectUri))
                return;

            Dictionary<string, string> keys = uri.SplitUrl();

            if (!keys.ContainsKey("access_token"))
                return;

            AccessToken token = new AccessToken(
                keys["access_token"],
                Int64.Parse(keys["expires_in"]) + DateTimeOffset.Now.ToUnixTimeSeconds(),
                Int32.Parse(keys["user_id"])
            );

            if (!CredentialsManager.IsTokenValid(token, callback))
                return;

            ApiManager.UpdateAccessToken(token);
        
            this.Authorized?.Invoke(this, EventArgs.Empty);
        }
    }
}