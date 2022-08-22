using System;
using Cappuccino.App.iOS.Extensions;
using Cappuccino.App.iOS.UI.Common;
using Cappuccino.Core.Network.Models.Users;
using Cappuccino.Core.Network.Models.Groups;
using Foundation;

namespace Cappuccino.App.iOS.UI.Chats {

    public partial class ChatsAdapterDelegate: TableViewAdapterBase<ChatItem, ChatViewCell> {}

    [Register("ChatViewCell")]
    public partial class ChatViewCell : TableViewCellBase<ChatItem> {
        public ChatViewCell(IntPtr handle) : base(handle) { }

        public override void Bind(ChatItem item) {
            //switch (item.InnerResponse.Conversation?.peer?.Type) {
            //    case "user":
            //        if (item.RelativeItem is User user) {
            //            name.Text = $"{user.FirstName} {user.LastName}";
            //            online.Hidden = user.Online != 1;
            //            photo.Load(user.Photo100);
            //        }
            //        break;
            //    case "group":
            //        if (item.RelativeItem is Group group) {
            //            name.Text = group.Name;
            //            online.Hidden = true;
            //            photo.Load(group.Photo100);
            //        }
            //        break;
            //    case "chat":
            //        name.Text = item.InnerResponse.Conversation?.chatSettings?.Title;
            //        photo.Load(item.InnerResponse.Conversation?.chatSettings?.Photo?.Photo100);
            //        online.Hidden = true;
            //        break;
            //}

            //message.Text = item.InnerResponse.LastMessage?.Text?.Replace("\n", " ");
            //unread.Text = $"  {item.InnerResponse.Conversation?.UnreadCount}  ";
            //unread.Hidden = item.InnerResponse.Conversation?.UnreadCount == 0;
            //isRead.Hidden = item.InnerResponse.LastMessage?.Id == item.InnerResponse.Conversation?.OutRead;
            //date.Text = " • " + item.InnerResponse.LastMessage?.Date.ParseShortDate();
            //contentConstraint.Priority = (unread.Hidden && isRead.Hidden) ? 1000 : 1;

            //if (item.InnerResponse.LastMessage?.Out == 1) {
            //    sender.Text = "You: ";
            //} else if (item.InnerResponse.Conversation?.peer?.Type == "chat") {
            //    if (item.InnerResponse.LastMessage?.FromId > 0) {
            //        if (item.RelativeItemFromMessage is User user)
            //            sender.Text = $"{user.FirstName}: ";
            //    } else {
            //        if (item.RelativeItemFromMessage is Group group)
            //            sender.Text = $"{group.Name}: ";
            //    }
            //} else {
            //    sender.Text = String.Empty;
            //}
        }
    }
}

