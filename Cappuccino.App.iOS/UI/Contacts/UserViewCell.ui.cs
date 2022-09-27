namespace Cappuccino.App.iOS.UI.Contacts;


public partial class UserViewCell {
    private UILabel? name;
    private UILabel? caption;
    private UIImageView? photo;


    public UserViewCell(IntPtr handle) : base(handle) { }
    public UserViewCell(): base() { }

    protected override void Initialize() {
        this.name = new UILabel();
        this.caption = new UILabel();
        this.photo = new UIImageView();

        this.name.ApplyCaption1Appearance();
        this.caption.ApplyCaption2Appearance();

        this.ContentView.AddSubview(this.photo);
        this.ContentView.AddSubview(this.name);
        this.ContentView.AddSubview(this.caption);

        this.BackgroundColor = Colors.Foreground;
    }

    public override CGSize SizeThatFits(CGSize size) => new CGSize(size.Width, 76);

    public override void LayoutSubviews() {
        base.LayoutSubviews();

        var width = this.ContentView.Bounds.Width;
        var height = this.ContentView.Bounds.Height;

        var photoSize = 52;
        var photoOffset = (height - photoSize) / 2;
        this.photo!.Frame = new CGRect(16, photoOffset, photoSize, photoSize);
        this.photo.ApplyRoundedAppearance();

        var textSpacing = 14;
        var textWidth = width - (48 + photoSize);
        var textHeight = height / 2 - textSpacing;
        this.name!.Frame = new CGRect(32 + photoSize, textSpacing, textWidth, textHeight);
        this.caption!.Frame = new CGRect(32 + photoSize, height / 2, textWidth, textHeight);
    }
}