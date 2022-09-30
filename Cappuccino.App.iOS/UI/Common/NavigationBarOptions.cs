namespace Cappuccino.App.iOS.UI.Common;

public class NavigationBarOptions {
    public string? Title { get; set; }
    public IEnumerable<BarButtonItemOptions>? Actions { get; set; }
    public bool Search { get; set; }
    public EventHandler<UISearchBarTextChangedEventArgs>? SearchTextChanged { get; set; }
    public EventHandler? SearchCancelled { get; set; }
    public BarButtonItemOptions? SearchAction { get; set; }

}