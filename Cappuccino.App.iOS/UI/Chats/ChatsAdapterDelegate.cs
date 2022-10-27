using Cappuccino.App.iOS.UI.Common;

namespace Cappuccino.App.iOS.UI.Chats;


public partial class ChatsAdapterDelegate : TableViewAdapterBase<ChatItem, ChatViewCell> {
    private readonly IComparer<ChatItem> comparer = new ChatItemComparer();

    public void InsertItems(IEnumerable<ChatItem> items) {
        foreach(var item in items) {
            var finded = this.items.Find(it => it.ChatId == item.ChatId);

            if (finded == null) {
                base.AddItem(item);
                continue;
            }

            base.RemoveItem(finded);
            base.AddItem(item);
        }

        this.items.Sort(comparer);
    }

    public void RemoveItemsById(IEnumerable<int> ids) {
        foreach(var id in ids) {
            var finded = this.items.Find(it => it.ChatId == id);

            if (finded != null)
                base.RemoveItem(finded);
        }
    }
}