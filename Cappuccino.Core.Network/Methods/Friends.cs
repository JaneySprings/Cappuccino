using System.Collections.Generic;

namespace Cappuccino.Core.Network.Methods.Friends {

    /* 
     * Mark: documentation [https://vk.com/dev/friends.get]
     */
    public sealed class Get : ApiRequest<Models.Friends.GetResponse> {
        public IEnumerable<string> Fields { set => AddParam("fields", value); }
        public string Order { set => AddParam("order", value); }
        public int Count { set => AddParam("count", value); }
        public int Offset { set => AddParam("offset", value); }
        public string NameCase { set => AddParam("name_case", value); }
        public int UserId { set => AddParam("user_id", value); }
        public int ListId { set => AddParam("list_id", value); }

        public Get() : base("friends.get") {}
    }

    /* 
     * Mark: documentation [https://vk.com/dev/friends.search]
     */
    public sealed class Search : ApiRequest<Models.Friends.SearchResponse> {
        public string Query { set => AddParam("q", value); }
        public IEnumerable<string> Fields { set => AddParam("fields", value); }
        public int UserId { set => AddParam("user_id", value); }
        public int Count { set => AddParam("count", value); }
        public int Offset { set => AddParam("offset", value); }
        public string NameCase { set => AddParam("name_case", value); }

        public Search() : base("friends.search") {}
    }
}