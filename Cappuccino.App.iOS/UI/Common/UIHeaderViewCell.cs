using System;
using Foundation;
using UIKit;
using CoreGraphics;


namespace Cappuccino.App.iOS.UI.Common;

public partial class HeaderViewCell: UITableViewHeaderFooterView {
    private UILabel? header;
    private UIView? divider;


	public HeaderViewCell(IntPtr handle) : base(handle) { Initialize(); }
    private void Initialize() {
        this.ContentView.Frame = new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, 50);

        this.divider = new UIView { BackgroundColor = UIColor.FromName("divider") };
        this.header = new UILabel();
        this.header.ApplyHeader2Appearance();

        this.ContentView.AddSubview(divider);
        this.ContentView.AddSubview(this.header);
    }

    public void Bind(string item) => this.header!.Text = item;

    public override void LayoutSubviews() {
        base.LayoutSubviews();

        this.header!.Frame = new CGRect(24, 0, this.ContentView.Frame.Width - 24, this.ContentView.Frame.Height);
        this.divider!.Frame = new CGRect(16, 0, this.ContentView.Frame.Width - 32, 1);
    }
}

