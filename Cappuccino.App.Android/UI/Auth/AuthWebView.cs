using System;
using Android.Webkit;

namespace Cappuccino.App.Android.UI.Auth {
    public class AuthWebClient : WebViewClient {
        public Action<string>? Redirected;

        public override bool ShouldOverrideUrlLoading(WebView? view, IWebResourceRequest? request) {
            if (request?.Url != null)
                this.Redirected?.Invoke(request.Url.ToString() ?? "");

            return false;
        }
    }
}