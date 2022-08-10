using Android.Views;
using AndroidX.RecyclerView.Widget;
using Cappuccino.App.Android.ViewBinding;

namespace Cappuccino.App.Android.UI.Common {
    public class TextHeader: RecyclerView.Adapter {
        private readonly int resId;
        private readonly bool lineVisible;

        public TextHeader(int resId, bool lineVisible = false) {
            this.resId = resId;
            this.lineVisible = lineVisible;
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
            return new HeaderHolder(ItemHeaderBinding.Inflate(
                LayoutInflater.From(parent.Context)!, parent, false));
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
            if (holder is HeaderHolder viewHolder)
                viewHolder.Bind(this.resId, this.lineVisible);
        }
        public override int ItemCount => 1;

        private class HeaderHolder: RecyclerView.ViewHolder {
            private readonly ItemHeaderBinding? binding;
            public HeaderHolder(ItemHeaderBinding binding) : base(binding.Root) { this.binding = binding; }

            public void Bind(int resId, bool lineVisible) {
                this.binding!.header.Text = ItemView.Context?.GetString(resId);
                this.binding.line.Visibility = lineVisible ? ViewStates.Visible : ViewStates.Gone;
            }
        }
    }
}