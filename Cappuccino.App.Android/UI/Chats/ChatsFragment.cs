using Android.OS;
using Android.Views;
using Cappuccino.App.Android.Extensions;
using Cappuccino.App.Android.ViewBinding;
using Cappuccino.Core.Network;
using Fragment = AndroidX.Fragment.App.Fragment;

namespace Cappuccino.App.Android.UI {
    public class ChatsFragment: Fragment {
        private FragmentChatsBinding? binding;
        private ChatViewModel? viewModel;
        private ChatAdapter chatAdapter = new ChatAdapter();


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
            this.binding = FragmentChatsBinding.Inflate(inflater);
            this.viewModel = this.ByViewModels<ChatViewModel>();
            return this.binding.Root!;
        }
        public override void OnViewCreated(View view, Bundle savedInstanceState) {
            base.OnViewCreated(view, savedInstanceState);

            new AppbarDecorator()
                .SetElement(this.binding!.appbar)
                .SetTitleText(GetString(Resource.String.chats))
                .SetMainActionIcon(Resource.Drawable.ic_search)
                .SetSubActionIcon(Resource.Drawable.ic_add)
                .DisableBackAction()
                .Apply();

            this.binding!.recyclerView.SetAdapter(this.chatAdapter);
            this.viewModel!.Chats.Observe(ViewLifecycleOwner, this.chatAdapter.Replace);

            if (this.chatAdapter.ItemCount == 0)
                this.viewModel.RequestConversations(0);

            LongPollExecutor.HistoryUpdated += (_, _) => this.viewModel!.RequestConversations(0);
        }
        public override void OnDestroyView() {
            base.OnDestroyView();
            this.binding = null;
        }
    }
}