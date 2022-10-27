using Cappuccino.Core.Network.Models.Users;
using Cappuccino.Core.Network.Models.Groups;
using Cappuccino.Core.Network.Models.Messages;
using Cappuccino.Core.Network.Methods.Messages;

namespace Cappuccino.App.iOS.UI.Chats;


public static class ChatItemMapper {
    public static List<ChatItem> ToChatItems(this GetConversations.Response.InnerResponse response) {
        return response.Items!.ConvertAll(chat => new ChatItem(chat, response.Profiles, response.Groups));
    }
}

public class ChatItemComparer : IComparer<ChatItem> {
    public int Compare(ChatItem? x, ChatItem? y) {
        return y!.InnerResponse.LastMessage!.Date.CompareTo(x!.InnerResponse.LastMessage!.Date);
    }
}

public class ChatItem {
    public const string TypeUser = "user";
    public const string TypeGroup = "group";
    public const string TypeChat = "chat";

    public int ChatId { get; }
    public string ChatName { get; }
    public string ChatType { get; }
    public Chat InnerResponse { get; }
    public object? RelativeItem { get; }

    public ChatItem(Chat chat, List<User>? profiles, List<Group>? groups) {
        this.ChatId = chat.Conversation?.peer?.Id ?? 0;
        this.ChatType = chat.Conversation?.peer?.Type ?? string.Empty;
        this.InnerResponse = chat;

        if (this.ChatType.Equals(ChatItem.TypeUser)) {
            this.RelativeItem = profiles?.Find(it => it.Id == chat.Conversation?.peer?.Id);
        } else {
            this.RelativeItem = groups?.Find(it => it.Id == -chat.Conversation?.peer?.Id);
        }

        if (chat.LastMessage?.FromId > 0) {
            var user = profiles?.Find(it => it.Id == chat.LastMessage?.FromId);
            this.ChatName = $"{user?.FirstName} {user?.LastName![0]}"; //: John D
        } else {
            var group = groups?.Find(it => it.Id == -chat.LastMessage?.FromId);
            this.ChatName = group?.Name ?? string.Empty;
        }
    }
}