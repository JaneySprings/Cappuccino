using Android.App;
using Android.OS;
using Android.Views;
using AndroidX.AppCompat.App;
using AndroidX.Navigation;
using AndroidX.Navigation.UI;
using Cappuccino.App.Android.Extensions;
using Cappuccino.App.Android.ViewBinding;
using Cappuccino.Core.Network;
using Cappuccino.Core.Network.Config;
using Cappuccino.Core.Network.Handlers;
using Cappuccino.Core.Network.Methods;
using Cappuccino.Core.Network.Models.Response;

namespace Cappuccino.App.Android.UI {

    [Activity(Name = "com.springsoftware.cappuccino.AppActivity", MainLauncher = true)]
    public class AppActivity : AppCompatActivity {
        private ActivityAppBinding? binding;

        protected override void OnCreate(Bundle? savedInstanceState) {
            base.OnCreate(savedInstanceState);

            this.binding = ActivityAppBinding.Inflate(LayoutInflater);
            SetContentView(this.binding.Root);

            NavigationExtensions.SetNavController(Navigation.FindNavController(this, Resource.Id.navFragment));
            SetupNavigationComponents();

            TokenExpiredHandler.Expired += (_, _) => PrepareNavigationAuth();
            LongPollExecutor.HistoryUpdated += (_, _) => RequestBadgeCounters();
        }
        protected override void OnPause() {
            base.OnPause();
            //LongPollExecutor.StopExecution();
        }
        protected override void OnResume() {
            base.OnResume();
            //LongPollExecutor.ResumeExecution();
        }
        protected override void OnDestroy() {
            base.OnDestroy();
            this.binding = null;
            LongPollExecutor.StopExecution();
        }


        private void SetupNavigationComponents() {
            this.binding!.bottomNavigation.SetOnNavigationItemReselectedListener(null);
            NavigationUI.SetupWithNavController(this.binding.bottomNavigation, NavigationExtensions.NavController);

            if (CredentialsManager.IsInternalTokenValid()) {
                PrepareNavigationHost();
            } else PrepareNavigationAuth();
        }
        private void RequestBadgeCounters() {
            Api.Get(new Account.GetCounters(), new ApiCallback<AccountGetCountersResponse>()
                .OnSuccess(result => {
                    this.binding!.bottomNavigation.GetOrCreateBadge(
                        this.binding!.bottomNavigation.Menu.GetItem(1)!.ItemId
                    ).SetVisible(result.Response?.Messages != 0);
                })
                .OnError(reason => { })
            );
        }

        public void PrepareNavigationAuth() {
            NavigationExtensions.NavController!.SetGraph(Resource.Navigation.navigation_auth);
            this.binding!.bottomNavigation.Visibility = ViewStates.Gone;
        }
        public void PrepareNavigationHost() {
            this.binding!.bottomNavigation.Visibility = ViewStates.Visible;
            NavigationExtensions.NavController!.SetGraph(Resource.Navigation.navigation_router);
            LongPollExecutor.StartExecution();
            RequestBadgeCounters();
        }
    }
}
