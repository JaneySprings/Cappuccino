using Cappuccino.App.iOS.UI.Common;

namespace Cappuccino.App.iOS.UI.Chats;


public partial class ChatsAdapterDelegate : TableViewAdapterBase<ChatItem, ChatViewCell> {
    private readonly IComparer<ChatItem> comparer = new ChatItemComparer();

    public void InsertItems(IEnumerable<ChatItem> items) {
        foreach(var item in items) {
            var finded = Items.Find(it => it.Id == item.Id);

            if (finded == null) {
                Items.Add(item);
                continue;
            }

            Items.Remove(finded);
            Items.Add(item);
        }

        Items.Sort(comparer);
    }

    public void RemoveItemsById(IEnumerable<int> ids) {
        foreach(var id in ids) {
            var finded = Items.Find(it => it.Id == id);

            if (finded != null)
                Items.Remove(finded);
        }
    }
}