using System;
using Android.OS;
using Android.Views;
using Cappuccino.App.Android.Extensions;
using Cappuccino.App.Android.ViewBinding;
using Fragment = AndroidX.Fragment.App.Fragment;

namespace Cappuccino.App.Android.UI {
    public class ContactsSearchFragment: Fragment {
        private FragmentSearchBinding? binding;
        private ContactsViewModel? viewModel;
        private readonly UsersAdapter userAdapter = new UsersAdapter();


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
            this.binding = FragmentSearchBinding.Inflate(inflater);
            this.viewModel = this.ByViewModels<ContactsViewModel>();

            this.userAdapter.OnItemClicked = OpenMessagesPage;
            this.userAdapter.OnItemPhotoClicked = OpenProfilePage;

            return this.binding.Root!;
        }
        public override void OnViewCreated(View view, Bundle savedInstanceState) {
            base.OnViewCreated(view, savedInstanceState);

            this.viewModel!.Search.Observe(ViewLifecycleOwner, this.userAdapter.Replace);
            this.binding!.recyclerView.SetAdapter(this.userAdapter);
            this.binding.search.TextChanged += (sender, args) => {
                if (args.Text!.ToString() != String.Empty)
                    this.viewModel.RequestSearch(args.Text!.ToString());
            };
            this.binding.search.RequestFocus();
            this.binding.backAction.Click += (_, _) => NavigationExtensions.NavController!.PopBackStack();

            // if (this.userAdapter.ItemCount == 0)
            //     this.viewModel.RequestSearch(".");
        }
        public override void OnDestroyView() {
            base.OnDestroyView();
            this.binding = null;
        }

        private void OpenProfilePage(int id) {}
        private void OpenMessagesPage(int id) {}
    }
}