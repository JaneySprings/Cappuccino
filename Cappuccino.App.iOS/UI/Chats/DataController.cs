using Cappuccino.Core.Network.Models;
using Cappuccino.Core.Network.Polling;
using System.Text.Json;

namespace Cappuccino.App.iOS.UI.Chats;


public class DataController {
    public Action<List<int>>? EventDeleteChats { get; set; }
    public Action<List<int>>? EventUpdateChats { get; set; }

    public void ProcessUpdates(LongPollResponse response) {
        var updatedChatIds = new List<int>();
        var deletedChatIds = new List<int>();

        if (response.Updates == null)
            return;

        foreach (var update in response.Updates) {
            var eventId = ((JsonElement)update[0]).GetInt32();

            if (eventId == Events.NewMessage)
                updatedChatIds.Add(((JsonElement)update[4]).GetInt32());

            if (eventId == Events.EditMessages || eventId == Events.DeleteMessages)
                updatedChatIds.Add(((JsonElement)update[3]).GetInt32());

            if (eventId == Events.ReadMessageInConversation)
                updatedChatIds.Add(((JsonElement)update[1]).GetInt32());

            if (eventId == Events.DeleteConversation)
                deletedChatIds.Add(((JsonElement)update[1]).GetInt32());
        }

        if (updatedChatIds.Count > 0)
            EventUpdateChats?.Invoke(updatedChatIds);

        if (deletedChatIds.Count > 0)
            EventDeleteChats?.Invoke(deletedChatIds);
    }
}