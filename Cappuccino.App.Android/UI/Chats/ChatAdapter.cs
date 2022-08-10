using System;
using Android.Views;
using AndroidX.RecyclerView.Widget;
using Cappuccino.App.Android.Extensions;
using Cappuccino.App.Android.UI.Common;
using Cappuccino.App.Android.Utils;
using Cappuccino.App.Android.ViewBinding;

namespace Cappuccino.App.Android.UI {
    public class ChatAdapter: AdapterBase<ChatItem> {

        public ChatAdapter() { this.DiffUtilCallback = new ChatsDiffCallback(this); }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
            return new ChatHolder(ItemChatBinding.Inflate(LayoutInflater.From(parent.Context)!, parent));
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
            if (holder is ChatHolder viewHolder)
                viewHolder.Bind(Items[position]);

            if (position == ItemCount-1)
                this.OnLastItemBind?.Invoke(ItemCount);
        }

        private class ChatHolder: RecyclerView.ViewHolder {
            private readonly ItemChatBinding? binding;
            public ChatHolder(ItemChatBinding binding) : base(binding.Root) { this.binding = binding; }

            public void Bind(ChatItem item) {
                this.binding!.name.Text = item.Title;
                this.binding.date.Text = " • " + DateTimeUtils.ParseShortDate(item.Date);
                this.binding.unread.Text = item.UnreadCount.ToString();
                this.binding.unread.Visibility = item.UnreadCount != 0 ? ViewStates.Visible : ViewStates.Gone;
                this.binding.isRead.Visibility = !item.IsRead ? ViewStates.Visible : ViewStates.Gone;
                this.binding.online.Visibility = item.IsOnline ? ViewStates.Visible : ViewStates.Gone;
                this.binding.message.Text = item.Message;

                if (item.IsOut) {
                    this.binding.sender.Text = $"{ItemView.Context?.GetString(Resource.String.you)}: ";
                } else if (item.Sender != String.Empty) {
                    this.binding.sender.Text = $"{item.Sender}: ";
                } else this.binding.sender.Text = String.Empty;

                if (item.Photo200 != String.Empty) this.binding.photo.Load(item.Photo200);
                else this.binding.photo.Load(PaintUtils.CreateCover(item.Title));
            }
        }
    }
}