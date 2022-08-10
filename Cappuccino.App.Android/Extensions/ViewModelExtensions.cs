using AndroidX.Lifecycle;

namespace Cappuccino.App.Android.Extensions {
    public static class ViewModelExtensions {
        public static TModel ByViewModels<TModel>(this IViewModelStoreOwner owner) where TModel: ViewModel {
            return (TModel)(new ViewModelProvider(owner).Get(Java.Lang.Class.FromType(typeof(TModel))));
        }
    }
}