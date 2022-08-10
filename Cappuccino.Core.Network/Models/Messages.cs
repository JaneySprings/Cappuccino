using System.Text.Json.Serialization;
using System.Collections.Generic;
using Cappuccino.Core.Network.Models.Groups;
using Cappuccino.Core.Network.Models.Users;

namespace Cappuccino.Core.Network.Models.Messages {
    /* 
     * Mark: documentation [https://vk.com/dev/messages.getLongPollServer]
     */
    public class GetLongPollServerResponse {
        [JsonPropertyName("response")] public Response? InnerResponse { get; set; }

        public class Response {
            [JsonPropertyName("server")] public string? Server { get; set; }
            [JsonPropertyName("key")] public string? Key { get; set; }
            [JsonPropertyName("ts")] public int Ts { get; set; }
            [JsonPropertyName("pts")] public int Pts { get; set; }
        }
    }

    /*  
     * Mark: documentation [https://vk.com/dev/messages.getConversations]
     */
    public class GetConversationsResponse {
        [JsonPropertyName("response")] public Response? InnerResponse { get; set; }

        public class Response {
            [JsonPropertyName("count")] public int Count { get; set; }
            [JsonPropertyName("items")] public List<Chat>? Items { get; set; }
            [JsonPropertyName("profiles")] public List<User>? Profiles { get; set; }
            [JsonPropertyName("groups")] public List<Group>? Groups { get; set; }
        }
    }

    /* 
     * 
     */

    public class Chat {
        [JsonPropertyName("conversation")] public Conversation? Conversation { get; set; }
        [JsonPropertyName("last_message")] public Message? LastMessage { get; set; }
    }

    public class Conversation {
        [JsonPropertyName("peer")] public Peer? peer { get; set; }
        [JsonPropertyName("last_message_id")] public int LastMessageId { get; set; }
        [JsonPropertyName("in_read")] public int InRead { get; set; }
        [JsonPropertyName("out_read")] public int OutRead { get; set; }
        [JsonPropertyName("sort_id")] public SortId? sortId { get; set; }
        [JsonPropertyName("last_conversation_message_id")] public int LastConversationMessageId { get; set; }
        [JsonPropertyName("in_read_cmid")] public int InReadCmid { get; set; }
        [JsonPropertyName("out_read_cmid")] public int OutReadCmid { get; set; }
        [JsonPropertyName("unread_count")] public int UnreadCount { get; set; }
        [JsonPropertyName("is_marked_unread")] public bool IsMarkedUnread { get; set; }
        [JsonPropertyName("important")] public bool Important { get; set; }
        [JsonPropertyName("chat_settings")] public ChatSettings? chatSettings { get; set; }

        public class Peer {
            [JsonPropertyName("id")] public int Id { get; set; }
            [JsonPropertyName("type")] public string? Type { get; set; }
            [JsonPropertyName("local_id")] public int LocalId { get; set; }
        }

        public class SortId {
            [JsonPropertyName("major_id")] public int MajorId { get; set; }
            [JsonPropertyName("minor_id")] public int MinorId { get; set; }
        }

        public class ChatSettings {
            [JsonPropertyName("members_count")] public int MembersCount { get; set; }
            [JsonPropertyName("title")] public string? Title { get; set; }
            [JsonPropertyName("pinned_message")] public Message? PinnedMessage { get; set; }
            [JsonPropertyName("state")] public string? State { get; set; }
            [JsonPropertyName("photo")] public Photo? Photo { get; set; }
            [JsonPropertyName("active_ids")] public List<int>? ActiveIds { get; set; }
            [JsonPropertyName("is_group_channel")] public bool IsGroupChannel { get; set; }
        }

        public class Photo {
            [JsonPropertyName("photo_200")] public string? Photo200 { get; set; }
            [JsonPropertyName("photo_100")] public string? Photo100 { get; set; }
            [JsonPropertyName("photo_50")] public string? Photo50 { get; set; }
        }
    }

    public class Message {
        [JsonPropertyName("date")] public int Date { get; set; }
        [JsonPropertyName("from_id")] public int FromId { get; set; }
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("out")] public int Out { get; set; }
        [JsonPropertyName("attachment")] public List<Attachment>? Attachments { get; set; }
        [JsonPropertyName("conversation_message_id")] public int ConversationMessageId { get; set; }
        [JsonPropertyName("fwd_messages")] public List<Message>? FwdMessages { get; set; }
        [JsonPropertyName("important")] public bool Important { get; set; }
        [JsonPropertyName("is_hidden")] public bool IsHidden { get; set; }
        [JsonPropertyName("peer_id")] public int PeerId { get; set; }
        [JsonPropertyName("random_id")] public int RandomId { get; set; }
        [JsonPropertyName("text")] public string? Text { get; set; }

        public class Attachment {

        }
    }
}