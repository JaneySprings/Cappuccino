namespace Cappuccino.App.iOS.Extensions;


public static class RequestConstants {
    public static IEnumerable<string> UserDefaults() => new[] { 
        "last_seen", "online", "photo_100", "photo_200" 
    };
}
