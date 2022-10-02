namespace Cappuccino.App.iOS.UI.Contacts;

public class FilterDataObject {
    public int SearchOrder { get; set; }
    public string? HomeTown { get; set; }


    public void Reset() {
        SearchOrder = 0;
        HomeTown = string.Empty;
    }
}