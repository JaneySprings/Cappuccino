using System.Collections.Generic;
using System.Linq;
using Cappuccino.Core.Network.Models.Users;
using Cappuccino.Core.Network.Models.Groups;
using Cappuccino.Core.Network.Models.Messages;
using Models = Cappuccino.Core.Network.Models;

namespace Cappuccino.App.iOS.UI.Chats {

    public static class ChatItemMapper {
        public static List<ChatItem> ToChatItems(this Models.Messages.GetConversationsResponse.Response response) {
            return response.Items!.Select(chat => new ChatItem(chat, response.Profiles, response.Groups)).ToList();
        }
    }

    public class ChatItem {
        public readonly Chat InnerResponse;
        public readonly object? RelativeItem;
        public readonly object? RelativeItemFromMessage;

        public ChatItem(Chat chat, List<User>? profiles, List<Group>? groups) {
            this.InnerResponse = chat;

            switch (chat.Conversation?.peer?.Type) {
                case "user":
                    this.RelativeItem = profiles?.Find(it => it.Id == chat.Conversation?.peer?.Id);
                    break;
                case "group":
                    this.RelativeItem = groups?.Find(it => it.Id == -chat.Conversation?.peer?.Id);
                    break;
            }

            if (chat.LastMessage?.FromId > 0) {
                this.RelativeItemFromMessage = profiles?.Find(it => it.Id == chat.LastMessage?.FromId);
            } else {
                this.RelativeItemFromMessage = groups?.Find(it => it.Id == -chat.LastMessage?.FromId);
            }
        }
    }
}

