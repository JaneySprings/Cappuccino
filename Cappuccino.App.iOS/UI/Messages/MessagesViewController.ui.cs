using Cappuccino.App.iOS.Extensions;

namespace Cappuccino.App.iOS.UI.Messages;


public partial class MessagesViewController {
    private UIImageView? chatPhotoButton;
    private BottomMessagingPanel? bottomPanel;
    private UITableView? tableView;
    private UIView? contentView;


    public override bool HidesBottomBarWhenPushed => true;
    private string? TitleLabel { set => this.NavigationItem.Title = value; }

    
    public override void ViewDidLoad() {
        base.ViewDidLoad();
        
        var photoContainer = new UIView(new CGRect(0, 0, Dimensions.ButtonIconSize, Dimensions.ButtonIconSize));
        var photoItem = new UIBarButtonItem(photoContainer);
        this.chatPhotoButton = new UIImageView(photoContainer.Bounds);
        this.tableView = new UITableView();
        this.bottomPanel = new BottomMessagingPanel();
        this.contentView = new UIView(this.View!.Frame);

        this.chatPhotoButton.ApplyRoundedAppearance();
        this.tableView.ApplyDefaultAppearance();

        photoContainer.AddSubview(this.chatPhotoButton);
        this.contentView.AddSubview(this.tableView);
        this.contentView.AddSubview(this.bottomPanel);
        this.View.AddSubview(this.contentView);

        this.NavigationItem.RightBarButtonItem = photoItem;
        this.View!.BackgroundColor = Colors.Foreground;
        this.NavigationItem.LargeTitleDisplayMode = UINavigationItemLargeTitleDisplayMode.Never;

        NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillShowNotification, KeyboardShowObserver);
        NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, KeyboardHideObserver);

        Initialize();
    }

    public override void ViewDidLayoutSubviews() {
        base.ViewDidLayoutSubviews();
        var bottomPanelSize = this.bottomPanel!.SizeThatFits(this.View!.Bounds.Size);
        var bottomPanelPivotY = this.View!.Bounds.Height - bottomPanelSize.Height - this.View!.SafeAreaInsets.Bottom;

        this.bottomPanel.Frame = new CGRect(0, bottomPanelPivotY, bottomPanelSize.Width, bottomPanelSize.Height);
        this.tableView!.Frame = new CGRect(0, 0, this.View!.Frame.Width, bottomPanelPivotY);
    }

    private void KeyboardShowObserver(NSNotification notification) {
        var keyboardAnimationDuration = UIKeyboard.AnimationDurationFromNotification(notification);
        var keyboardHeight = UIKeyboard.FrameEndFromNotification(notification).Height - this.View!.SafeAreaInsets.Bottom;

        UIView.Animate(keyboardAnimationDuration, () => 
            this.contentView!.Frame = new CGRect(0, -keyboardHeight, this.contentView!.Frame.Width, this.contentView!.Frame.Height)
        );
    }

    private void KeyboardHideObserver(NSNotification notification) {
        var keyboardAnimationDuration = UIKeyboard.AnimationDurationFromNotification(notification);
        var keyboardFrame = CGRect.Empty;

        UIView.Animate(keyboardAnimationDuration, () => 
            this.contentView!.Frame = new CGRect(0, 0, this.contentView!.Frame.Width, this.contentView!.Frame.Height)
        );
    }


    private class BottomMessagingPanel: UIView {
        public UIButton? SendMessageButton { get; }
        public UIButton? StickerViewButton { get; }
        public UIButton? AttachMediaButton { get; }
        public UITextView? MessageBoxField { get; }

        private readonly UILabel? messageBoxPlaceholderView;
        private readonly nfloat maximumMessageBoxHeight = 160;


        public BottomMessagingPanel() {
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
                SetNeedsLayout();
            };
        }

        public override CGSize SizeThatFits(CGSize size) {
            var messageBoxPivotX = 24 + (Dimensions.ButtonIconSize * 2);
            var messageBoxWidth = size.Width - messageBoxPivotX - 16 - Dimensions.ButtonIconSize;
            var messageBoxRequestedHeight = MessageBoxField!.SizeThatFits(new CGSize(messageBoxWidth, 100)).Height;
            var messageBoxHeight = (messageBoxRequestedHeight < maximumMessageBoxHeight) ? messageBoxRequestedHeight : maximumMessageBoxHeight;
            var panelHeight = messageBoxHeight + 16;

            return new CGSize(size.Width, panelHeight);
        }

        public override void LayoutSubviews() {
            base.LayoutSubviews();

            var messageBoxHeightOld = this.MessageBoxField!.Frame.Height;
            var messageBoxPivotX = 24 + (Dimensions.ButtonIconSize * 2);
            var messageBoxWidth = this.Bounds.Width - messageBoxPivotX - 16 - Dimensions.ButtonIconSize;
            var messageBoxRequestedHeight = MessageBoxField!.SizeThatFits(new CGSize(messageBoxWidth, 100)).Height;
            var messageBoxHeight = (messageBoxRequestedHeight < maximumMessageBoxHeight) ? messageBoxRequestedHeight : maximumMessageBoxHeight;
            
            var panelHeight = messageBoxHeight + 16;
            var buttonPivotY = panelHeight - Dimensions.ButtonIconSize - ((panelHeight - messageBoxHeight) / 2);
            var messageBoxPivotY = (panelHeight - messageBoxHeight) / 2;

            AttachMediaButton!.Frame = new CGRect(8, buttonPivotY, Dimensions.ButtonIconSize, Dimensions.ButtonIconSize);
            StickerViewButton!.Frame = new CGRect(16 + Dimensions.ButtonIconSize, buttonPivotY, Dimensions.ButtonIconSize, Dimensions.ButtonIconSize);
            SendMessageButton!.Frame = new CGRect(this.Bounds.Width - 8 - Dimensions.ButtonIconSize, buttonPivotY, Dimensions.ButtonIconSize, Dimensions.ButtonIconSize);
            MessageBoxField.Frame = new CGRect(messageBoxPivotX, messageBoxPivotY, messageBoxWidth, messageBoxHeight);
            this.messageBoxPlaceholderView!.Frame = new CGRect(14, 0, messageBoxWidth - 28, messageBoxHeight);
          
            if (messageBoxHeightOld != 0 && messageBoxHeightOld != messageBoxHeight) 
                this.Superview.Superview.SetNeedsLayout();

            MessageBoxField.ApplyRoundedAppearance();
        }
    }
}

