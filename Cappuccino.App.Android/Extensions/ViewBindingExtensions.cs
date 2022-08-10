using System;
using Android.Views;
using AndroidX.Fragment.App;
using Cappuccino.App.Android.ViewBinding;

namespace Cappuccino.App.Android.Extensions {
    public static class ViewBindingExtensions {

        public static TBinding ByViewBinding<TBinding>(this Fragment fragment, Func<View, TBinding> bind) where TBinding: BaseBinding {
            return bind(fragment.RequireView());
        }

        public static TBinding ByViewBinding<TBinding>(this Fragment fragment, Func<LayoutInflater, ViewGroup?, bool, TBinding> inflate) where TBinding: BaseBinding {
            return inflate(fragment.LayoutInflater, null, false);
        }
    }
}