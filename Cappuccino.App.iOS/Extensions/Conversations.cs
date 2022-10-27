using Cappuccino.Core.Network.Models.Messages;
using _Messages = Cappuccino.Core.Network.Methods.Messages;
using Cappuccino.Core.Network;

namespace Cappuccino.App.iOS.Extensions;

public class GetConversationsById : ApiRequest<_Messages.GetConversations.Response> {
    public IEnumerable<int>? ConversationIds { get; set; }
    public IEnumerable<string>? Fields { get; set; }
    public int Extended { get; set; }

    protected override async Task<_Messages.GetConversations.Response> OnExecute() {
        var conversationsLastMessageIds = new List<int>();
        var conversationsRequest = new _Messages.GetConversationsById {
            PeerIds = this.ConversationIds!,
            Extended = this.Extended,
            Fields = this.Fields!
        };

        _Messages.GetConversationsById.Response conversations = await conversationsRequest.Execute();

        foreach (var conversation in conversations.Inner!.Items!)
            conversationsLastMessageIds.Add(conversation.LastMessageId);

        var messagesRequest = new _Messages.GetById { MessageIds = conversationsLastMessageIds };
        var chats = new List<Chat>();

        _Messages.GetById.Response messages = await messagesRequest.Execute();

        for (int i = 0; i < conversationsLastMessageIds.Count; i++) {
            chats.Add(new Chat {
                Conversation = conversations.Inner.Items[i],
                LastMessage = messages.Inner!.Items![i]
            });
        }

        return new _Messages.GetConversations.Response { 
            Inner = new _Messages.GetConversations.Response.InnerResponse {
                Count = chats.Count,
                Items = chats,
                Profiles = conversations.Inner.Profiles,
                Groups = conversations.Inner.Groups
            }
        };
    }
}