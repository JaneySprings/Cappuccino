using System;
using System.Collections.Generic;

namespace Cappuccino.App.Android.UI.Common {
    public abstract class DiffUtilCallback<TItem> {
        private readonly AdapterBase<TItem> adapter;

        public DiffUtilCallback(AdapterBase<TItem> adapter) { this.adapter = adapter; }

        public void UpdateCollection(List<TItem> newCollection) {
            List<int> result = new List<int>();
            List<TItem> oldCollection = this.adapter.Items;
            int newCount = newCollection.Count;
            int oldCount = oldCollection.Count;
            int min = Math.Min(newCount, oldCount);
            int max = Math.Max(newCount, oldCount);

            for (int i = 0; i < min; i++) {
                if (!PrimaryKeysSame(newCollection[i], oldCollection[i])) result.Add(i);
                else if (!ItemsSame(newCollection[i], oldCollection[i])) result.Add(i);
            }

            this.adapter.Items.Clear();
            this.adapter.Items.AddRange(newCollection);
            foreach (int index in result)
                this.adapter.NotifyItemChanged(index);

            if (newCount == oldCount) return;

            if (newCount > oldCount) this.adapter.NotifyItemRangeInserted(min, max - min);
            else this.adapter.NotifyItemRangeRemoved(min, max-min);
        }

        public abstract bool PrimaryKeysSame(TItem newItem, TItem oldItem);
        public abstract bool ItemsSame(TItem newItem, TItem oldItem);
    }
}