namespace Cappuccino.Core.Network.Methods.Account {

    /* 
     * Mark: documentation [https://vk.com/dev/account.getCounters] 
     */
    public class GetCounters : ApiRequest<Models.Account.GetCountersResponse> {
        public int UserId { set => AddParam("user_id", value); }
        public string Filter { set => AddParam("filter", value); }

        public GetCounters() : base("account.getCounters") {}
    }
}