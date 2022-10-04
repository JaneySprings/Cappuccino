using Cappuccino.Core.Network;
using Cappuccino.Core.Network.Handlers;
using Cappuccino.App.iOS.Extensions;
using _Messages = Cappuccino.Core.Network.Methods.Messages;
using Models = Cappuccino.Core.Network.Models;

namespace Cappuccino.App.iOS.UI.Messages;


public partial class MessagesViewController : UIViewController {
    public override bool HidesBottomBarWhenPushed => true;
    private int conversationId;

    public MessagesViewController(int conversationId) {
        this.conversationId = conversationId;
    }
    

    private void Initialize() {
        RequrstConversation();
    }



    private void RequrstConversation() {
        Api.Get(new _Messages.GetConversationsById {
            PeerIds = new[] { this.conversationId },
            Fields = Constants.DefaultUserFields,
            Extended = 1
        }, new ApiCallback<Models.Messages.GetConversationsByIdResponse>()
            .OnSuccess(result => {
                var conversation = result.InnerResponse?.Items?[0];
                switch(conversation?.peer?.Type) {
                    case "user":
                        var user = result.InnerResponse?.Profiles?.FirstOrDefault(it => it.Id == this.conversationId);
                        this.photo!.Load(user?.Photo200);
                        this.title!.Text = $"{user?.FirstName} {user?.LastName}";
                        break;
                    case "chat":
                        this.photo!.Load(conversation.chatSettings?.Photo?.Photo200);
                        this.title!.Text = conversation.chatSettings?.Title; 
                        break;
                    case "group":
                        var group = result.InnerResponse?.Groups?.FirstOrDefault(it => it.Id == -this.conversationId);
                        this.photo!.Load(group?.Photo200);
                        this.title!.Text = group?.Name;
                        break;
                }
            })
            .OnError(error => {})
        );
    }
}