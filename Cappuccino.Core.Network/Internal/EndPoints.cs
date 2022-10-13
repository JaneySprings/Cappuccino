namespace Cappuccino.Core.Network.Internal {

    internal static class EndPoints {
        public const string AuthorizeBaseUri = "https://oauth.vk.com";
        public const string AuthorizeDirectUri = AuthorizeBaseUri + "/token";
        public const string AuthorizeImplicitUri = AuthorizeBaseUri + "/authorize";
        public const string RedirectUri = AuthorizeBaseUri + "/blank.html";
        public const string MethodsUri = "https://api.vk.com/method";
    }
}