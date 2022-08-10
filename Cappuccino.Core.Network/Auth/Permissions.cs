using System.Collections.Generic;
using System.Linq;

namespace Cappuccino.Core.Network.Auth {

    /* Mark: documentation[https://vk.com/dev/permissions] */
    public static class Permissions {
        public const int Friends = 2;
        public const int Photos = 4;
        public const int Audio = 8;
        public const int Video = 16;
        public const int Stories = 64;
        public const int Pages = 128;
        public const int Status = 1024;
        public const int Notes = 2048;
        public const int Messages = 4096;
        public const int Wall = 8192;
        public const int Offline = 65536;
        public const int Docs = 131072;
        public const int Groups =  262144;
        public const int Notifications = 52428;
        
        public static int ToPermissionsMask(this IEnumerable<int> permissions) {
            return permissions.Sum();
        }
    }

}