using System;
using UIKit;

namespace Cappuccino.App.iOS.UI.Common {
    public abstract class TableViewCellBase<TItem>: UITableViewCell {
        public TableViewCellBase(IntPtr handle) : base(handle) { }

        public abstract void Bind(TItem item);
    }
}