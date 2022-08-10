using System.Collections.Generic;
using System.Linq;
using Cappuccino.App.Android.UI;
using Cappuccino.Core.Network.Models;
using Cappuccino.Core.Network.Models.Response;

namespace Cappuccino.App.Android.Extensions {
    public static class ChatItemMapper {
        public static List<ChatItem> ToChatItems(this ResponseEx<Chat> response) {
            return response.Items!.Select(chat => new ChatItem(chat, response.Profiles!, response.Groups!)).ToList();
        }
    }
}