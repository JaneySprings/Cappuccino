namespace Cappuccino.Core.Network.Methods.Account {

    /* 
     * Mark: documentation [https://vk.com/dev/account.getCounters] 
     */
    public class GetCounters : ApiRequest<Models.Account.GetCountersResponse> {
        public GetCounters(int? user_id = null,
                           Filter? filter = null) : base("account.getCounters") {

            AddParam(nameof(user_id), user_id);
            AddParam(nameof(filter), filter?.ToString());
        }
        
        public enum Filter {
            friends,
            friends_suggestions,
            messages,
            photos,
            videos,
            gifts,
            events,
            groups,
            notifications,
            sdk,
            app_requests,
            friends_recommendations,
            menu_discover_badge,
            menu_clips_badge,
            menu_superapp_friends_badge,
            menu_new_clips_badge,
            calls
        }
    }

}