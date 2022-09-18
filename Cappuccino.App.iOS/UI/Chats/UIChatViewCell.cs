using System;


namespace Cappuccino.App.iOS.UI.Chats;

public partial class ChatViewCell {
    private UILabel? title;
    private UILabel? message;
    private UIImageView? photo;

    public ChatViewCell(IntPtr handle) : base(handle) { }
    protected override void Initialize() {
        this.title = new UILabel();
        this.message = new UILabel();
        this.photo = new UIImageView();

        this.title.ApplyCaption1Appearance();
        this.message.ApplyCaption2Appearance();

        this.ContentView.AddSubview(this.photo);
        this.ContentView.AddSubview(this.title);
        this.ContentView.AddSubview(this.message);

        this.BackgroundColor = UIColor.FromName("foreground");
    }

    public override void LayoutSubviews() {
        base.LayoutSubviews();

        var cellSize = 78;
        var photoSize = 58;
        var photoOffset = (cellSize / 2) - (photoSize / 2);

        this.ContentView.Frame = new CGRect(0, 0, this.ContentView.Frame.Width, cellSize);

        this.photo!.Frame = new CGRect(16, photoOffset, photoSize, photoSize);
        this.photo.ApplyRoundedAppearance();

        var textSpacing = 14;
        var textWidth = this.ContentView.Frame.Width - (48 + photoSize);
        var textHeight = (this.ContentView.Frame.Height / 2) - textSpacing;
        this.title!.Frame = new CGRect(32 + photoSize, textSpacing, textWidth, textHeight);

        var frameHalf = this.ContentView.Frame.Height / 2;
        this.message!.Frame = new CGRect(32 + photoSize, frameHalf, textWidth, textHeight);

    }
}

