using Android.Views;
using Cappuccino.App.Android.ViewBinding;

namespace Cappuccino.App.Android.UI {

    public class AppbarDecorator {
        private AppbarBinding? binding;

        public AppbarDecorator SetElement(AppbarBinding element) {
            this.binding = element;
            return this;
        }

        public AppbarDecorator SetMainActionIcon(int resource) {
            if (resource < 0)
                return this;

            this.binding!.mainAction.SetImageResource(resource);
            this.binding.mainAction.Visibility = ViewStates.Visible;

            return this;
        }

        public AppbarDecorator SetSubActionIcon(int resource) {
            if (resource < 0)
                return this;

            this.binding!.subAction.SetImageResource(resource);
            this.binding.subAction.Visibility = ViewStates.Visible;

            return this;
        }

        public AppbarDecorator SetBackActionIcon(int resource) {
            if (resource < 0)
                return this;

            this.binding!.backAction.SetImageResource(resource);
            this.binding.backAction.Visibility = ViewStates.Visible;

            return this;
        }

        public AppbarDecorator SetTitleText(string text) {
            this.binding!.title.Text = text;
            return this;
        }

        public AppbarDecorator DisableBackAction() {
            this.binding!.backAction.Visibility = ViewStates.Gone;
            return this;
        }


        public void Apply() {
            this.binding = null;
        }
    }
}
