using UIKit;

namespace Cappuccino.App.iOS.UI.Styles {

    public class AppStyle {
        public string AccentColorName { get; } = "accent";
        public string ForegroundColorName { get; } = "foreground";
        public string BackgroundColorName { get; } = "background";

        public string TextColorName { get; } = "text";
        public string TextGrayLightColorName { get; } = "text_gray_light";
        public string TextGrayColorName { get; } = "text_gray";

        public FontStyle TextHeading1Font { get; } = new FontStyle(22f, "text", "VKSansDisplay-DemiBold");          // Navigation Header
        public FontStyle TextHeading2Font { get; } = new FontStyle(19f, "text_gray_light", "VKSansDisplay-Medium"); // Lists Header
        public FontStyle TextHeading3Font { get; } = new FontStyle(17f, "text", UIFontWeight.Medium);               // Item Header
        public FontStyle TextCaption4Font { get; } = new FontStyle(16f, "text", UIFontWeight.Regular);              // Regular Text
        public FontStyle TextCaption5Font { get; } = new FontStyle(15f, "text_gray_light", UIFontWeight.Regular);   // Description


        private readonly static AppStyle instance = new AppStyle();
        public static AppStyle Instance { get; } = instance;
    }

    public class FontStyle {
        public string? Name;
        public string Color;
        public float Size;
        public UIFontWeight Weight;

        public FontStyle(float size, string color, string name) {
            this.Name = name;
            this.Color = color;
            this.Size = size;
        }

        public FontStyle(float size, string color, UIFontWeight weight) {
            this.Weight = weight;
            this.Color = color;
            this.Size = size;
        }
    };
}

