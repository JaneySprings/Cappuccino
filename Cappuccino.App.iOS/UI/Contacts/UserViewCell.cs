using Cappuccino.App.iOS.Extensions;
using Cappuccino.App.iOS.UI.Common;
using Cappuccino.Core.Network.Models.Users;

namespace Cappuccino.App.iOS.UI.Contacts;


public partial class UserViewCell : TableViewCellBase<User> {
    public override void Bind(User item) {
        this.name!.Text = $"{item.FirstName} {item.LastName}";

        if (item.Online == 1) {
            this.caption!.Text = "online";
            this.caption.TextColor = Colors.Accent;
        } else {
            this.caption!.Text = $"was online {item.lastSeen?.Time.ParseShortDate()}";
            this.caption.TextColor = Colors.TextGray;
        }

        this.photo!.Load(item.Photo100);
    }
}