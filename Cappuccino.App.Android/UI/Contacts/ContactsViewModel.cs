using System;
using System.Collections.Generic;
using AndroidX.Lifecycle;
using Cappuccino.App.Android.Extensions;
using Cappuccino.Core.Network;
using Cappuccino.Core.Network.Handlers;
using Cappuccino.Core.Network.Methods;
using Cappuccino.Core.Network.Models;
using Cappuccino.Core.Network.Models.Response;

namespace Cappuccino.App.Android.UI {
    public class ContactsViewModel: ViewModel {
        public readonly Observable<List<User>> Users = new Observable<List<User>>();
        public readonly Observable<List<User>> Search = new Observable<List<User>>();
        public readonly Observable<List<User>> ImpUsers = new Observable<List<User>>();
        private string buffer = String.Empty;

        private const int RequestCount = 30;
        private const int RequestSearchCount = 15;
        private const int RequestImportantCount = 5;


        public void RequestImportantFriends() {
            Api.Get(new Friends.Get(Order.Hints, RequestImportantCount, 0, Fields.Users.Default), new ApiCallback<FriendsGetResponse>()
                .OnSuccess(result => this.ImpUsers.PostValue(result.Response!.Items!))
                .OnError(reason => { })
            );
        }

        public void RequestFriends(int offset) {
            Api.Get(new Friends.Get(Order.Name, RequestCount, offset, Fields.Users.Default), new ApiCallback<FriendsGetResponse>()
                .OnSuccess(result => this.Users.PostValue(result.Response!.Items!))
                .OnError(reason => { })
            );
        }

        public void RequestSearch(string query) {
            Api.Get(new Friends.Search(query, RequestSearchCount, 0, Fields.Users.Default), new ApiCallback<FriendsSearchResponse>()
                .OnSuccess(result => {
                    if (this.buffer != String.Empty) {
                        RequestSearch(this.buffer);
                        this.buffer = String.Empty;
                    } else this.Search.PostValue(result.Response!.Items!);
                })
                .OnBusy(count => { this.buffer = query; }
            ), RequestPriority.Single);
        }
    }
}