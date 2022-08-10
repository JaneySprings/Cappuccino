using System;
using Foundation;
using UIKit;

namespace Cappuccino.App.iOS.UI.Common {

	[Register("HeaderViewCell")]
	public partial class HeaderViewCell: UITableViewHeaderFooterView {
		public HeaderViewCell(IntPtr handle) : base(handle) {}

		public void SetCaption(string caption) {
			this.caption.Text = caption;
		}
		public void SetDivider(bool enable) {
			this.divider.Hidden = !enable;
		}
	}
}

