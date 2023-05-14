using Cappuccino.App.iOS.Extensions;
using Cappuccino.App.iOS.UI.Common;
using Cappuccino.Core.Network.Models.Groups;
using Cappuccino.Core.Network.Models.Users;
using ChatSettings = Cappuccino.Core.Network.Models.Messages.Conversation.ChatSettings;

namespace Cappuccino.App.iOS.UI.Chats;


public partial class ChatViewCell : TableViewCellBase<ChatItem> {
    public override void Bind(ChatItem item) {
        var sender = item.LastMessageSender;
        if (item.ChatType?.Equals(ChatItem.TypeChat) == false)
            sender = string.Empty;
        if (item.LastMessage?.Out == 1)
            sender = Localization.Instance.GetString("common_abbr_you");

        var trailing = "";
        if (item.LastMessage?.FwdMessages?.Count != 0)
            trailing = $"[{Localization.Instance.GetString("common_abbr_messages")}]";
        if (item.LastMessage?.Attachments?.Count != 0)
            trailing = $"[{Localization.Instance.GetString("common_abbr_attachments")}]";

        message!.Text = sender
            + (string.IsNullOrEmpty(sender) ? "" : ": ")
            + item.LastMessage?.Text?.Replace("\n", " ")
            + (string.IsNullOrEmpty(item.LastMessage?.Text) ? "" : " ")
            + trailing;

        unread!.Text = item.UnreadCount.ToString();
        unread.Hidden = item.UnreadCount == 0;
        read!.Hidden = item.IsRead;
        title!.Text = item.Title;
        online!.Hidden = !item.IsOnline;
        photo!.Load(item.Photo);
    }
}