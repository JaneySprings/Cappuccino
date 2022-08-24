using System;
using UIKit;
using CoreGraphics;

namespace Cappuccino.App.iOS.UI.Contacts {

    public partial class UserViewCell {
        private UILabel? name;
        private UILabel? caption;
        private UIImageView? photo;

        public UserViewCell(IntPtr handle) : base(handle) { }
        protected override void Initialize() {
            this.name = new UILabel();
            this.caption = new UILabel();
            this.photo = new UIImageView();

            this.name.ApplyCaption1Appearance();
            this.caption.ApplyCaption2Appearance();

            this.ContentView.AddSubview(this.photo);
            this.ContentView.AddSubview(this.name);
            this.ContentView.AddSubview(this.caption);

            this.BackgroundColor = UIColor.FromName("foreground");
        }


        public override void LayoutSubviews() {
            base.LayoutSubviews();
            this.ContentView.Frame = new CGRect(0, 0, this.ContentView.Frame.Width, 76);

            var photoSize = 52;
            var photoOffset = (this.ContentView.Frame.Height - photoSize) / 2;
            this.photo!.Frame = new CGRect(16, photoOffset, photoSize, photoSize);
            this.photo.ApplyRoundedAppearance();

            var textSpacing = 14;
            var textEndOffset = this.ContentView.Frame.Width - (48 + photoSize);
            var textHeight = (this.ContentView.Frame.Height / 2) - textSpacing;
            this.name!.Frame = new CGRect(32 + photoSize, textSpacing, textEndOffset, textHeight);

            var frameHalf = this.ContentView.Frame.Height / 2;
            this.caption!.Frame = new CGRect(32 + photoSize, frameHalf, textEndOffset, textHeight);
        }
    }
}

