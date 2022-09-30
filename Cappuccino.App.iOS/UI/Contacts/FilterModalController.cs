namespace Cappuccino.App.iOS.UI.Contacts;


public partial class FilterModalController: UIViewController {
    private FilterDataObject dataObject;


    public FilterModalController(FilterDataObject dataObject) {
        this.dataObject = dataObject;
    }

    private void InitializeDefaults() {
        this.searchOrder!.SelectedSegment = new IntPtr(this.dataObject.SearchOrder);
    }

    public override void ViewDidAppear(bool animated) {
        base.ViewDidAppear(animated);

        this.closeButton!.TouchUpInside += (s, e) => this.DismissViewController(true, null);
        this.searchOrder!.ValueChanged += (s, e) => this.dataObject.SearchOrder = this.searchOrder!.SelectedSegment.ToInt32();
        
    }

}