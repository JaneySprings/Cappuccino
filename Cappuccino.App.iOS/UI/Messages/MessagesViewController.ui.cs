namespace Cappuccino.App.iOS.UI.Messages;


public partial class MessagesViewController {
    private UIImageView? chatPhotoButton;
    private BottomMessagingPanel? bottomPanel;
    private UITableView? tableView;


    public override bool HidesBottomBarWhenPushed => true;
    public override bool CanBecomeFirstResponder => true;
    public override UIView InputAccessoryView => this.bottomPanel!;
    private string? TitleLabel { set => this.NavigationItem.Title = value; }


    
    public override void ViewDidLoad() {
        base.ViewDidLoad();
        
        var photoContainer = new UIView(new CGRect(0, 0, Dimensions.ButtonIconSize, Dimensions.ButtonIconSize));
        var photoItem = new UIBarButtonItem(photoContainer);
        this.chatPhotoButton = new UIImageView(photoContainer.Bounds);
        this.tableView = new UITableView();
        this.bottomPanel = new BottomMessagingPanel(this.View!.Bounds.Size);

        this.chatPhotoButton.ApplyRoundedAppearance();
        this.tableView.ApplyDefaultAppearance();

        photoContainer.AddSubview(this.chatPhotoButton);
        this.View!.AddSubview(this.tableView);

        this.NavigationItem.RightBarButtonItem = photoItem;
        this.View!.BackgroundColor = Colors.Foreground;
        this.NavigationItem.LargeTitleDisplayMode = UINavigationItemLargeTitleDisplayMode.Never;
        this.tableView.KeyboardDismissMode = UIScrollViewKeyboardDismissMode.Interactive;

        NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillShowNotification, KeyboardShowObserver);
        NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, KeyboardHideObserver);
        
        Initialize();
    }

    public override void ViewDidLayoutSubviews() {
        base.ViewDidLayoutSubviews();
        var tableViewHeight = this.View!.Frame.Height - this.View!.SafeAreaInsets.Bottom;
        this.tableView!.Frame = new CGRect(0, 0, this.View!.Frame.Width, tableViewHeight);
    }

    private void KeyboardShowObserver(NSNotification notification) {
        var keyboardFrame = UIKeyboard.FrameEndFromNotification(notification);
        this.tableView!.ContentInset = new UIEdgeInsets(0, 0, keyboardFrame.Height, 0);
    }
    private void KeyboardHideObserver(NSNotification notification) {
        var keyboardFrame = UIKeyboard.FrameEndFromNotification(notification);
        this.tableView!.ContentInset = new UIEdgeInsets(0, 0, this.bottomPanel!.Bounds.Height, 0);
    }


    private class BottomMessagingPanel: UIView {
        public UIButton? SendMessageButton { get; private set; }
        public UIButton? StickerViewButton { get; private set; }
        public UIButton? AttachMediaButton { get; private set; }
        public UITextView? MessageBoxField { get; private set; }
        private UILabel? messageBoxPlaceholderView;

        private nfloat minimumHeight;


        public BottomMessagingPanel(CGSize size) : base() {
            SendMessageButton = new UIButton();
            StickerViewButton = new UIButton();
            AttachMediaButton = new UIButton();
            MessageBoxField = new UITextView();
            messageBoxPlaceholderView = new UILabel();

            SendMessageButton.ApplyImageAppearance();
            StickerViewButton.ApplyImageAppearance();
            AttachMediaButton.ApplyImageAppearance();
            MessageBoxField.ApplyDefaultAppearance();
            messageBoxPlaceholderView.ApplyCaption2Appearance();

            AddSubview(SendMessageButton);
            AddSubview(StickerViewButton);
            AddSubview(AttachMediaButton);
            AddSubview(MessageBoxField);

            MessageBoxField.AddSubview(messageBoxPlaceholderView);

            BackgroundColor = Colors.Foreground;
            SendMessageButton.SetImage(UIImage.FromBundle("send_28"), UIControlState.Normal);
            StickerViewButton.SetImage(UIImage.FromBundle("smile_outline_28"), UIControlState.Normal);
            AttachMediaButton!.SetImage(UIImage.FromBundle("add_circle_outline_28"), UIControlState.Normal);
            messageBoxPlaceholderView.Text = Localization.Instance.GetString("common_message_input_placeholder");

            MessageBoxField.Changed += (sender, e) => {
                this.messageBoxPlaceholderView.Hidden = MessageBoxField!.Text?.Length > 0;
                var contentSize = SizeThatFits(this.Bounds.Size);
                var offset = minimumHeight - contentSize.Height;
                this.Frame = new CGRect(0, offset, contentSize.Width, this.Bounds.Height);
                SetNeedsLayout();
            };

            var requestedSize = SizeThatFits(size);
            this.Frame = new CGRect(0, 0, requestedSize.Width, requestedSize.Height);
        }

        public override CGSize SizeThatFits(CGSize size) {
            var messageBoxMaxHeight = 180;
            var messageBoxPivotX = 24 + Dimensions.ButtonIconSize * 2;
            var messageBoxWidth = size.Width - messageBoxPivotX - 16 - Dimensions.ButtonIconSize;
            var messageBoxRequestedHeight = MessageBoxField!.SizeThatFits(new CGSize(messageBoxWidth, 100)).Height;
            var messageBoxHeight = messageBoxRequestedHeight < messageBoxMaxHeight ? messageBoxRequestedHeight : messageBoxMaxHeight;
            var panelHeight = messageBoxHeight + 16;

            return new CGSize(size.Width, panelHeight);
        }

        public override void LayoutSubviews() {
            base.LayoutSubviews();

            var messageBoxMaxHeight = 180;
            var messageBoxPivotX = 24 + Dimensions.ButtonIconSize * 2;
            var messageBoxWidth = this.Bounds.Width - messageBoxPivotX - 16 - Dimensions.ButtonIconSize;
            var messageBoxRequestedHeight = MessageBoxField!.SizeThatFits(new CGSize(messageBoxWidth, 100)).Height;
            var messageBoxHeight = messageBoxRequestedHeight < messageBoxMaxHeight ? messageBoxRequestedHeight : messageBoxMaxHeight;
            
            var panelHeight = messageBoxHeight + 16;
            var buttonPivotY = panelHeight - Dimensions.ButtonIconSize - (panelHeight - messageBoxHeight) / 2;
            var messageBoxPivotY = (panelHeight - messageBoxHeight) / 2;

            if (minimumHeight == 0)
                minimumHeight = panelHeight;

            AttachMediaButton!.Frame = new CGRect(8, buttonPivotY, Dimensions.ButtonIconSize, Dimensions.ButtonIconSize);
            StickerViewButton!.Frame = new CGRect(16 + Dimensions.ButtonIconSize, buttonPivotY, Dimensions.ButtonIconSize, Dimensions.ButtonIconSize);
            SendMessageButton!.Frame = new CGRect(this.Bounds.Width - 8 - Dimensions.ButtonIconSize, buttonPivotY, Dimensions.ButtonIconSize, Dimensions.ButtonIconSize);
            MessageBoxField.Frame = new CGRect(messageBoxPivotX, messageBoxPivotY, messageBoxWidth, messageBoxHeight);
            this.messageBoxPlaceholderView!.Frame = new CGRect(14, 0, messageBoxWidth - 28, messageBoxHeight);
          
            MessageBoxField.ApplyRoundedAppearance();
        }


        // StackOverflow hack for safe area insets
        public override void MovedToWindow() {
            base.MovedToWindow();
            if(this.Window != null) {
                this.BottomAnchor.ConstraintLessThanOrEqualToSystemSpacingBelowAnchor(this.Window!.SafeAreaLayoutGuide.BottomAnchor, 1).Active = true;
            }
        }
    }
}

