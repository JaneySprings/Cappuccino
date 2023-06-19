using System.Threading.Tasks;
using System.Collections.Generic;
using Cappuccino.Core.Network.Internal;
using Cappuccino.Core.Network.Utils;
using Cappuccino.Core.Network.Config;
using System;
using System.Threading;

namespace Cappuccino.Core.Network {
    public class ApiMethod<TResult>: ApiRequest<TResult>, IDisposable where TResult : class {
        private readonly string method;
        private readonly Dictionary<string, string> arguments;
        private Action<TResult>? successHandler;
        private Action<ApiException>? errorHandler;
        private readonly Executor executor;

        public ApiMethod(string method) {
            this.executor = new Executor(EndPoints.ApiBaseUri);
            this.arguments = new Dictionary<string, string>();
            this.method = method;

            AddParam("lang", CredentialsManager.ApiConfig?.ApiLanguage);
            AddParam("v", CredentialsManager.ApiConfig?.ApiVersion);
            AddParam("access_token", CredentialsManager.AccessToken?.Token);
        }

        protected override async Task<TResult> OnExecuteAsync(CancellationToken cancellationToken) {
            string request = $"method/{this.method}?{this.arguments.JoinToUrl()}";
            string? response = await executor.GetAsync(request, cancellationToken);
            if (response == null)
                throw new ApiException("Response is null");

            return OnServerResponseReceived(response);
        }

        public ApiMethod<TResult> OnSuccess(Action<TResult> handler) {
            this.successHandler = handler;
            return this;
        }
        public ApiMethod<TResult> OnError(Action<ApiException> handler) {
            this.errorHandler = handler;
            return this;
        }
        public ApiMethod<TResult> AddParam(string key, int? value) {
            if (value != null)
                this.arguments.Add(key, value.ToString());
            return this;
        }
        public ApiMethod<TResult> AddParam(string key, string? value) {
            if (value != null)
                this.arguments.Add(key, value);
            return this;
        }
        public ApiMethod<TResult> AddParam(string key, bool? value) {
            if (value != null)
                this.arguments.Add(key, value.ToString().ToLower());
            return this;
        }
        public ApiMethod<TResult> AddParam(string key, IEnumerable<string>? value) {
            if (value != null)
                this.arguments.Add(key, string.Join(',', value));
            return this;
        }
        public ApiMethod<TResult> AddParam(string key, IEnumerable<int>? value) {
            if (value != null)
                this.arguments.Add(key, string.Join(',', value));
            return this;
        }
        public async Task GetAsync(CancellationToken cancellationToken = default) {
            try {
                var response = await ExecuteAsync(cancellationToken);
                this.successHandler?.Invoke(response);
            } catch (TaskCanceledException) {
                // ignored
            } catch (ApiException e) {
                this.errorHandler?.Invoke(e);
            }
        }

        public void Dispose() {
            this.executor.Dispose();
        }
    }
}