using Cappuccino.Core.Network.Models.Users;
using Cappuccino.Core.Network.Models.Groups;
using Cappuccino.Core.Network.Models.Messages;

namespace Cappuccino.App.iOS.UI.Messages {

    public static class MessageItemMapper {
        //public static List<MessageItem> ToMessageItems(this Models.Messages.Message response) {
        //    return response.Items!.Select(chat => new ChatItem(chat, response.Profiles, response.Groups)).ToList();
        //}
        public static MessageItem ToMessageItem(this Message response, List<User>? profiles, List<Group>? groups) {
            return new MessageItem(response, profiles, groups);
        }
    }

    public class MessageItem {
        public readonly Message InnerResponse;
        public readonly object? RelativeItem;

        public MessageItem(Message message, List<User>? profiles, List<Group>? groups) {
            this.InnerResponse = message;

            switch (message?.FromId < 0 ? "group" : "user") {
                case "user": this.RelativeItem = profiles?.Find(it => it.Id == message?.FromId); break;
                case "group": this.RelativeItem = groups?.Find(it => it.Id == -message?.FromId); break;
            }
        }
    }
}

