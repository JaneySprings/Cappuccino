using System.Text.Json.Serialization;
using System.Collections.Generic;


namespace Cappuccino.Core.Network.Models.Users {

    /*  
     * Mark: documentation [https://vk.com/dev/users.get]
     */
    public class GetResponse {
        [JsonPropertyName("response")] public Response? InnerResponse { get; set; }

        public class Response {
            [JsonPropertyName("count")] public int Count { get; set; }
            [JsonPropertyName("items")] public List<User>? Items { get; set; }
        }
    }

    /*  
     * Mark: documentation [https://vk.com/dev/users.search]
     */
    public class SearchResponse {
        [JsonPropertyName("response")] public Response? InnerResponse { get; set; }

        public class Response {
            [JsonPropertyName("count")] public int Count { get; set; }
            [JsonPropertyName("items")] public List<User>? Items { get; set; }
        }
    }


    /* 
     * Mark: documentation [https://vk.com/dev/objects/user] 
     */
    public class User {
        // Base properties
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("first_name")] public string? FirstName { get; set; }
        [JsonPropertyName("last_name")] public string? LastName { get; set; }
        [JsonPropertyName("deactivated")] public string? Deactivated { get; set; }
        [JsonPropertyName("is_closed")] public bool IsClosed { get; set; }
        [JsonPropertyName("can_access_closed")] public bool CanAccessClosed { get; set; }

        // Optional properties
        [JsonPropertyName("about")] public string? About { get; set; }
        [JsonPropertyName("activities")] public string? Activities { get; set; }
        [JsonPropertyName("bdate")] public string? Bdate { get; set; }
        [JsonPropertyName("blacklisted")] public byte Blacklisted { get; set; }
        [JsonPropertyName("blacklisted_by_me")] public byte BlacklistedByMe { get; set; }
        [JsonPropertyName("books")] public string? Books { get; set; }
        [JsonPropertyName("can_post")] public byte CanPost { get; set; }
        [JsonPropertyName("can_see_all_posts")] public byte CanSeeAllPosts { get; set; }
        [JsonPropertyName("can_see_audio")] public byte CanSeeAudio { get; set; }
        [JsonPropertyName("can_send_friend_request")] public byte CanSendFriendRequest { get; set; }
        [JsonPropertyName("can_write_private_message")] public byte CanWritePrivateMessage { get; set; }
        [JsonPropertyName("career")] public Career? career { get; set; }
        [JsonPropertyName("city")] public City? city { get; set; }
        [JsonPropertyName("common_count")] public int CommonCount { get; set; }
        [JsonPropertyName("connections")] public Connections? connections { get; set; }
        [JsonPropertyName("contacts")] public Contacts? contacts { get; set; }
        [JsonPropertyName("counters")] public Counters? counters { get; set; }
        [JsonPropertyName("country")] public Country? country { get; set; }
        [JsonPropertyName("domain")] public string? Domain { get; set; }
        [JsonPropertyName("friend_status")] public int FriendStatus { get; set; }
        [JsonPropertyName("games")] public string? Games { get; set; }
        [JsonPropertyName("has_mobile")] public byte HasMobile { get; set; }
        [JsonPropertyName("has_photo")] public byte HasPhoto { get; set; }
        [JsonPropertyName("home_town")] public string? HomeTown { get; set; }
        [JsonPropertyName("interests")] public string? Interests { get; set; }
        [JsonPropertyName("is_favorite")] public byte IsFavorite { get; set; }
        [JsonPropertyName("is_friend")] public byte IsFriend { get; set; }
        [JsonPropertyName("is_hidden_from_feed")] public byte IsHiddenFromFeed { get; set; }
        [JsonPropertyName("is_no_index")] public byte IsNoIndex { get; set; }
        [JsonPropertyName("last_seen")] public LastSeen? lastSeen { get; set; }
        [JsonPropertyName("online")] public byte Online { get; set; }
        [JsonPropertyName("photo_50")] public string? Photo50 { get; set; }
        [JsonPropertyName("photo_100")] public string? Photo100 { get; set; }
        [JsonPropertyName("photo_200_orig")] public string? Photo200Orig { get; set; }
        [JsonPropertyName("photo_200")] public string? Photo200 { get; set; }
        [JsonPropertyName("photo_400_orig")] public string? Photo400Orig { get; set; }
        [JsonPropertyName("photo_max")] public string? PhotoMax { get; set; }
        [JsonPropertyName("photo_max_orig")] public string? PhotoMaxOrig { get; set; }
        [JsonPropertyName("quotes")] public string? Quotes { get; set; }
        [JsonPropertyName("relation")] public int Relation { get; set; }
        [JsonPropertyName("screen_name")] public string? ScreenName { get; set; }
        [JsonPropertyName("status")] public string? Status { get; set; }

        public class Career {
            [JsonPropertyName("group_id")] public int GroupId { get; set; }
            [JsonPropertyName("company")] public string? Company { get; set; }
            [JsonPropertyName("country_id")] public int CountryId { get; set; }
            [JsonPropertyName("city_id")] public int CityId { get; set; }
            [JsonPropertyName("city_name")] public string? CityName { get; set; }
            [JsonPropertyName("from")] public int From { get; set; }
            [JsonPropertyName("until")] public byte Until { get; set; }
            [JsonPropertyName("position")] public string? Position { get; set; }
        }

        public class City {
            [JsonPropertyName("id")] public int Id { get; set; }
            [JsonPropertyName("title")] public string? Title { get; set; }
        }

        public class Connections {
            [JsonPropertyName("skype")] public string? Skype { get; set; }
            [JsonPropertyName("facebook")] public string? Facebook { get; set; }
            [JsonPropertyName("twitter")] public string? Twitter { get; set; }
            [JsonPropertyName("livejournal")] public string? Livejournal { get; set; }
            [JsonPropertyName("instagram")] public string? Instagram { get; set; }
        }

        public class Contacts {
            [JsonPropertyName("mobile_phone")] public string? MobilePhone { get; set; }
            [JsonPropertyName("home_phone")] public string? HomePhone { get; set; }
        }

        public class Counters {
            [JsonPropertyName("albums")] public int Albums { get; set; }
            [JsonPropertyName("videos")] public int Videos { get; set; }
            [JsonPropertyName("audios")] public int Audios { get; set; }
            [JsonPropertyName("photos")] public int Photos { get; set; }
            [JsonPropertyName("notes")] public int Notes { get; set; }
            [JsonPropertyName("friends")] public int Friends { get; set; }
            [JsonPropertyName("groups")] public int Groups { get; set; }
            [JsonPropertyName("online_friends")] public int OnlineFriends { get; set; }
            [JsonPropertyName("mutual_friends")] public int MutualFriends { get; set; }
            [JsonPropertyName("user_videos")] public int UserVideos { get; set; }
            [JsonPropertyName("followers")] public int Followers { get; set; }
            [JsonPropertyName("pages")] public int Pages { get; set; }
        }

        public class Country {
            [JsonPropertyName("id")] public int Id { get; set; }
            [JsonPropertyName("title")] public string? Title { get; set; }
        }

        public class LastSeen {
            [JsonPropertyName("time")] public int Time { get; set; }
            [JsonPropertyName("platform")] public int Platform { get; set; }
        }
    }
}