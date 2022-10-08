using Cappuccino.App.iOS.UI.Common;

namespace Cappuccino.App.iOS.UI.Messages;


public partial class MessageViewCell : TableViewCellBase<MessageItem> {
    
    public override void Bind(MessageItem item) {
        this.message!.Text = item.InnerResponse.Text;
    }
}