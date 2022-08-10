using System;
using System.Collections.Generic;
using AndroidX.RecyclerView.Widget;

namespace Cappuccino.App.Android.UI.Common {
    public abstract class AdapterBase<TItem>: RecyclerView.Adapter {
        protected DiffUtilCallback<TItem>? DiffUtilCallback;
        public List<TItem> Items { get; } = new List<TItem>();
        public Action<int>? OnItemClicked;
        public Action<int>? OnItemLongClicked;
        public Action<int>? OnLastItemBind;

        public override int ItemCount => Items.Count;

        public void Add(List<TItem> items) {
            Items.AddRange(items);
            NotifyItemRangeInserted(Items.Count, items.Count);
        }
        public void Replace(List<TItem> items) {
            if (this.DiffUtilCallback == null) {
                Items.Clear();
                Items.AddRange(items);
                NotifyDataSetChanged();
            } else this.DiffUtilCallback.UpdateCollection(items);
        }
        public void Clear() {
            Items.Clear();
            NotifyDataSetChanged();
        }
    }
}