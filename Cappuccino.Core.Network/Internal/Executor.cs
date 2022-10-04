using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Cappuccino.Core.Network.Internal {

    internal class Executor : IDisposable {

        public async Task<string?> GetAsync(string uri) {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.Timeout = 30000;

            using HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
            await using Stream? stream = response.GetResponseStream();

            if (stream == null)
                return null;

            using StreamReader reader = new StreamReader(stream);
            return await reader.ReadToEndAsync();
        }

        public void Dispose() { }
    }
}