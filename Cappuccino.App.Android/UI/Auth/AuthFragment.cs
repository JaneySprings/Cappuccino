using Android.OS;
using Android.Views;
using Cappuccino.App.Android.Extensions;
using Cappuccino.App.Android.UI.Auth;
using Cappuccino.App.Android.Utils;
using Cappuccino.App.Android.ViewBinding;
using Cappuccino.Core.Network.Auth;
using Cappuccino.Core.Network.Config;
using Fragment = AndroidX.Fragment.App.Fragment;

namespace Cappuccino.App.Android.UI {

    public class AuthFragment: Fragment {
        private FragmentAuthBinding? binding;
        private AppActivity? hostActivity;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
            this.binding = this.ByViewBinding(FragmentAuthBinding.Inflate);
            return this.binding.Root!;
        }
        public override void OnViewCreated(View view, Bundle savedInstanceState) {
            base.OnViewCreated(view, savedInstanceState);
            this.hostActivity = RequireActivity() as AppActivity;
            AuthManager authManager = new AuthManager();
            AuthWebClient webClient = new AuthWebClient();

            new AppbarDecorator()
                .SetElement(this.binding!.appbar)
                .SetTitleText(GetString(Resource.String.sign_in))
                .Apply();

            webClient.Redirected = authManager.Login;
            authManager.Authorized += (_, _) => OnAuthorized();

            this.binding.appbar.backAction.Click += (_, _) => this.hostActivity?.Finish();
            this.binding.webView.SetWebViewClient(webClient);
            this.binding.webView.LoadUrl(authManager.BuildAuthorizationUri());
        }
        public override void OnDestroyView() {
            base.OnDestroyView();

            this.hostActivity = null;
            this.binding = null;
        }


        private void OnAuthorized() {
            KeyValueStorage storage = new(RequireContext());
            CredentialsManager.SaveAccessToken(storage.SaveAccessToken);

            this.binding!.webView.Visibility = ViewStates.Gone;
            this.hostActivity?.PrepareNavigationHost();
        }
    }
}
