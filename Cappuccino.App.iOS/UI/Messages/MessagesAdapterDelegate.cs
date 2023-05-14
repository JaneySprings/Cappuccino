using Cappuccino.App.iOS.UI.Common;

namespace Cappuccino.App.iOS.UI.Messages;


public partial class MessagesAdapterDelegate : TableViewAdapterBase<MessageItem, MessageViewCell> {
    private readonly IComparer<MessageItem> comparer = new MessageItemComparer();

    public bool AddItem(MessageItem item) {
        if (Items.Find(it => it.MessageId == item.MessageId) != null) 
            return false;

        Items.Add(item);
        return true;
    }

    public int UpdateItem(MessageItem item) {
        var finded = Items.Find(it => it.MessageId == item.MessageId);
        if (finded != null) {
            var index = Items.IndexOf(finded);
            Items[index] = item;
            return index;
        }

        return -1;
    }

    public int RemoveItemById(int id) {
        var finded = Items.Find(it => it.MessageId == id);
        if (finded != null) {
            var index = Items.IndexOf(finded);
            Items.Remove(finded);
            return index;
        }

        return -1;
    }

    public void Reorder() {
        Items.Sort(comparer);
    }
}