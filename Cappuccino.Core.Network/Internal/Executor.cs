using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Cappuccino.Core.Network.Internal {
    internal sealed class Executor : IDisposable {
        private readonly HttpClient client;

        public Executor(string baseUrl) {
            this.client = new HttpClient() {
                BaseAddress = new Uri(baseUrl)
            };
        }

        public async Task<string?> GetAsync(string request, CancellationToken cancellationToken) {
            using var response = await this.client.GetAsync(request, cancellationToken);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public void Dispose() {
            this.client.Dispose();
        }
    }
}