using System.Collections.Generic;
using Cappuccino.Core.Network.Handlers;
using Cappuccino.Core.Network.Internal;

namespace Cappuccino.Core.Network.Config {

    public class ApiConfiguration {
        public IEnumerable<int> Permissions { get; private set; }
        public int ApplicationId { get; private set; }
        public int DeviceId { get; private set; }
        public int LpVersion { get; private set; }
        public string? ApiLanguage { get; private set; }
        public string? ApiVersion { get; private set; }
        public ITokenStorageHandler? TokenStorageHandler { get; private set; }

        public ApiConfiguration(Builder builder) {
            ApplicationId = builder.AppId;
            DeviceId = builder.DeviceId;
            ApiLanguage = builder.Language;
            ApiVersion = builder.ApiVersion;
            LpVersion = builder.LpVersion;
            Permissions = builder.Permissions;
            TokenStorageHandler = builder.TokenStorageHandler;
        }

        public class Builder {
            internal int AppId { get; private set; }
            internal int DeviceId { get; private set; }
            internal int LpVersion { get; private set; }
            internal string? Language { get; private set; }
            internal string? ApiVersion { get; private set; }
            internal ITokenStorageHandler? TokenStorageHandler { get; private set; }
            internal IEnumerable<int> Permissions = new List<int>();

            public Builder SetAppId(int appId) {
                AppId = appId;
                return this;
            }

            public Builder SetDeviceId(int deviceId) {
                DeviceId = deviceId;
                return this;
            }

            public Builder SetApiLanguage(string language) {
                Language = language;
                return this;
            }

            public Builder SetApiVersion(string version) {
                ApiVersion = version;
                return this;
            }

            public Builder SetLongPollVersion(int version) {
                LpVersion = version;
                return this;
            }

            public Builder SetPermissions(IEnumerable<int> permissions) {
                Permissions = permissions;
                return this;
            }

            public Builder SetTokenStorageHandler(ITokenStorageHandler handler) {
                TokenStorageHandler = handler;
                return this;
            }

            public ApiConfiguration Build() {
                return new ApiConfiguration(this);
            }
        }
    }
}