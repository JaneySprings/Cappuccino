using System.Collections.Generic;

namespace Cappuccino.Core.Network.Methods {

    public class UserFields {
        public const string About = "about";
        public const string Activities = "activities";
        public const string BDate = "bdate";
        public const string Blacklisted = "blacklisted";
        public const string BlacklistedByMe = "blacklisted_by_me";
        public const string Books = "books";
        public const string CanPost = "can_post";
        public const string CanSeeAllPosts = "can_see_all_posts";
        public const string CanSeeAudio = "can_see_audio";
        public const string CanSendFriendRequest = "can_send_friend_request";
        public const string CanWritePrivateMessage = "can_write_private_message";
        public const string Career = "career";
        public const string City = "city";
        public const string CommonCount = "common_count";
        public const string Connections = "connections";
        public const string Contacts = "contacts";
        public const string Counters = "counters";
        public const string Country = "country";
        public const string Education = "education";
        public const string Exports = "exports";
        public const string Games = "games";
        public const string HomeTown = "home_town";
        public const string Interests = "interests";
        public const string FriendStatus = "friend_status";
        public const string IsFriend = "is_friend";
        public const string IsHiddenFromFeed = "is_hidden_from_feed";
        public const string LastSeen = "last_seen";
        public const string Lists = "lists";
        public const string MaidenName = "maiden_name";
        public const string Movies = "movies";
        public const string Music = "music";
        public const string Nickname = "nickname";
        public const string Occupation = "occupation";
        public const string Online = "online";
        public const string Personal = "personal";
        public const string Photo50 = "photo_50";
        public const string Photo100 = "photo_100";
        public const string Photo200Orig = "photo_200_orig";
        public const string Photo200 = "photo_200";
        public const string Photo400Orig = "photo_400_orig";
        public const string PhotoId = "photo_id";
        public const string PhotoMax = "photo_max";
        public const string PhotoMaxOrig = "photo_max_orig";
        public const string Quotes = "quotes";
        public const string Relatives = "relatives";
        public const string Relation = "relation";
        public const string Schools = "schools";
        public const string ScreenName = "screen_name";
        public const string Site = "site";
        public const string Status = "status";
        public const string Trending = "trending";
        public const string Tv = "tv";
        public const string Verified = "verified";

        public static IEnumerable<string> Default = new[] {
            LastSeen, Online, City, Country, HomeTown, Status, ScreenName, Photo100, Photo200
        };
    }

    public enum NameCases { nom, gen, dat, acc, ins, abl }
}