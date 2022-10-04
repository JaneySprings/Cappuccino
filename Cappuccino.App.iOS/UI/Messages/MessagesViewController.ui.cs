namespace Cappuccino.App.iOS.UI.Messages;


public partial class MessagesViewController {
    private UILabel? title;
    private UIImageView? photo;


    public override void ViewDidLoad() {
        base.ViewDidLoad();
        this.View!.BackgroundColor = Colors.Background;
        NavigationItem.LargeTitleDisplayMode = UINavigationItemLargeTitleDisplayMode.Never;

        var photoSize = 36;

        this.title = new UILabel().ApplyCaption1Appearance();
        this.photo = new UIImageView(new CGRect(0, 0, photoSize, photoSize));
        this.photo.ApplyRoundedAppearance();

        var photoContainer = new UIView(new CGRect(0, 0, photoSize, photoSize));
        photoContainer.AddSubview(this.photo);
        var photoItem = new UIBarButtonItem(photoContainer);
        this.NavigationItem.RightBarButtonItem = photoItem;
        this.NavigationItem.TitleView = this.title;

        Initialize();
    }

    public override void ViewDidLayoutSubviews() {
        base.ViewDidLayoutSubviews();
        
    }
}

