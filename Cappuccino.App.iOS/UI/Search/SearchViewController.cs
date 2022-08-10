using System;
using Cappuccino.App.iOS.Extensions;
using Cappuccino.App.iOS.UI.Common;
using Cappuccino.App.iOS.UI.Contacts;
using Cappuccino.Core.Network;
using Cappuccino.Core.Network.Handlers;
using Cappuccino.Core.Network.Methods;
using Models = Cappuccino.Core.Network.Models;
using Users = Cappuccino.Core.Network.Methods.Users;
using Foundation;
using UIKit;

namespace Cappuccino.App.iOS.UI.Search {

    [Register("SearchViewController")]
    public partial class SearchViewController : UIViewController {
        public SearchViewController(IntPtr handle) : base(handle) {}
        public SearchViewController() : base("SearchViewController", null) {}


        private readonly SearchAdapterDelegate adapter = new SearchAdapterDelegate();


        public override void ViewDidLoad() {
            base.ViewDidLoad();

            new NavigationBarBuilder()
                .WithElement(NavigationItem)
                .WithTitle("Global Search")
                .WithSearch(SearchTextChanged)
                .WithSearchIcon("sliders_outline_28", SearchIconClicked)
                .Apply();
            
            tableView.RegisterNibForCellReuseEx<UserViewCell>();
            tableView.DataSource = this.adapter;
            tableView.Delegate = this.adapter;
        }

        public override void DidReceiveMemoryWarning() {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }



        private void SearchTextChanged(object sender, UISearchBarTextChangedEventArgs args) {
            if (!args.SearchText.Equals(String.Empty)) {
                SearchUsers(args.SearchText);
            } else {
                this.adapter.ClearAll();
                tableView.ReloadData();
            }
        }

        private void SearchIconClicked(object sender, EventArgs args) {
            
        }


        private void SearchUsers(string q) {
            Api.Get(new Users.Search(q, 0, 0, 100, UserFields.Default), new ApiCallback<Models.Users.SearchResponse>()
                .OnSuccess(result => {
                    this.adapter.Replace(result.InnerResponse?.Items!);
                    tableView.ReloadData();
                })
                .OnError(reason => {
                    Console.WriteLine(reason);
                }), RequestPriority.Single
            );
        }
    }
}


