namespace Cappuccino.App.iOS.UI.Messages;


public partial class MessageViewCell {
    private UILabel? message;


    public MessageViewCell(IntPtr handle) : base(handle) { }
    public MessageViewCell(): base() { }


    protected override void Initialize() {
        this.message = new UILabel().ApplyDefaultAppearance();

        this.ContentView.AddSubview(message);
    }

    public override CGSize SizeThatFits(CGSize size) {
        var messageSize = message!.SizeThatFits(size);
        return messageSize;
    }

    public override void LayoutSubviews() {
        base.LayoutSubviews();
        var messageSize = message!.SizeThatFits(new CGSize(ContentView.Bounds.Width, ContentView.Bounds.Height));
        message!.Frame = new CGRect(0, 0, messageSize.Width, messageSize.Height);
    }
}