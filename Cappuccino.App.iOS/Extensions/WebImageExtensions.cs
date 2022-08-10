using System;
using Foundation;
using Ricardo.SDWebImage.iOS;
using UIKit;

namespace Cappuccino.App.iOS.Extensions {

    public static class WebImageExtensions {
        public static void Load(this UIImageView imageView, string? path) {
            if (path == null) return;
            if (path == String.Empty) return;

            NSUrl? url = NSUrl.FromString(path);
            imageView.Sd_setImageWithURL(url);
        }
    }
}