using Cappuccino.Core.Network.Models.Users;
using Cappuccino.Core.Network.Models.Groups;
using Cappuccino.Core.Network.Models.Messages;
using Cappuccino.Core.Network.Methods.Messages;

namespace Cappuccino.App.iOS.UI.Messages;


public static class MessageItemMapper {
    public static List<MessageItem> ToMessageItems(this GetHistory.Response.InnerResponse response) {
        return response.Items!.ConvertAll(it => it.ToMessageItem(response.Profiles, response.Groups));
    }
    public static List<MessageItem> ToMessageItems(this GetLongPollHistory.Response.InnerResponse response) {
        return response.Messages!.Items!.ConvertAll(it => it.ToMessageItem(response.Profiles, response.Groups));
    }
    public static MessageItem ToMessageItem(this Message response, List<User>? profiles, List<Group>? groups) {
        return new MessageItem(response, profiles, groups);
    }
}

public class MessageItemComparer : IComparer<MessageItem> {
    public int Compare(MessageItem? x, MessageItem? y) {
        return x!.InnerResponse.Date.CompareTo(y!.InnerResponse.Date);
    }
}

public class MessageItem {
    public const string TypeUser = "user";
    public const string TypeGroup = "group";

    public int MessageId { get; }
    public Message InnerResponse { get; }
    public object? RelativeItem { get; }

    public MessageItem(Message message, List<User>? profiles, List<Group>? groups) {
        this.MessageId = message.Id;
        this.InnerResponse = message;

        if (message?.FromId < 0) {
            this.RelativeItem = groups?.Find(it => it.Id == -message?.FromId);
        } else {
            this.RelativeItem = profiles?.Find(it => it.Id == message?.FromId);
        }
    }
}