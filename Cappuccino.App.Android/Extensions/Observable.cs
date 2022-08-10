using System;
using AndroidX.Lifecycle;

namespace Cappuccino.App.Android.Extensions {

    public class Observable<TValue> {
        private Action<TValue>? observer;
        private EventHandler? valueChanged;
        private ILifecycleOwner? lifecycleOwner;

        public void PostValue(TValue items) {
            this.valueChanged?.Invoke(this, EventArgs.Empty);
            if (this.lifecycleOwner == null)
                return;

            if (this.lifecycleOwner.Lifecycle.CurrentState != Lifecycle.State.Destroyed)
                this.observer?.Invoke(items);
        }

        public void Observe(ILifecycleOwner owner, Action<TValue> handler) {
            this.observer = handler;
            this.lifecycleOwner = owner;
        }
        public void AddChangeListener(EventHandler changed) {
            this.valueChanged += changed;
        }
        public void RemoveChangeListener(EventHandler changed) {
            this.valueChanged -= changed;
        }
    }

}