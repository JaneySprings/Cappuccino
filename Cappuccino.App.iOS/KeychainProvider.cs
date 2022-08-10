using System;
using System.Collections.Generic;
using Cappuccino.Core.Network.Auth;
using Cappuccino.Core.Network.Handlers;
using Foundation;
using Security;

namespace Cappuccino.App.iOS {
    public class KeychainProvider: ITokenStorageHandler {
        private const string Alias = "Cappuccino.KeychainProvider";

        public AccessToken? OnTokenRequested() {
            SecRecord record = new SecRecord (SecKind.Key) { Label = Alias };
            NSData[]? data = SecKeyChain.QueryAsData(record, 1);

            if (data == null)
                return null;

            string valueData = data[0].ToString();
            string[] tokens = valueData.Split('&');

            if (tokens.Length != 3)
                return null;

            return new AccessToken(
                token: tokens[0],
                expired: Int64.Parse(tokens[1]),
                id: Int32.Parse(tokens[2])
            );
        }

        public void OnTokenReceived(AccessToken token) {
            SecRecord record = new SecRecord (SecKind.Key) { Label = Alias };
            SecKeyChain.Remove(record);

            IEnumerable<string> items = new List<string>() {
                token.Token,
                token.ExpiresIn.ToString(),
                token.UserId.ToString()
            };

            string data = String.Join('&', items);
            record = new SecRecord (SecKind.Key) {
                Label = Alias,
                ValueData = NSData.FromString(data, NSStringEncoding.UTF8)
            };

            SecKeyChain.Add(record);
        }
    }
}