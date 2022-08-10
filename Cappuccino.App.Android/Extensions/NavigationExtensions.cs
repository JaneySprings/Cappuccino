using Android.OS;
using AndroidX.Navigation;

namespace Cappuccino.App.Android.Extensions {
    public static class NavigationExtensions {
        public static NavController? NavController;

        public static void SetNavController(NavController controller) {
            NavController = controller;
        }

        public static void RouteOn(this NavController controller, int action) {
            NavOptions options = new NavOptions.Builder()
                 .SetEnterAnim(Resource.Animator.slide_toright)
                 .SetExitAnim(Resource.Animator.slide_outleft)
                 .Build();
            controller.Navigate(action, Bundle.Empty, options);
        }
    }
}