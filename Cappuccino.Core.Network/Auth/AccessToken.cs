namespace Cappuccino.Core.Network.Auth {

    public class AccessToken {
        public string Token { get; private set; }
        public long ExpiresIn { get; private set; }
        public int UserId { get; private set; }

        public AccessToken(string token, long expired, int id) {
            this.UserId = id;
            this.Token = token;
            this.ExpiresIn = expired;
        }
    }
}