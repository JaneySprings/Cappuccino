namespace Cappuccino.App.iOS.UI.Common;

public class LinearLayout: UIView {
    public nfloat Spacing { get; set; }
    private nfloat pivot;

    public LinearLayout() {
        this.Spacing = 0;
        this.pivot = 0;
    }

    public void AddView(UIView view, int extraSpacing = 0, int extraHeight = 0) {
        var viewHeight = view.SizeThatFits(this.Bounds.Size).Height + extraHeight;
        view.Frame = new CGRect(0, this.pivot, this.Bounds.Width, viewHeight);
        this.pivot += viewHeight + Spacing + extraSpacing;

        this.AddSubview(view);
    }
}