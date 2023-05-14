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
        return y!.LastMessage!.Date.CompareTo(x!.LastMessage!.Date);
    }
}

public class ChatItem {
    public const string TypeUser = "user";
    public const string TypeGroup = "group";
    public const string TypeChat = "chat";

    public int Id { get; set; }
    public int UnreadCount { get; set; }
    public string? Title { get; set; }
    public string? Photo { get; set; }
    public string? ChatType { get; set; }
    public string? LastMessageSender { get; set; }
    public bool IsOnline { get; set; }
    public bool IsRead { get; set; }
    public Message? LastMessage { get; set; }

    public ChatItem() { }
    public ChatItem(Chat chat, List<User>? profiles, List<Group>? groups) {
        IsRead = chat.LastMessage?.Id == chat.Conversation?.OutRead;
        ChatType = chat.Conversation?.peer?.Type ?? string.Empty;
        UnreadCount = chat.Conversation?.UnreadCount ?? 0;
        Id = chat.Conversation?.peer?.Id ?? 0;
        LastMessage = chat.LastMessage;

        if (chat.LastMessage?.FromId > 0) {
            var user = profiles?.Find(it => it.Id == chat.LastMessage?.FromId);
            LastMessageSender = $"{user?.FirstName} {user?.LastName![0]}"; //: John D
        } else {
            var group = groups?.Find(it => it.Id == -chat.LastMessage?.FromId);
            LastMessageSender = group?.Name ?? string.Empty;
        }

        switch (ChatType) {
            case ChatItem.TypeUser:
                var user = profiles?.Find(it => it.Id == Id);
                Title = $"{user?.FirstName} {user?.LastName}";
                Photo = user?.Photo100 ?? string.Empty;
                IsOnline = user?.Online == 1;
                break;
            case ChatItem.TypeGroup:
                var group = groups?.Find(it => it.Id == -Id);
                Title = group?.Name ?? string.Empty;
                Photo = group?.Photo100 ?? string.Empty;
                IsOnline = false;
                break;
            case ChatItem.TypeChat:
                Title = chat.Conversation?.chatSettings?.Title ?? string.Empty;
                Photo = chat.Conversation?.chatSettings?.Photo?.Photo100 ?? string.Empty;
                IsOnline = false;
                break;
        }
    }
}