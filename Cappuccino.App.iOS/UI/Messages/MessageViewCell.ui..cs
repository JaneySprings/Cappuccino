namespace Cappuccino.App.iOS.UI.Messages;


public partial class MessageViewCell {
    private UILabel? message;
    private UILabel? date;
    private UIImageView? photo;
    private UIView? container;

    private readonly nfloat containerWidthRation = 0.65f;
    private readonly UIEdgeInsets containerContentInsets = new UIEdgeInsets(8, 12, 8, 12);
    private readonly UIEdgeInsets containerViewInsets = new UIEdgeInsets(8, 8, 8, 8);


    private CGSize containerRequestedSize = CGSize.Empty;
    private CGSize messageTextRequestedSize = CGSize.Empty;
    private CGSize messageDateRequestedSize = CGSize.Empty;
    private int containerHorizontalAlignment = 0;
    private bool displayAdditinalInfo = false;



    public MessageViewCell(IntPtr handle) : base(handle) { }
    public MessageViewCell(): base() { }


    protected override void Initialize() {
        this.message = new UILabel();
        this.date = new UILabel();
        this.photo = new UIImageView();
        this.container = new UIView();

        this.message.ApplyDefaultAppearance();
        this.message.ApplyMultilineAppearance();
        this.date.ApplyCaption3Appearance();

        this.container.AddSubview(this.message);
        this.container.AddSubview(this.date);
        this.ContentView.AddSubview(this.photo);
        this.ContentView.AddSubview(this.container);

        this.ContentView.BackgroundColor = Colors.Foreground;
        this.container.Layer.CornerRadius = 18;
    }

    public override CGSize SizeThatFits(CGSize size) {
        var containerMaxWidth = size.Width * this.containerWidthRation;
        var messageMaxWidth = containerMaxWidth - containerContentInsets.Left - containerContentInsets.Right; 
        messageTextRequestedSize = this.message!.SizeThatFits(new CGSize(messageMaxWidth, nfloat.MaxValue));
        messageDateRequestedSize = this.date!.SizeThatFits(new CGSize(messageMaxWidth, nfloat.MaxValue));
        
        var primaryContentWidth = nfloat.Max(messageTextRequestedSize.Width, messageDateRequestedSize.Width);
        var containerWidth = primaryContentWidth + containerContentInsets.Left + containerContentInsets.Right;
        var containerHeight = messageTextRequestedSize.Height + messageDateRequestedSize.Height + containerContentInsets.Top + containerContentInsets.Bottom;
        var cellHeight = containerHeight + containerViewInsets.Top + containerViewInsets.Bottom;

        containerRequestedSize = new CGSize(containerWidth, containerHeight);
        return new CGSize(size.Width, cellHeight);
    }

    public override void LayoutSubviews() {
        base.LayoutSubviews();

        var width = this.ContentView.Bounds.Width;
        var height = this.ContentView.Bounds.Height;

        if (displayAdditinalInfo) {
            this.photo!.Frame = new CGRect(
                containerViewInsets.Left, 
                height - containerViewInsets.Bottom - Dimensions.ButtonIconSize, 
                Dimensions.ButtonIconSize, Dimensions.ButtonIconSize
            );
            this.photo.ApplyRoundedAppearance();
        } else {
            this.photo!.Frame = CGRect.Empty;
        }

        if (containerHorizontalAlignment == 1) {
            this.container!.Frame = new CGRect(
                width - containerViewInsets.Right - containerRequestedSize.Width, 
                containerViewInsets.Top, 
                containerRequestedSize.Width, 
                containerRequestedSize.Height
            );
        } else {
            this.container!.Frame = new CGRect(
                containerViewInsets.Left + this.photo.Frame.X + this.photo.Frame.Width, 
                containerViewInsets.Top, 
                containerRequestedSize.Width, 
                containerRequestedSize.Height
            );
        }

        this.message!.Frame = new CGRect(
            containerContentInsets.Left, 
            containerContentInsets.Top, 
            messageTextRequestedSize.Width, 
            messageTextRequestedSize.Height
        );
        this.date!.Frame = new CGRect(
            containerRequestedSize.Width - messageDateRequestedSize.Width - containerContentInsets.Right, 
            containerContentInsets.Top + messageTextRequestedSize.Height, 
            messageDateRequestedSize.Width, 
            messageDateRequestedSize.Height
        );
    }
}