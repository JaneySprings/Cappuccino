namespace Cappuccino.App.iOS.UI.Chats;


public partial class ChatViewCell {
    private UILabel? title;
    private UILabel? message;
    private UILabel? unread;
    private UIImageView? photo;
    private UIImageView? online;
    private UIImageView? read;


    public ChatViewCell(IntPtr handle) : base(handle) { }
    public ChatViewCell(): base() { }

    protected override void Initialize() {
        this.title = new UILabel();
        this.message = new UILabel();
        this.unread = new UILabel();
        this.photo = new UIImageView();
        this.online = new UIImageView();
        this.read = new UIImageView();

        this.title.ApplyCaption1Appearance();
        this.message.ApplyCaption2Appearance();
        this.unread.ApplyBadgeAppearance();

        this.ContentView.AddSubview(this.photo);
        this.ContentView.AddSubview(this.online);
        this.ContentView.AddSubview(this.title);
        this.ContentView.AddSubview(this.message);
        this.ContentView.AddSubview(this.read);
        this.ContentView.AddSubview(this.unread);

        this.BackgroundColor = Colors.Foreground;

        this.online.BackgroundColor = Colors.Foreground;
        this.online.TintColor = Colors.Accent;
        this.online.Image = UIImage.FromBundle("dot_24");

        this.read.TintColor = Colors.TextGray;
        this.read.Image = UIImage.FromBundle("dot_24");

        this.unread.BackgroundColor = Colors.Accent;
    }

    public override CGSize SizeThatFits(CGSize size) => new CGSize(size.Width, 78);

    public override void LayoutSubviews() {
        base.LayoutSubviews();

        var width = this.ContentView.Bounds.Width;
        var height = this.ContentView.Bounds.Height;

        var photoSize = 56;
        var onlineSize = 18;
        var readSize = 12;
        var photoOffset = (height - photoSize) / 2;

        this.photo!.Frame = new CGRect(16, photoOffset, photoSize, photoSize);
        this.photo.ApplyRoundedAppearance();

        this.online!.Frame = new CGRect(16 + photoSize - onlineSize, height - photoOffset - onlineSize, onlineSize, onlineSize);
        this.online.ApplyRoundedAppearance();

        this.read!.Frame = new CGRect(width - 16 - readSize, (height - readSize) / 2, readSize, readSize);
        this.read.ApplyRoundedAppearance();

        var unreadSize = this.unread!.SizeThatFits(new CGSize(width/2, height));
        unreadSize.Width += 20;

        if (this.unread!.Text?.Length == 1) {
            var size = Math.Max(unreadSize.Width, unreadSize.Height);
            unreadSize.Width = photoSize / 2;
            unreadSize.Height = photoSize / 2;
        }

        this.unread.Frame = new CGRect(width - 16 - unreadSize.Width, (height - unreadSize.Height) / 2, unreadSize.Width, photoSize / 2);
        this.unread.ApplyRoundedAppearance();

        var readWidth = read.Hidden ? 0 : (readSize + 16);
        var unreadWidth = unread.Hidden ? 0 : (unreadSize.Width + 16);
        var textSpacing = 12;
        var textWidth = width - (48 + photoSize) - readWidth - unreadWidth;
        var textHeight = height / 2 - textSpacing;

        this.title!.Frame = new CGRect(32 + photoSize, textSpacing, textWidth, textHeight);
        this.message!.Frame = new CGRect(32 + photoSize, height / 2, textWidth, textHeight);

    }
}

