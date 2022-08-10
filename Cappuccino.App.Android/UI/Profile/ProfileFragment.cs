using Android.OS;
using Android.Views;
using Cappuccino.App.Android.ViewBinding;
using Cappuccino.App.Android.Extensions;
using Cappuccino.Core.Network.Models;
using Cappuccino.Core.Network.Models.Response;
using Fragment = AndroidX.Fragment.App.Fragment;

namespace Cappuccino.App.Android.UI {

    public class ProfileFragment: Fragment {
        private FragmentProfileBinding? binding;
        private ProfileViewModel? viewModel;


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
            this.binding = this.ByViewBinding(FragmentProfileBinding.Inflate);
            this.viewModel = this.ByViewModels<ProfileViewModel>();
            return this.binding.Root!;
        }
        public override void OnViewCreated(View view, Bundle savedInstanceState) {
            base.OnViewCreated(view, savedInstanceState);

            this.viewModel!.Users.Observe(ViewLifecycleOwner, BindInformation);
            this.viewModel.RequestCurrentUser();
        }
        public override void OnDestroyView() {
            base.OnDestroyView();
            this.binding = null;
        }


        private void BindInformation(UsersGetResponse response) {
            User user = response.Response![0];

            this.binding!.name.Text = $"{user.FirstName} {user.LastName}";
            this.binding.caption.Text = $"@{user.ScreenName}";
            this.binding.photo.Load(user.Photo200);
        }
    }

}