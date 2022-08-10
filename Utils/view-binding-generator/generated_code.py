base_binding_content = """using Android.Views;

namespace Cappuccino.App.Android.ViewBinding {

    public abstract class BaseBinding {
        public View? Root;

        protected BaseBinding(View view) { Initialize(view); }

        protected void Initialize(View view) {
            this.Root = view;
            FindViews(view);
        }
        protected abstract void FindViews(View view);
    }

}"""