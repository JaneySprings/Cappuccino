namespace Cappuccino.Core.Network.Auth {

    public class AccessToken {
        public readonly string Token;
        public readonly long ExpiresIn;
        public readonly int UserId;

        public AccessToken(string token, long expired, int id) {
            this.UserId = id;
            this.Token = token;
            this.ExpiresIn = expired;
        }
    }
}