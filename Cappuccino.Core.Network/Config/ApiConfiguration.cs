using System.Collections.Generic;
using System.Linq;
using Cappuccino.Core.Network.Handlers;

namespace Cappuccino.Core.Network.Config {

    public class ApiConfiguration {
        public int Permissions { get; }
        public int ApplicationId { get; }
        public int DeviceId { get; }
        public int LpVersion { get; }
        public string? ApiLanguage { get; }
        public string? ApiVersion { get; }
        public string? SecretKey { get; }
        public ITokenStorageHandler? TokenStorageHandler { get; }

        public ApiConfiguration(Builder builder) {
            ApplicationId = builder.AppId;
            DeviceId = builder.DeviceId;
            ApiLanguage = builder.Language;
            ApiVersion = builder.ApiVersion;
            LpVersion = builder.LpVersion;
            Permissions = builder.Permissions;
            SecretKey = builder.SecretKey;
            TokenStorageHandler = builder.TokenStorageHandler;
        }

        public class Builder {
            internal int AppId { get; private set; }
            internal int Permissions { get; private set; }
            internal int DeviceId { get; private set; }
            internal int LpVersion { get; private set; }
            internal string? Language { get; private set; }
            internal string? ApiVersion { get; private set; }
            internal string? SecretKey { get; private set; }
            internal ITokenStorageHandler? TokenStorageHandler { get; private set; }


            public Builder WithAppId(int appId) {
                AppId = appId;
                return this;
            }
            public Builder WithDeviceId(int deviceId) {
                DeviceId = deviceId;
                return this;
            }
            public Builder WithApiLanguage(string language) {
                Language = language;
                return this;
            }
            public Builder WithApiVersion(string version) {
                ApiVersion = version;
                return this;
            }
            public Builder WithLongPollVersion(int version) {
                LpVersion = version;
                return this;
            }
            public Builder WithPermissions(IEnumerable<int> permissions) {
                Permissions = permissions.Sum();
                return this;
            }
            public Builder WithSecretKey(string secretKey) {
                SecretKey = secretKey;
                return this;
            }
            public Builder WithTokenStorageHandler(ITokenStorageHandler handler) {
                TokenStorageHandler = handler;
                return this;
            }

            public ApiConfiguration Build() {
                return new ApiConfiguration(this);
            }
        }
    }
}