namespace Cappuccino.App.iOS.UI.Contacts;

public class FilterDataObject {
    public int SearchOrder { get; set; }
    public string? HomeTown { get; set; }
    public int BirthDay { get; set; }
    public int BirthMonth { get; set; } 
    public int BirthYear { get; set; }
    public int AgeFrom { get; set; }
    public int AgeTo { get; set; }

    public void Reset() {
        SearchOrder = 0;
        HomeTown = string.Empty;
        BirthDay = 0;
        BirthMonth = 0;
        BirthYear = 0;
        AgeFrom = 0;
        AgeTo = 100;
    }
}