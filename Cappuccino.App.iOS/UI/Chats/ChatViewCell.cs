using System;
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
                    //online.Hidden = user.Online != 1;
                    photo!.Load(user.Photo100);
                }
                break;
            case "group":
                if (item.RelativeItem is Group group) {
                    title!.Text = group.Name;
                    //online.Hidden = true;
                    photo!.Load(group.Photo100);
                }
                break;
            case "chat":
                title!.Text = item.InnerResponse.Conversation?.chatSettings?.Title;
                photo!.Load(item.InnerResponse.Conversation?.chatSettings?.Photo?.Photo100);
                //online.Hidden = true;
                break;
        }

        //unread.Text = $"  {item.InnerResponse.Conversation?.UnreadCount}  ";
        //unread.Hidden = item.InnerResponse.Conversation?.UnreadCount == 0;
        //isRead.Hidden = item.InnerResponse.LastMessage?.Id == item.InnerResponse.Conversation?.OutRead;
        //date.Text = " • " + item.InnerResponse.LastMessage?.Date.ParseShortDate();
        //contentConstraint.Priority = (unread.Hidden && isRead.Hidden) ? 1000 : 1;

        var sender = "";
        if (item.InnerResponse.LastMessage?.Out == 1) {
            sender = "You: ";
        } else if (item.InnerResponse.Conversation?.peer?.Type == "chat") {
            if (item.InnerResponse.LastMessage?.FromId > 0) {
                if (item.RelativeItemFromMessage is User user)
                    sender = $"{user.FirstName}: ";
            } else {
                if (item.RelativeItemFromMessage is Group group)
                    sender = $"{group.Name}: ";
            }
        }

        message!.Text = sender + item.InnerResponse.LastMessage?.Text?.Replace("\n", " ");
    }
}