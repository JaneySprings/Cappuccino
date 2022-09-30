using Cappuccino.App.iOS.UI.Common;

namespace Cappuccino.App.iOS.UI.Contacts;

public partial class FilterModalController {
    private UIButton? closeButton;
    private UISegmentedControl? searchOrder;
    private UISegmentedControl? searchSource;

    public override void ViewDidLoad() {
        base.ViewDidLoad();

        this.closeButton = new UIButton();
        this.searchOrder = new UISegmentedControl();
        this.searchSource = new UISegmentedControl();

        this.View!.BackgroundColor = Colors.Foreground;

        var header = new UILabel();
        header.ApplyHeaderAppearance();
        header.Text = Localization.Instance.GetString("title_page_filters");

        var headerSize = header.SizeThatFits(this.View.Bounds.Size);
        header.Frame = new CGRect((this.View.Bounds.Width - headerSize.Width)/2, 16, headerSize.Width, headerSize.Height);

        this.closeButton.SetTitle(Localization.Instance.GetString("common_close_action"), UIControlState.Normal);
        this.closeButton.SetTitleColor(Colors.Accent, UIControlState.Normal);
        var closeButtonSize = this.closeButton.SizeThatFits(this.View.Bounds.Size);
        this.closeButton.Frame = new CGRect(this.View.Bounds.Width - closeButtonSize.Width - 16, 16, closeButtonSize.Width, closeButtonSize.Height);

        this.View.AddSubview(header);
        this.View.AddSubview(this.closeButton);

        var stackOrigin = header.Frame.Height + 24;
        var measuredSize = CGSize.Empty;

        measuredSize = InterfaceUtility.LayoutSegmentedSection(this.searchOrder, this.View, new SegmentedControlOptions {
            Title = Localization.Instance.GetString("filters_search_order"),
            Options = new[] { 
                Localization.Instance.GetString("filters_search_order_option_registration_date")!, 
                Localization.Instance.GetString("filters_search_order_option_popularity")!
            },
            OriginY = stackOrigin,
            HorizontalPadding = 16,
            VerticalPadding = 16,
            Spacing = 8,
            ContentSize = this.View.Bounds.Size,
            HeightAmplifier = 2
        });

        stackOrigin += measuredSize.Height;

        InitializeDefaults();
    }
}