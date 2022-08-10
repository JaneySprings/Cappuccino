using System;
using Android.Views;
using AndroidX.RecyclerView.Widget;
using Cappuccino.App.Android.Extensions;
using Cappuccino.App.Android.UI.Common;
using Cappuccino.App.Android.ViewBinding;
using Cappuccino.Core.Network.Models;

namespace Cappuccino.App.Android.UI {
    public class UsersAdapter: AdapterBase<User> {
        public Action<int>? OnItemPhotoClicked;

        public UsersAdapter() { this.DiffUtilCallback = new UsersDiffCallback(this); }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
            return new UserHolder(ItemUserBinding.Inflate(LayoutInflater.From(parent.Context)!, parent));
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
            if (holder is UserHolder viewHolder)
                viewHolder.Bind(Items[position]);

            if (position == ItemCount-1)
                this.OnLastItemBind?.Invoke(ItemCount);
        }

        private class UserHolder: RecyclerView.ViewHolder {
            private readonly ItemUserBinding? binding;
            public UserHolder(ItemUserBinding binding) : base(binding.Root) {
                this.binding = binding;
            }

            public void Bind(User item) {
                this.binding!.name.Text = $"{item.FirstName} {item.LastName}";
                this.binding.caption.Visibility = item.City?.Title == null ? ViewStates.Gone : ViewStates.Visible;
                this.binding.caption.Text = item.City?.Title;
                this.binding.online.Visibility = item.Online == 1 ? ViewStates.Visible : ViewStates.Gone;
                this.binding.photo.Load(item.Photo100);
            }
        }
    }
}