using System;
using System.Collections.Generic;
using Cappuccino.Core.Network.Config;
using Cappuccino.Core.Network.Handlers;
using Cappuccino.Core.Network.Internal;
using Cappuccino.Core.Network.Utils;

namespace Cappuccino.Core.Network.Auth {

    public class ImplicitAuthentificator {
        public EventHandler? Authorized { get; set; }


        public string BuildAuthorizationUri() {
            if (CredentialsManager.ApiConfig?.ApplicationId == null)
                throw new Exception("Application Id does not exist in configuration");

            string[] uriParams = {
                "client_id=" + CredentialsManager.ApiConfig.ApplicationId,
                "redirect_uri=" + EndPoints.RedirectUri,
                "scope=" + CredentialsManager.ApiConfig.Permissions,
                "response_type=token",
                "display=mobile",
                "revoke=1"
            };

            return $"{EndPoints.AuthorizeImplicitUri}?{String.Join("&", uriParams)}";
        }

        public void TryAuthorizeFromUri(string uri, IValidationCallback? callback = null) {
            if (!uri.Contains(EndPoints.RedirectUri))
                return;

            Dictionary<string, string> keys = uri.SplitUrl();

            if (!keys.ContainsKey("access_token"))
                return;

            AccessToken token = new AccessToken(
                keys["access_token"],
                Int64.Parse(keys["expires_in"]),
                Int32.Parse(keys["user_id"])
            );

            if (token.ExpiresIn > 0)
                token.ApplyCurrentTime();

            if (!CredentialsManager.ApplyAccessToken(token, callback))
                return;
                
            this.Authorized?.Invoke(this, EventArgs.Empty);
        }
    }
}