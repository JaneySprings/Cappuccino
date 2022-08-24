using Cappuccino.App.iOS.UI.Common;
using Cappuccino.App.iOS.UI.Contacts;
using Cappuccino.Core.Network.Models.Users;

namespace Cappuccino.App.iOS.UI.Search {
    public class SearchAdapterDelegate : TableViewAdapterBase<User, UserViewCell> {
        public SearchAdapterDelegate() : base(1) {}
    }
}