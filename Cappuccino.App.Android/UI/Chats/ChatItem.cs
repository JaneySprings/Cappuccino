using System.Collections.Generic;
using Cappuccino.Core.Network.Models;

namespace Cappuccino.App.Android.UI {
    public class ChatItem {
        public readonly int Id;
        public readonly int Date;
        public readonly int UnreadCount;
        public readonly string Title;
        public readonly string Photo100;
        public readonly string Photo200;
        public readonly string Sender;
        public readonly string Message;
        public readonly bool IsOnline;
        public readonly bool IsRead;
        public readonly bool IsOut;

        public ChatItem(Chat chat, List<User> profiles, List<Group> groups) {
            switch (chat.Conversation?.Peer?.Type) {
                case "user":
                    User user = profiles.Find(it => it.Id == chat.Conversation.Peer.Id);
                    this.Title = $"{user.FirstName} {user.LastName}";
                    this.Photo100 = user.Photo100!;
                    this.Photo200 = user.Photo200!;
                    this.IsOnline = user.Online == 1;
                    break;
                case "group":
                    Group group = groups.Find(it => it.Id == chat.Conversation.Peer.Id);
                    this.Title = group.Name!;
                    this.Photo100 = group.Photo100!;
                    this.Photo200 = group.Photo200!;
                    this.IsOnline = false;
                    this.Sender = this.Title;
                    break;
                case "chat":
                    this.Title = chat.Conversation.ChatSettings!.Title!;
                    this.Photo100 = chat.Conversation.ChatSettings!.Photo?.Photo100 ?? "";
                    this.Photo200 = chat.Conversation.ChatSettings!.Photo?.Photo200 ?? "";
                    this.IsOnline = false;
                    if (chat.LastMessage?.Out == 0) {
                        if (chat.LastMessage.FromId < 0) { // Is from group
                            group = groups.Find(it => it.Id == -chat.LastMessage.FromId);
                            this.Sender = group.Name ?? "";
                        } else { // If from user
                            user = profiles.Find(it => it.Id == chat.LastMessage.FromId);
                            this.Sender = user.FirstName ?? "";
                        }
                    }
                    break;
            }
            this.Id = chat.Conversation!.Peer!.Id;
            this.Message = chat.LastMessage!.Text!;
            this.UnreadCount = chat.Conversation.UnreadCount;
            this.IsRead = chat.LastMessage.Id == chat.Conversation.OutRead;
            this.Date = chat.LastMessage.Date;
            this.IsOut = chat.LastMessage?.Out == 1;
        }
    }
}