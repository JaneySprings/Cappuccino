using System;
using System.Linq;
using System.Collections.Generic;
using Cappuccino.Core.Network.Config;
using Cappuccino.Core.Network.Handlers;
using Cappuccino.Core.Network.Internal;
using Cappuccino.Core.Network.Utils;

namespace Cappuccino.Core.Network.Auth {

    [Obsolete("Need direct authorization permissions, not tested", true)]
    internal class DirectAuthentificator: IRequestCallback<int> {
        private IValidationCallback? validationCallback;
        public EventHandler? Authorized { get; set; }


        public void TryAuthorize(string login, string password, IValidationCallback? callback = null) {
            if (CredentialsManager.ApiConfig?.ApplicationId == null)
                throw new Exception("Application Id does not exist in configuration");

            this.validationCallback = callback;
            Api.Get(new AuthRequest(login, password), this);
        }


        void IRequestCallback<int>.OnSuccess(int result) {
            //Validate token
            this.Authorized?.Invoke(this, EventArgs.Empty);
        }
        void IRequestCallback<int>.OnError(string reason) {
            this.validationCallback?.OnValidationFail(reason);
        }  
    }
}