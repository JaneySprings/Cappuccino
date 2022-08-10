using System;
using Foundation;
using UIKit;

namespace Cappuccino.App.iOS.UI.Styles {

    [Register("UIRoundedImageView")]
    public class UIRoundedImageView: UIImageView {
        public UIRoundedImageView(IntPtr handle) : base(handle) {}

        public override void LayoutSubviews() {
            base.LayoutSubviews();
            Layer.CornerRadius = Bounds.Height / 2;
        }
    }
}

