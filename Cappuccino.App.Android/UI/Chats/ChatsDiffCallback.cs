using Cappuccino.App.Android.UI.Common;

namespace Cappuccino.App.Android.UI {
    public class ChatsDiffCallback: DiffUtilCallback<ChatItem> {
        public ChatsDiffCallback(AdapterBase<ChatItem> adapter) : base(adapter) { }

        public override bool PrimaryKeysSame(ChatItem newItem, ChatItem oldItem) {
            return newItem.Id == oldItem.Id;
        }

        public override bool ItemsSame(ChatItem newItem, ChatItem oldItem) {
            return newItem.Title == oldItem.Title &&
                   newItem.Photo100 == oldItem.Photo100 &&
                   newItem.Photo200 == oldItem.Photo200 &&
                   newItem.Message == oldItem.Message &&
                   newItem.Date == oldItem.Date &&
                   newItem.IsRead == oldItem.IsRead &&
                   newItem.UnreadCount == oldItem.UnreadCount &&
                   newItem.IsOnline == oldItem.IsOnline;
        }
    }
}