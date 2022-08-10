using System;
using Cappuccino.App.iOS.Extensions;
using Cappuccino.App.iOS.UI.Common;
using Cappuccino.App.iOS.UI.Styles;
using Cappuccino.Core.Network.Models.Users;
using Foundation;
using UIKit;

namespace Cappuccino.App.iOS.UI.Contacts {

    public partial class UsersAdapterDelegate : TableViewAdapterBase<User, UserViewCell> {
        protected override void InitializeSections(int count) {
            base.InitializeSections(2);
        }

        [Export("tableView:viewForHeaderInSection:")]
        public UIView GetViewForHeader(UITableView tableView, nint section) {
            UITableViewHeaderFooterView view = tableView.DequeueReusableHeaderFooterViewEx<HeaderViewCell>();
            HeaderViewCell? cell = view as HeaderViewCell;
           
            cell?.SetCaption(section == 0 ? "Important" : $"All ({ItemLimit})");
            cell?.SetDivider(section == 1);

            return cell!;
        }
    }

    [Register("UserViewCell")]
    public partial class UserViewCell : TableViewCellBase<User> {
        public UserViewCell(IntPtr handle) : base(handle) { }

        public override void Bind(User item) {
            this.name.Text = $"{item.FirstName} {item.LastName}";

            if (item.Online == 1) {
                this.caption.Text = "online";
                this.caption.TextColor = UIColor.FromName(AppStyle.Instance.AccentColorName);
            } else {
                this.caption.Text = $"was online {item.lastSeen?.Time.ParseShortDate()}";
                this.caption.TextColor = UIColor.FromName(AppStyle.Instance.TextGrayLightColorName);
            }
 
            this.photo.Load(item.Photo100);
        }
    }
}