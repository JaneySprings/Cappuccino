using System.Collections.Generic;
namespace Cappuccino.Core.Network.Methods.Friends {

    public enum Order { hints, random, name }

    /* 
     * Mark: documentation [https://vk.com/dev/friends.get]
     */
    public sealed class Get : ApiRequest<Models.Friends.GetResponse> {
        public Get(IEnumerable<string>? fields = null,
                   Order? order = null,
                   int? offset = null,
                   int? count = null,
                   int? user_id = null,
                   int? list_id = null,
                   NameCases? name_case = null) : base("friends.get") {

            AddParam(nameof(order), order?.ToString());
            AddParam(nameof(name_case), name_case?.ToString());
            AddParam(nameof(user_id), user_id);
            AddParam(nameof(count), count);
            AddParam(nameof(offset), offset);
            AddParam(nameof(fields), fields);
            AddParam(nameof(list_id), list_id);
        }
    }

    /* 
     * Mark: documentation [https://vk.com/dev/friends.search]
     */
    public sealed class Search : ApiRequest<Models.Friends.SearchResponse> {
        public Search(string q,
                      IEnumerable<string>? fields = null,
                      int? user_id = null,
                      int? offset = null,
                      int? count = null,
                      NameCases? name_case = null) : base("friends.search") {

            AddParam(nameof(name_case), name_case?.ToString());
            AddParam(nameof(q), q);
            AddParam(nameof(fields), fields);
            AddParam(nameof(user_id), user_id);
            AddParam(nameof(offset), offset);
            AddParam(nameof(count), count);
        }
    }
}