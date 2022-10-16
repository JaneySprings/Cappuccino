using Cappuccino.App.iOS.UI.Common;
using Cappuccino.App.iOS.Extensions;
using Cappuccino.Core.Network.Models.Groups;
using Cappuccino.Core.Network.Models.Users;

namespace Cappuccino.App.iOS.UI.Messages;


public partial class MessageViewCell : TableViewCellBase<MessageItem> {
    private const int chatIdOffset = 2000000000;
    
    public override void Bind(MessageItem item) {
        this.message!.Text = item.InnerResponse.Text;
        this.containerHorizontalAlignment = item.InnerResponse.Out;
        this.date!.Text = item.InnerResponse.Date.ParseShortDate();
        
        if (item.InnerResponse.Out == 1) {
            this.container!.BackgroundColor = Colors.MessageBoxOut;
            this.displayAdditinalInfo = false;
        } else {
            this.container!.BackgroundColor = Colors.MessageBoxIn;

            if (item.InnerResponse.PeerId > chatIdOffset) {
                this.displayAdditinalInfo = true;

                if (item.RelativeItem is User user) 
                    this.photo!.Load(user.Photo100);
                else if (item.RelativeItem is Group group) 
                    this.photo!.Load(group.Photo100);
            } else {
                this.displayAdditinalInfo = false;
            }
        }
    }
}