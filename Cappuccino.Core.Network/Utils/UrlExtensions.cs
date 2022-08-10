using System;
using System.Collections.Generic;
using System.Linq;

namespace Cappuccino.Core.Network.Utils {
    internal static class UrlExtensions {
        public static string JoinToUrl(this Dictionary<string, string> dictionary) {
            return String.Join('&', dictionary.Select(pair => $"{pair.Key}={pair.Value}"));
        }

        public static Dictionary<string, string> SplitUrl(this string url) {
            string[] tokens = url[(url.IndexOf('#') + 1)..].Split('&');
            Dictionary<string, string> param = new();

            foreach (string token in tokens) {
                string[] pair = token.Split('=');

                if (pair.Length == 2)
                    param.Add(pair[0], pair[1]);
            }
            return param;
        }
    }
}

