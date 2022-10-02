using System;
using System.Text.Json;
using System.Threading.Tasks;
using Cappuccino.Core.Network.Handlers;
using Cappuccino.Core.Network.Internal;
using Cappuccino.Core.Network.Utils;
using Cappuccino.Core.Network.Models;
using Cappuccino.Core.Network.Config;

namespace Cappuccino.Core.Network {

    public abstract class ApiRequest<TResult>: Requestable {
        private readonly string method;

        protected ApiRequest(string method) { this.method = method; }


        public virtual async Task<TResult> Execute() {
            if (!CredentialsManager.IsInternalTokenValid()) {
                throw new Exception("Access token incorrect");
            }   

            AddParam("lang", CredentialsManager.ApiConfig?.ApiLanguage);
            AddParam("v", CredentialsManager.ApiConfig?.ApiVersion);
            AddParam("access_token", CredentialsManager.AccessToken!.Token);

            using Executor executor = new Executor();
            string request = $"{EndPoints.MethodsUri}/{this.method}?{this.Args.JoinToUrl()}";
            string? response = await executor.GetAsync(request);
            
            return OnServerResponseReceived(response!);
        } 

        /* Dirty response, may contain error from server */
        protected virtual TResult OnServerResponseReceived(string response) {
            ErrorResponse error = JsonSerializer.Deserialize<ErrorResponse>(response)!;

            if (error.InnerError == null)
                return OnResponseSuccess(response);

            TokenExpiredHandler.ValidateError(error);
            throw new Exception($"Api {error.InnerError!.ErrorCode}: {error.InnerError!.ErrorMsg}");
        }

        /* Handled response, deserialize and pass */
        protected virtual TResult OnResponseSuccess(string response) {
            TResult result = JsonSerializer.Deserialize<TResult>(response)!;
            return result;
        }
    }
}