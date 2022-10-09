namespace Cappuccino.App.iOS;

public static class Constants {
    public const int ChatIdOffset = 2000000000;
    public static IEnumerable<string> DefaultUserFields => new[] { "last_seen", "online", "photo_100", "photo_200" };
}