using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Cappuccino.Core.Network.Handlers;
using Cappuccino.Core.Network.Internal;
using Cappuccino.Core.Network.Utils;
using Cappuccino.Core.Network.Models;
using Cappuccino.Core.Network.Config;

namespace Cappuccino.Core.Network {

    public abstract class ApiRequest<TResult> {
        public readonly Dictionary<string, string> Args;
        private readonly string method;

        protected ApiRequest(string method) {
            this.method = method;
            this.Args = new Dictionary<string, string>();
        }


        protected void AddParam(string key, int? value) {
            if (value != null)
                this.Args.Add(key, value.ToString());
        }

        protected void AddParam(string key, string? value) {
            if (value != null)
                this.Args.Add(key, value);
        }

        protected void AddParam(string key, bool? value) {
            if (value != null)
                this.Args.Add(key, value.ToString());
        }

        protected void AddParam(string key, IEnumerable<string>? value) {
            if (value != null)
                this.Args.Add(key, String.Join(',', value));
        }

        protected void AddParam(string key, IEnumerable<int>? value) {
            if (value != null)
                this.Args.Add(key, String.Join(',', value));
        }


        public async Task<TResult> Execute() {
            if (!CredentialsManager.IsInternalTokenValid()) {
                TokenExpiredHandler.RequestTokenError();
                throw new Exception("Access token incorrect!");
            }

            AddParam("lang", ApiManager.ApiConfig?.ApiLanguage);
            AddParam("v", ApiManager.ApiConfig?.ApiVersion);
            AddParam("access_token", ApiManager.AccessToken!.Token);

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