using System;
using Foundation;
using UIKit;

namespace Cappuccino.App.iOS.UI.Styles {

    [Register("UIStylableLabel")]
    public class UIStylableLabel: UILabel {
        public UIStylableLabel(IntPtr handle) : base(handle) {
            switch (Tag) {
                case 1: ApplyStyle(AppStyle.Instance.TextHeading1Font); break;
                case 2: ApplyStyle(AppStyle.Instance.TextHeading2Font); break;
                case 3: ApplyStyle(AppStyle.Instance.TextHeading3Font); break;
                case 4: ApplyStyle(AppStyle.Instance.TextCaption4Font); break;
                case 5: ApplyStyle(AppStyle.Instance.TextCaption5Font); break;
            }
        }

        private void ApplyStyle(FontStyle style) {
            UIFont font;

            if (style.Name != null) {
                font = UIFont.FromName(style.Name, style.Size);
            } else {
                font = UIFont.SystemFontOfSize(style.Size, style.Weight);
            }

            Font = font;
            TextColor = UIColor.FromName(style.Color);
        }
    }
}

