namespace Cappuccino.App.iOS.UI.Messages;


public partial class MessagesViewController {
    private UIImageView? chatPhotoButton;
    private UIButton? sendMessageButton;
    private UIButton? stickerViewButton;
    private UIButton? attachMediaButton;
    private UITextView? messageBoxField;
    private UIView? bottomBarContainer;
    private UILabel? messageBoxPlaceholderView;
    private UITableView? tableView;

    private nfloat keyboardFrameHeight;


    public override bool HidesBottomBarWhenPushed => true;
    private string? TitleLabel { set => this.NavigationItem.Title = value; }

    
    public override void ViewDidLoad() {
        base.ViewDidLoad();
        
        var photoContainer = new UIView(new CGRect(0, 0, Dimensions.ButtonIconSize, Dimensions.ButtonIconSize));
        var photoItem = new UIBarButtonItem(photoContainer);

        this.chatPhotoButton = new UIImageView(photoContainer.Bounds);
        this.bottomBarContainer = new UIView(); 
        this.sendMessageButton = new UIButton();
        this.stickerViewButton = new UIButton();
        this.attachMediaButton = new UIButton();
        this.messageBoxField = new UITextView();
        this.messageBoxPlaceholderView = new UILabel();
        this.tableView = new UITableView();

        this.chatPhotoButton.ApplyRoundedAppearance();
        this.sendMessageButton.ApplyImageAppearance();
        this.stickerViewButton.ApplyImageAppearance();
        this.attachMediaButton.ApplyImageAppearance();
        this.messageBoxField.ApplyDefaultAppearance();
        this.messageBoxPlaceholderView.ApplyCaption2Appearance();
        this.tableView.ApplyDefaultAppearance();

        photoContainer.AddSubview(this.chatPhotoButton);
        this.messageBoxField.AddSubview(this.messageBoxPlaceholderView);
        this.bottomBarContainer.AddSubview(this.sendMessageButton);
        this.bottomBarContainer.AddSubview(this.stickerViewButton);
        this.bottomBarContainer.AddSubview(this.attachMediaButton);
        this.bottomBarContainer.AddSubview(this.messageBoxField);
        this.View!.AddSubview(this.bottomBarContainer);
        this.View.AddSubview(this.tableView);

        this.NavigationItem.RightBarButtonItem = photoItem;
        this.NavigationItem.LargeTitleDisplayMode = UINavigationItemLargeTitleDisplayMode.Never;
        this.View!.BackgroundColor = Colors.Foreground;
        this.bottomBarContainer!.BackgroundColor = Colors.Foreground;
        this.sendMessageButton!.SetImage(UIImage.FromBundle("send_28"), UIControlState.Normal);
        this.stickerViewButton!.SetImage(UIImage.FromBundle("smile_outline_28"), UIControlState.Normal);
        this.attachMediaButton!.SetImage(UIImage.FromBundle("add_circle_outline_28"), UIControlState.Normal);
        this.messageBoxPlaceholderView.Text = Localization.Instance.GetString("common_message_input_placeholder");

        this.messageBoxField.Changed += (sender, e) => {
            this.messageBoxPlaceholderView.Hidden = this.messageBoxField!.Text?.Length > 0;
            this.View!.SetNeedsLayout();
        };
        NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillShowNotification, KeyboardWillShow);
        NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, KeyboardWillHide);
        Initialize();
    }

    public override void ViewDidLayoutSubviews() {
        base.ViewDidLayoutSubviews();
        
        var messageBoxMaxHeight = 180;
        var messageBoxPivotX = 24 + Dimensions.ButtonIconSize * 2;
        var messageBoxWidth = this.View!.Frame.Width - messageBoxPivotX - 16 - Dimensions.ButtonIconSize;
        var messageBoxRequestedHeight = this.messageBoxField!.SizeThatFits(new CGSize(messageBoxWidth, 100)).Height;
        var messageBoxHeight = messageBoxRequestedHeight < messageBoxMaxHeight ? messageBoxRequestedHeight : messageBoxMaxHeight;
        
        var toolbarHeight = messageBoxHeight + 16;
        var safeAreaBottomInset = (keyboardFrameHeight == 0) ? this.View!.SafeAreaInsets.Bottom : 0;
        var bottomBarPivotY = this.View.Frame.Height - toolbarHeight - safeAreaBottomInset - keyboardFrameHeight;
        var buttonPivotY = toolbarHeight - Dimensions.ButtonIconSize - (toolbarHeight - messageBoxHeight) / 2;
        var messageBoxPivotY = (toolbarHeight - messageBoxHeight) / 2;

        this.bottomBarContainer!.Frame = new CGRect(0, bottomBarPivotY, this.View.Frame.Width, toolbarHeight);
        this.attachMediaButton!.Frame = new CGRect(8, buttonPivotY, Dimensions.ButtonIconSize, Dimensions.ButtonIconSize);
        this.stickerViewButton!.Frame = new CGRect(16 + Dimensions.ButtonIconSize, buttonPivotY, Dimensions.ButtonIconSize, Dimensions.ButtonIconSize);
        this.sendMessageButton!.Frame = new CGRect(this.bottomBarContainer.Frame.Width - 8 - Dimensions.ButtonIconSize, buttonPivotY, Dimensions.ButtonIconSize, Dimensions.ButtonIconSize);
        this.messageBoxField.Frame = new CGRect(messageBoxPivotX, messageBoxPivotY, messageBoxWidth, messageBoxHeight);
        this.messageBoxPlaceholderView!.Frame = new CGRect(12, 0, messageBoxWidth - 24, messageBoxHeight);
        this.tableView!.Frame = new CGRect(0, 0, this.View.Frame.Width, bottomBarPivotY);

        this.messageBoxField.ApplyRoundedAppearance();
    }

    private void KeyboardWillShow(NSNotification notification) {
        var keyboardFrame = UIKeyboard.FrameEndFromNotification(notification);
        keyboardFrameHeight = keyboardFrame.Height;
        UIView.Animate(0, 0, UIViewAnimationOptions.CurveEaseOut, () => this.View!.SetNeedsLayout(), null);
    }
    private void KeyboardWillHide(NSNotification notification) {
        keyboardFrameHeight = 0;
        UIView.Animate(0, 0, UIViewAnimationOptions.CurveEaseOut, () => this.View!.SetNeedsLayout(), null);
    }
}

