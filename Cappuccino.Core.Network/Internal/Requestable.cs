using System.Collections.Generic;

namespace Cappuccino.Core.Network.Internal {

    public abstract class Requestable {
        protected Dictionary<string, string> Args { get; private set; }


        public Requestable() {
            Args = new Dictionary<string, string>();
        }


        protected void AddParam(string key, int? value) {
            if (value != null)
                this.Args.Add(key, value.ToString());
        }
        protected void AddParam(string key, string? value) {
            if (value != null)
                this.Args.Add(key, value);
        }
        protected void AddParam(string key, bool? value) {
            if (value != null)
                this.Args.Add(key, value.ToString().ToLower());
        }
        protected void AddParam(string key, IEnumerable<string>? value) {
            if (value != null)
                this.Args.Add(key, string.Join(',', value));
        }
        protected void AddParam(string key, IEnumerable<int>? value) {
            if (value != null)
                this.Args.Add(key, string.Join(',', value));
        }
    }
}