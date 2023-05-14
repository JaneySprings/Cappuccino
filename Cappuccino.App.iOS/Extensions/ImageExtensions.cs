using SDWebImage;

namespace Cappuccino.App.iOS.Extensions;


public static class ImageExtensions {
    public static void Load(this UIImageView imageView, string? path) {
        if (path == null || path == String.Empty) 
            return;

        NSUrl? url = NSUrl.FromString(path);
        imageView.Sd_setImageWithURL(url);
    }
}
