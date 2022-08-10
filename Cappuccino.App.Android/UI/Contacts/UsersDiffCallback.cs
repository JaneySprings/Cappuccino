using Cappuccino.App.Android.UI.Common;
using Cappuccino.Core.Network.Models;

namespace Cappuccino.App.Android.UI {
    public class UsersDiffCallback: DiffUtilCallback<User> {
        public UsersDiffCallback(AdapterBase<User> adapter) : base(adapter) { }

        public override bool PrimaryKeysSame(User newItem, User oldItem) {
            return newItem.Id == oldItem.Id;
        }
        public override bool ItemsSame(User newItem, User oldItem) {
            return newItem.FirstName == oldItem.FirstName &&
                   newItem.Photo100 == oldItem.Photo100 &&
                   newItem.Online == oldItem.Online;
        }
    }
}