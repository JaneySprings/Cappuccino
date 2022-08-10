using Android.OS;
using Android.Views;
using Cappuccino.App.Android.Extensions;
using Cappuccino.App.Android.ViewBinding;
using Fragment = AndroidX.Fragment.App.Fragment;


namespace Cappuccino.App.Android.UI {
    public class ContactsFragment: Fragment {
        private FragmentContactsBinding? binding;
        private ContactsViewModel? viewModel;
        // private ConcatAdapter? concatAdapter;
        // private readonly UsersAdapter impAdapter = new UsersAdapter();
        private readonly UsersAdapter allAdapter = new UsersAdapter();


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
            this.binding = FragmentContactsBinding.Inflate(inflater);
            this.viewModel = this.ByViewModels<ContactsViewModel>();

            // this.impAdapter.OnItemClicked = OpenMessagesPage;
            // this.impAdapter.OnItemPhotoClicked = OpenProfilePage;

            this.allAdapter.OnItemClicked = OpenMessagesPage;
            this.allAdapter.OnItemPhotoClicked = OpenProfilePage;
            this.allAdapter.OnLastItemBind = this.viewModel.RequestFriends;

            // this.concatAdapter = new ConcatAdapter(
            //     this.impAdapter,
            //     new TextHeader(Resource.String.all, true),
            //     this.allAdapter
            // );

            return this.binding.Root!;
        }
        public override void OnViewCreated(View view, Bundle savedInstanceState) {
            base.OnViewCreated(view, savedInstanceState);

            new AppbarDecorator()
                .SetElement(this.binding!.appbar)
                .SetTitleText(GetString(Resource.String.contacts))
                .SetMainActionIcon(Resource.Drawable.ic_search)
                .DisableBackAction()
                .Apply();

            this.viewModel!.Users.Observe(ViewLifecycleOwner, this.allAdapter.Add);
            // this.viewModel.ImpUsers.Observe(ViewLifecycleOwner, this.impAdapter.Add);
            this.binding.recyclerView.SetAdapter(this.allAdapter);
            this.binding.appbar.mainAction.Click += (_, _) => {
                NavigationExtensions.NavController?.RouteOn(Resource.Id.action_contacts_to_search);
            };

            // if (this.impAdapter.ItemCount == 0)
            //     this.viewModel.RequestImportantFriends();

            if (this.allAdapter.ItemCount == 0)
                this.viewModel.RequestFriends(0);
        }
        public override void OnDestroyView() {
            base.OnDestroyView();
            this.binding = null;
        }

        private void OpenProfilePage(int id) {}
        private void OpenMessagesPage(int id) {}
    }
}