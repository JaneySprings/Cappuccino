using System;
using System.Collections.Generic;
using Android.App;
using Android.Runtime;
using Cappuccino.App.Android.Utils;
using Cappuccino.Core.Network.Auth;
using Cappuccino.Core.Network.Config;
using Scope = Cappuccino.Core.Network.Auth.Permissions;

namespace Cappuccino.App.Android {

    [Application(Name="com.springsoftware.Cappuccino", AllowBackup=true, Label="@string/app_name", Icon="@mipmap/ic_launcher", RoundIcon="@mipmap/ic_launcher_round", SupportsRtl=true, Theme="@style/Theme.CappuccinoApp")]
    public class App : Application {

        public App(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) {}

        public override void OnCreate() {
            base.OnCreate();

            new ApiConfiguration.Builder()
                .SetAppId(Constants.ApplicationId)
                .SetApiLanguage(Context.GetString(Resource.String.api_language))
                .SetPermissions(new List<int> {Scope.Friends, Scope.Messages})
                .Initialize();

            KeyValueStorage storage = new KeyValueStorage(this);
            AccessToken storedToken = storage.GetStoredToken();

            if (CredentialsManager.IsTokenValid(storedToken))
                CredentialsManager.ApplyAccessToken(storedToken);
        }
    }
}
