using Cappuccino.App.iOS.Extensions;
using Cappuccino.App.iOS.UI.Common;
using Cappuccino.Core.Network.Models.Groups;
using Cappuccino.Core.Network.Models.Users;
using ChatSettings = Cappuccino.Core.Network.Models.Messages.Conversation.ChatSettings;

namespace Cappuccino.App.iOS.UI.Chats;


public partial class ChatViewCell : TableViewCellBase<ChatItem> {
    public override void Bind(ChatItem item) {
        switch (item.ChatType) {
            case ChatItem.TypeUser: BindTypeUser(item.RelativeItem); break;
            case ChatItem.TypeGroup: BindTypeGroup(item.RelativeItem); break;
            case ChatItem.TypeChat: BindTypeChat(item.InnerResponse.Conversation?.chatSettings); break;
        }

        var sender = item.ChatName;
        if (!item.ChatType.Equals(ChatItem.TypeChat))
            sender = string.Empty;
        if (item.InnerResponse.LastMessage?.Out == 1)
            sender = Localization.Instance.GetString("common_abbr_you");

        var trailing = "";
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


    private void BindTypeUser(object? relativeItem) {
        if (relativeItem is User user) {
            title!.Text = $"{user.FirstName} {user.LastName}";
            online!.Hidden = user.Online != 1;
            photo!.Load(user.Photo100);
        }
    }
    private void BindTypeGroup(object? relativeItem) {
        if (relativeItem is Group group) {
            title!.Text = group.Name;
            online!.Hidden = true;
            photo!.Load(group.Photo100);
        }
    }
    private void BindTypeChat(ChatSettings? settings) {
        title!.Text = settings?.Title;
        photo!.Load(settings?.Photo?.Photo100);
        online!.Hidden = true;
    }
}