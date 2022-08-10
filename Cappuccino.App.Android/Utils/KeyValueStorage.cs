using System.Collections.Generic;
using Android.Content;
using Cappuccino.Core.Network.Auth;

namespace Cappuccino.App.Android.Utils {

    public class KeyValueStorage {
        private readonly ISharedPreferences? preferences;
        private const string KeyStoreAlias = "com.springsoftware.cappuccino.credentials";


        public KeyValueStorage(Context context) {
            this.preferences = context.GetSharedPreferences(KeyStoreAlias, FileCreationMode.Private);
        }


        public AccessToken GetStoredToken() {
            Dictionary<string, string> param = new() {
                { "access_token", this.preferences?.GetString(nameof(AccessToken.Token), "")! },
                { "expires_in", this.preferences?.GetString(nameof(AccessToken.ExpiresIn), "0")! },
                { "creation_date", this.preferences?.GetString(nameof(AccessToken.CreationDate), "0")! },
                { "user_id", this.preferences?.GetString(nameof(AccessToken.UserId), "-1")! }
            };
            return AccessToken.CreateFromParams(param);
        }

        public void SaveAccessToken(AccessToken token) {
            ISharedPreferencesEditor? editor = this.preferences?.Edit();
            editor?.PutString(nameof(token.Token), token.Token);
            editor?.PutString(nameof(token.ExpiresIn), token.ExpiresIn.ToString());
            editor?.PutString(nameof(token.CreationDate), token.CreationDate.ToString());
            editor?.PutString(nameof(AccessToken.UserId), token.UserId.ToString());
            editor?.Apply();
        }

        public void ClearStorage() {
            ISharedPreferencesEditor? editor = this.preferences?.Edit();
            editor?.Remove(nameof(AccessToken.Token));
            editor?.Remove(nameof(AccessToken.ExpiresIn));
            editor?.Remove(nameof(AccessToken.CreationDate));
            editor?.Remove(nameof(AccessToken.UserId));
            editor?.Apply();
        }
    }
}