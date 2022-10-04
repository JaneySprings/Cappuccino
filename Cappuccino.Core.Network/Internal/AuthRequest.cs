using Cappuccino.Core.Network.Models;
using Cappuccino.Core.Network.Handlers;
using Cappuccino.Core.Network.Internal;
using Cappuccino.Core.Network.Utils;
using Cappuccino.Core.Network.Config;
using System.Threading.Tasks;
using System;

namespace Cappuccino.Core.Network.Internal { 

    [Obsolete("Need direct authorization permissions, not tested", true)]
    internal class AuthRequest : ApiRequest<int> {
        
        public AuthRequest(string username, string password) : base("") { 
            AddParam("client_id", CredentialsManager.ApiConfig?.ApplicationId);
            AddParam("client_secret", CredentialsManager.ApiConfig?.SecretKey);
            AddParam("scope", CredentialsManager.ApiConfig?.Permissions);
            AddParam("v", CredentialsManager.ApiConfig?.ApiVersion);
            AddParam("grant_type", "password");
            AddParam("username", username);
            AddParam("password", password);
        }


        public override async Task<int> Execute() {
            if (string.IsNullOrEmpty(CredentialsManager.ApiConfig?.SecretKey)) {
                throw new Exception("Secret key does not exist in configuration");
            }

            using Executor executor = new Executor();
            string request = $"{EndPoints.AuthorizeBaseUri}/token?{this.Args.JoinToUrl()}";
            string? response = await executor.GetAsync(request);
            
            return OnServerResponseReceived(response!);
        }
    }
}