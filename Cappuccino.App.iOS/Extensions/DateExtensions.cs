﻿namespace Cappuccino.App.iOS.Extensions;


public static class DateExtensions {
    public static string ParseDate(this int unix) {
        long current = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
        long offset = current - unix;

        if (offset < 24 * 60 * 60) {
            int cur_hours = Int32.Parse(DateTimeOffset.FromUnixTimeSeconds(current).ToLocalTime().ToString("HH"));
            int last_hours = Int32.Parse(DateTimeOffset.FromUnixTimeSeconds(unix).ToLocalTime().ToString("HH"));
            string result = DateTimeOffset.FromUnixTimeSeconds(unix).ToLocalTime().ToString("HH:mm");

            if (cur_hours >= last_hours)
                return $"{Localization.Instance.GetString("common_abbr_today")}, {result}";
            return $"{Localization.Instance.GetString("common_abbr_yesterday")}, {result}";
        }
        if (offset < 365 * 24 * 60 * 60)
            return DateTimeOffset.FromUnixTimeSeconds(unix).ToString("d MMM");
        return DateTimeOffset.FromUnixTimeSeconds(unix).ToString("MMM yyyy");
    }

    public static string ParseShortDate(this int unix) {
        long current = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
        long offset = current - unix;

        if (offset < 24 * 60 * 60) {
            string result = DateTimeOffset.FromUnixTimeSeconds(unix).ToLocalTime().ToString("HH:mm");

            return result;
        }
        if (offset < 365 * 24 * 60 * 60)
            return DateTimeOffset.FromUnixTimeSeconds(unix).ToString("d MMM");
        return DateTimeOffset.FromUnixTimeSeconds(unix).ToString("MMM yyyy");
    }
}

