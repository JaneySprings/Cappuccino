namespace Cappuccino.App.iOS.UI.Contacts;


public partial class FilterModalController {
    private UIButton? close;
    private UIButton? clear;
    private UIButton? show;
    private UITextField? location;
    private UISegmentedControl? order;

    public override void ViewDidLoad() {
        base.ViewDidLoad();
        this.View!.BackgroundColor = Colors.Foreground;

        // Header
        this.close = new UIButton().ApplyDefaultAppearance();
        this.clear = new UIButton().ApplyDefaultAppearance();
        var header = new UILabel { Text = Localization.Instance.GetString("title_page_filters") };
        header.ApplyHeaderAppearance();

        var headerSize = header.SizeThatFits(this.View.Bounds.Size);
        header.Frame = new CGRect((this.View.Bounds.Width - headerSize.Width)/2, 16, headerSize.Width, headerSize.Height);

        this.close.SetTitle(Localization.Instance.GetString("common_action_close"), UIControlState.Normal);
        this.clear.SetTitle(Localization.Instance.GetString("common_action_reset"), UIControlState.Normal);

        var closeButtonSize = this.close.SizeThatFits(this.View.Bounds.Size);
        this.close.Frame = new CGRect(this.View.Bounds.Width - closeButtonSize.Width - 16, 16, closeButtonSize.Width, closeButtonSize.Height);

        var clearButtonSize = this.clear.SizeThatFits(this.View.Bounds.Size);
        this.clear.Frame = new CGRect(16, 16, closeButtonSize.Width, closeButtonSize.Height);

        this.View.AddSubview(header);
        this.View.AddSubview(this.clear);
        this.View.AddSubview(this.close);

        // Content
        var labelSpacing = 8;
        var sectionSpacing = 20;
        var labelHeight = 24;


        var orderLabel = new UILabel { 
            Text = Localization.Instance.GetString("filters_search_order"),
            Frame = new CGRect(16, headerSize.Height + 36, this.View.Bounds.Width - 32, labelHeight)
        };
        orderLabel.ApplyCaption2Appearance();
        this.View.AddSubview(orderLabel);

        this.order = new UISegmentedControl(new[] { 
            Localization.Instance.GetString("filters_search_order_option_popularity"),
            Localization.Instance.GetString("filters_search_order_option_registration_date")
        });
        this.order.Frame = new CGRect(16, orderLabel.Frame.Bottom + labelSpacing, this.View.Bounds.Width - 32, Dimensions.EditorsHeight);
        this.order.ApplyDefaultAppearance();
        this.View.AddSubview(this.order);


        var locationLabel = new UILabel { 
            Text = Localization.Instance.GetString("filters_location"),
            Frame = new CGRect(16, this.order.Frame.Bottom + sectionSpacing, this.View.Bounds.Width - 32, labelHeight)
        };
        locationLabel.ApplyCaption2Appearance();
        this.View.AddSubview(locationLabel);

        this.location = new UITextField { 
            Placeholder = Localization.Instance.GetString("filters_location_placeholder"),
            Frame = new CGRect(16, locationLabel.Frame.Bottom + labelSpacing, this.View.Bounds.Width - 32, Dimensions.EditorsHeight)
        };
        this.location.ApplyDefaultAppearance();
        this.View.AddSubview(this.location);


        this.show = new UIButton {
            Frame = new CGRect(16, this.location.Frame.Bottom + 2*sectionSpacing, this.View.Bounds.Width - 32, Dimensions.EditorsHeight)
        };
        this.show.ApplyActionAppearance();
        this.show.SetTitle(Localization.Instance.GetString("common_action_show_results"), UIControlState.Normal);
        this.View.AddSubview(this.show);


        InitializeDefaults();
    }
}