﻿using System.Collections.Generic;
using Cappuccino.Core.Network.Handlers;
using Cappuccino.Core.Network.Internal;

namespace Cappuccino.Core.Network.Config {

    public class ApiConfiguration {
        public readonly IEnumerable<int> Permissions;
        public readonly int ApplicationId;
        public readonly int DeviceId;
        public readonly int LpVersion;
        public readonly string? ApiLanguage;
        public readonly string? ApiVersion;

        public ApiConfiguration(Builder builder) {
            this.ApplicationId = builder.AppId;
            this.DeviceId = builder.DeviceId;
            this.ApiLanguage = builder.Language;
            this.ApiVersion = builder.ApiVersion;
            this.LpVersion = builder.LpVersion;
            this.Permissions = builder.Permissions;
        }

        public class Builder {
            internal int AppId;
            internal int DeviceId;
            internal int LpVersion;
            internal string? Language;
            internal string? ApiVersion;
            internal IEnumerable<int> Permissions = new List<int>();

            public Builder SetAppId(int appId) {
                this.AppId = appId;
                return this;
            }

            public Builder SetDeviceId(int deviceId) {
                this.DeviceId = deviceId;
                return this;
            }

            public Builder SetApiLanguage(string language) {
                this.Language = language;
                return this;
            }

            public Builder SetApiVersion(string version) {
                this.ApiVersion = version;
                return this;
            }

            public Builder SetLongPollVersion(int version) {
                this.LpVersion = version;
                return this;
            }

            public Builder SetPermissions(IEnumerable<int> permissions) {
                this.Permissions = permissions;
                return this;
            }

            public Builder SetTokenStorageHandler(ITokenStorageHandler handler) {
                ApiManager.UpdateStorageHandler(handler);
                return this;
            }

            public ApiConfiguration Build() {
                return new ApiConfiguration(this);
            }
        }
    }
}