using System.Threading.Tasks;
using System.Collections.Generic;
using Cappuccino.Core.Network.Internal;
using Cappuccino.Core.Network.Utils;

using Cappuccino.Core.Network.Config;

namespace Cappuccino.Core.Network {

    public abstract class ApiMethod<TResult>: ApiRequest<TResult> {
        private readonly string method;
        private readonly Dictionary<string, string> arguments;


        protected ApiMethod(string method) { 
            this.arguments = new Dictionary<string, string>();
            this.method = method; 

            AddParam("lang", CredentialsManager.ApiConfig?.ApiLanguage);
            AddParam("v", CredentialsManager.ApiConfig?.ApiVersion);
            AddParam("access_token", CredentialsManager.AccessToken?.Token);
        }

        protected override async Task<TResult> OnExecute() {
            using Executor executor = new Executor();
            string request = $"{EndPoints.MethodsUri}/{this.method}?{this.arguments.JoinToUrl()}";
            string? response = await executor.GetAsync(request);
            
            return OnServerResponseReceived(response!);
        }


        protected void AddParam(string key, int? value) {
            if (value != null)
                this.arguments.Add(key, value.ToString());
        }
        protected void AddParam(string key, string? value) {
            if (value != null)
                this.arguments.Add(key, value);
        }
        protected void AddParam(string key, bool? value) {
            if (value != null)
                this.arguments.Add(key, value.ToString().ToLower());
        }
        protected void AddParam(string key, IEnumerable<string>? value) {
            if (value != null)
                this.arguments.Add(key, string.Join(',', value));
        }
        protected void AddParam(string key, IEnumerable<int>? value) {
            if (value != null)
                this.arguments.Add(key, string.Join(',', value));
        }
        protected void ClearParams() {
            this.arguments.Clear();
        }
    }
}