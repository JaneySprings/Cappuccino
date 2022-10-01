using Cappuccino.App.iOS.UI.Common;

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

        this.close = new UIButton().ApplyDefaultAppearance();;
        this.clear = new UIButton().ApplyDefaultAppearance();;
        this.show = new UIButton().ApplyActionAppearance();;
        this.location = new UITextField().ApplyDefaultAppearance();;
        var layout = new LinearLayout { Spacing = 6 };
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

        layout.Frame = new CGRect(16, header.Frame.Height + 48, this.View.Bounds.Width - 32, this.View.Bounds.Height - header.Frame.Height - 48);

        this.View.AddSubview(header);
        this.View.AddSubview(this.clear);
        this.View.AddSubview(this.close);
        this.View.AddSubview(layout);

        // Order
        var orderLabel = new UILabel { Text = Localization.Instance.GetString("filters_search_order") };
        orderLabel.ApplyCaption2Appearance();
        layout.AddView(orderLabel);

        this.order = new UISegmentedControl(new[] { 
            Localization.Instance.GetString("filters_search_order_option_popularity"),
            Localization.Instance.GetString("filters_search_order_option_registration_date")
        });
        this.order.ApplyDefaultAppearance();
        layout.AddView(order, 16, 8);

        // Location
        var locationLabel = new UILabel { Text = Localization.Instance.GetString("filters_location") };
        locationLabel.ApplyCaption2Appearance();
        layout.AddView(locationLabel);

        this.location = new UITextField { Placeholder = Localization.Instance.GetString("filters_location_placeholder") };
        this.location.ApplyDefaultAppearance();
        layout.AddView(this.location, 32, 24);

        // Apply
        this.show.SetTitle(Localization.Instance.GetString("common_action_show_results"), UIControlState.Normal);
        layout.AddView(this.show, 0, 8);


        InitializeDefaults();
    }
}