using System.Reflection;
using Cappuccino.Core.Network.Auth;

namespace Cappuccino.Core.Network.Tests;

public static class TokenFactory {

    public static AccessToken ValidUnlimited => new(HashFromAssemblyName(), -1, 0);
    public static AccessToken ValidExpires => new(HashFromAssemblyName(), DateTimeOffset.Now.ToUnixTimeSeconds()+10000, 0);
    public static AccessToken InvalidExpired => new(HashFromAssemblyName(), DateTimeOffset.Now.ToUnixTimeSeconds()-10000, 0);
    public static AccessToken Invalid => new(string.Empty, int.MinValue, int.MinValue);


    public static AccessToken ValidUnlimitedTokenForUser(int id) => new(HashFromAssemblyName(), -1, id);

    public static string HashFromAssemblyName() {
        return Assembly.GetExecutingAssembly().GetName().Name!.GetHashCode().ToString();
    }
}
