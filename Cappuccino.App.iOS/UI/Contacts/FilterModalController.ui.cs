namespace Cappuccino.App.iOS.UI.Contacts;


public partial class FilterModalController {
    private UIButton? close;
    private UIButton? clear;
    private UIButton? show;
    private UILabel? title;
    private UILabel? locationTitle;
    private UILabel? orderTitle;
    private UITextField? location;
    private UISegmentedControl? order;

    public override void ViewDidLoad() {
        base.ViewDidLoad();
        this.View!.BackgroundColor = Colors.Foreground;

        // Header
        this.close = new UIButton().ApplyDefaultAppearance();
        this.clear = new UIButton().ApplyDefaultAppearance();
        this.title = new UILabel { Text = Localization.Instance.GetString("title_page_filters") };
        this.locationTitle = new UILabel { Text = Localization.Instance.GetString("filters_location") };
        this.orderTitle = new UILabel { Text = Localization.Instance.GetString("filters_search_order") };
        this.order = new UISegmentedControl(new[] { 
            Localization.Instance.GetString("filters_search_order_option_popularity"),
            Localization.Instance.GetString("filters_search_order_option_registration_date")
        });
        this.location = new UITextField { 
            Placeholder = Localization.Instance.GetString("filters_location_placeholder")
        };
        this.show = new UIButton();

        this.title.ApplyHeaderAppearance();
        this.orderTitle.ApplyCaption2Appearance();
        this.locationTitle.ApplyCaption2Appearance();
        this.order.ApplyDefaultAppearance();
        this.location.ApplyDefaultAppearance();
        this.show.ApplyActionAppearance();

        this.View.AddSubview(this.title);
        this.View.AddSubview(this.clear);
        this.View.AddSubview(this.close);
        this.View.AddSubview(this.orderTitle);
        this.View.AddSubview(this.order);
        this.View.AddSubview(this.locationTitle);
        this.View.AddSubview(this.location);        
        this.View.AddSubview(this.show);

        InitializeDefaults();
    }

    public override void ViewDidLayoutSubviews() {
        base.ViewDidLayoutSubviews();
        var titleSize = this.title!.SizeThatFits(this.View!.Bounds.Size);
        this.title.Frame = new CGRect((this.View.Bounds.Width - titleSize.Width)/2, 16, titleSize.Width, titleSize.Height);

        this.close!.SetTitle(Localization.Instance.GetString("common_action_close"), UIControlState.Normal);
        this.clear!.SetTitle(Localization.Instance.GetString("common_action_reset"), UIControlState.Normal);

        var closeButtonSize = this.close.SizeThatFits(this.View.Bounds.Size);
        this.close.Frame = new CGRect(this.View.Bounds.Width - closeButtonSize.Width - 16, 16, closeButtonSize.Width, closeButtonSize.Height);

        var clearButtonSize = this.clear.SizeThatFits(this.View.Bounds.Size);
        this.clear.Frame = new CGRect(16, 16, closeButtonSize.Width, closeButtonSize.Height);

        // Content
        var labelSpacing = 8;
        var sectionSpacing = 20;
        var labelHeight = 24;

        this.orderTitle!.Frame = new CGRect(16, titleSize.Height + 36, this.View.Bounds.Width - 32, labelHeight);
        this.order!.Frame = new CGRect(16, this.orderTitle.Frame.Bottom + labelSpacing, this.View.Bounds.Width - 32, Dimensions.EditorsHeight);
        
        this.locationTitle!.Frame = new CGRect(16, this.order.Frame.Bottom + sectionSpacing, this.View.Bounds.Width - 32, labelHeight);
        this.location!.Frame = new CGRect(16, this.locationTitle.Frame.Bottom + labelSpacing, this.View.Bounds.Width - 32, Dimensions.EditorsHeight);
        

        this.show!.Frame = new CGRect(16, this.View.Bounds.Height - Dimensions.EditorsHeight - this.View.SafeAreaInsets.Bottom - 16, this.View.Bounds.Width - 32, Dimensions.EditorsHeight);
        this.show.SetTitle(Localization.Instance.GetString("common_action_show_results"), UIControlState.Normal);
        this.show.ApplyActionAppearance();
    }
}