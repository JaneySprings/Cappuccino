using System;

namespace Cappuccino.Core.Network.Auth {

    public class AccessToken {
        public string Token { get; }
        public long ExpiresIn { get; private set; }
        public int UserId { get; }

        public AccessToken(string token, long expired, int id) {
            this.UserId = id;
            this.Token = token;
            this.ExpiresIn = expired;
        }

        public void ApplyCurrentTime() {
            this.ExpiresIn += DateTimeOffset.Now.ToUnixTimeSeconds();
        }
    }
}