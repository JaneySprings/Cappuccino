using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Cappuccino.Core.Network.Models.Messages {

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
    }

    public class Message {
        [JsonPropertyName("date")] public int Date { get; set; }
        [JsonPropertyName("from_id")] public int FromId { get; set; }
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("out")] public int Out { get; set; }
        [JsonPropertyName("attachments")] public List<Attachment>? Attachments { get; set; }
        [JsonPropertyName("conversation_message_id")] public int ConversationMessageId { get; set; }
        [JsonPropertyName("fwd_messages")] public List<Message>? FwdMessages { get; set; }
        [JsonPropertyName("reply_message")] public Message? ReplyMessage { get; set; }
        [JsonPropertyName("important")] public bool Important { get; set; }
        [JsonPropertyName("is_hidden")] public bool IsHidden { get; set; }
        [JsonPropertyName("peer_id")] public int PeerId { get; set; }
        [JsonPropertyName("random_id")] public int RandomId { get; set; }
        [JsonPropertyName("text")] public string? Text { get; set; }
        [JsonPropertyName("action")] public Action? action { get; set; }
        [JsonPropertyName("admin_author_id")] public int AdminAuthorId { get; set; }
        [JsonPropertyName("is_cropped")] public bool IsCroped { get; set; }
        [JsonPropertyName("members_count")] public int MembersCount { get; set; }
        [JsonPropertyName("update_time")] public int UpdateTime { get; set; }
        [JsonPropertyName("was_listened")] public bool WasListened { get; set; }
        [JsonPropertyName("pinned_at")] public int PinnedAt { get; set; }
        [JsonPropertyName("message_tag")] public string? MessageTag { get; set; }

        public class Attachment {
            [JsonPropertyName("type")] public string? Type { get; set; }
            [JsonPropertyName("photo")] public Photo? photo { get; set; }
            [JsonPropertyName("video")] public Video? video { get; set; }
            [JsonPropertyName("audio")] public Audio? audio { get; set; }
            [JsonPropertyName("doc")] public Document? Doc { get; set; }
            [JsonPropertyName("link")] public Link? link { get; set; }
            [JsonPropertyName("sticker")] public Sticker? sticker { get; set; }
            [JsonPropertyName("graffiti")] public Graffiti? graffiti { get; set; }
            [JsonPropertyName("audio_message")] public AudioMessage? audioMessage { get; set; }
            [JsonPropertyName("wall")] public Wall? wall { get; set; }

            public class Photo {}

            public class Video {}

            public class Audio {}

            public class Document {}

            public class Link {}

            public class Sticker {}

            public class Graffiti {}

            public class AudioMessage {}

            public class Wall {}
        }

        public class Action {
            [JsonPropertyName("type")] public string? Type { get; set; }
            [JsonPropertyName("member_id")] public int MemberId { get; set; }
            [JsonPropertyName("text")] public string? Text { get; set; }
            [JsonPropertyName("email")] public string? Email { get; set; }
            [JsonPropertyName("photo")] public Photo? Photo { get; set; }
        }
    }
}