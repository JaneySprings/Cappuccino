using AndroidX.Lifecycle;
using Cappuccino.App.Android.Extensions;
using Cappuccino.Core.Network;
using Cappuccino.Core.Network.Methods;
using Cappuccino.Core.Network.Handlers;
using Cappuccino.Core.Network.Models.Response;

namespace Cappuccino.App.Android.UI {
    public class ProfileViewModel: ViewModel {
        public readonly Observable<UsersGetResponse> Users = new Observable<UsersGetResponse>();

        public void RequestCurrentUser() {
            Api.Get(new Users.Get(Fields.Users.Default), new ApiCallback<UsersGetResponse>()
                .OnSuccess(result => this.Users.PostValue(result))
                .OnError(reason => { })
            );
        }
    }
}