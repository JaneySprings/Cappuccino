using Cappuccino.App.iOS.Extensions;
using Cappuccino.App.iOS.UI.Common;
using Cappuccino.Core.Network.Models.Groups;
using Cappuccino.Core.Network.Models.Users;

namespace Cappuccino.App.iOS.UI.Chats;


public partial class ChatViewCell : TableViewCellBase<ChatItem> {
    public override void Bind(ChatItem item) {
        switch (item.InnerResponse.Conversation?.peer?.Type) {
            case "user":
                if (item.RelativeItem is User user) {
                    title!.Text = $"{user.FirstName} {user.LastName}";
                    online!.Hidden = user.Online != 1;
                    photo!.Load(user.Photo100);
                }
                break;
            case "group":
                if (item.RelativeItem is Group group) {
                    title!.Text = group.Name;
                    online!.Hidden = true;
                    photo!.Load(group.Photo100);
                }
                break;
            case "chat":
                title!.Text = item.InnerResponse.Conversation?.chatSettings?.Title;
                photo!.Load(item.InnerResponse.Conversation?.chatSettings?.Photo?.Photo100);
                online!.Hidden = true;
                break;
        }

        var sender = "";
        var trailing = "";

        if (item.InnerResponse.LastMessage?.Out == 1) {
            sender = Localization.Instance.GetString("common_abbr_you");
        } else if (item.InnerResponse.Conversation?.peer?.Type == "chat") {
            if (item.InnerResponse.LastMessage?.FromId > 0) {
                if (item.RelativeItemFromMessage is User user)
                    sender = user.FirstName;
            } else {
                if (item.RelativeItemFromMessage is Group group)
                    sender = group.Name;
            }
        }

        if (item.InnerResponse.LastMessage?.FwdMessages?.Count != 0)
            trailing = $"[{Localization.Instance.GetString("common_abbr_messages")}]";
        if (item.InnerResponse.LastMessage?.Attachments?.Count != 0)
            trailing = $"[{Localization.Instance.GetString("common_abbr_attachments")}]";

        message!.Text = sender 
            + (string.IsNullOrEmpty(sender) ? "" : ": ") 
            + item.InnerResponse.LastMessage?.Text?.Replace("\n", " ")
            + (string.IsNullOrEmpty(item.InnerResponse.LastMessage?.Text) ? "" : " ") 
            + trailing;

        unread!.Text = item.InnerResponse.Conversation?.UnreadCount.ToString();
        unread.Hidden = item.InnerResponse.Conversation?.UnreadCount == 0;
        read!.Hidden = item.InnerResponse.LastMessage?.Id == item.InnerResponse.Conversation?.OutRead;
    }
}