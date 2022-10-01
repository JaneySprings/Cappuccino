namespace Cappuccino.App.iOS.UI.Contacts;


public partial class FilterModalController: UIViewController {
    private FilterDataObject dataObject;
    private Action? finishAction;


    public FilterModalController(FilterDataObject dataObject, Action callback) {
        this.dataObject = dataObject;
        this.finishAction = callback;
    }

    private void InitializeDefaults() {
        this.order!.SelectedSegment = new IntPtr(this.dataObject.SearchOrder);
        this.location!.Text = this.dataObject.HomeTown;
    }

    public override void ViewDidAppear(bool animated) {
        base.ViewDidAppear(animated);

        this.close!.TouchUpInside += (s, e) => this.DismissViewController(true, null);
        this.clear!.TouchUpInside += (s, e) => {
            this.dataObject.Reset();
            InitializeDefaults();
        };
        this.show!.TouchUpInside += (s, e) => {
            this.dataObject.SearchOrder = this.order!.SelectedSegment.ToInt32();
            this.dataObject.HomeTown = this.location!.Text;

            this.finishAction?.Invoke();
            this.DismissViewController(true, null);
        };
    }
}