using System;

namespace Cappuccino.App.Android.Utils {
    public class DateTimeUtils {
        public static string ParseShortDate(int unix) {
            long offset = DateTimeOffset.Now.ToUnixTimeSeconds() - unix;

            if (offset < 24 * 60 * 60)
                return DateTimeOffset.FromUnixTimeSeconds(unix).ToString("HH:mm");
            if (offset < 365*24*60*60)
                return DateTimeOffset.FromUnixTimeSeconds(unix).ToString("d MMM");
            return DateTimeOffset.FromUnixTimeSeconds(unix).ToString("MMM yyyy");
        }
    }
}