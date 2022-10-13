using System.Collections.Generic;

namespace Cappuccino.Core.Network {

    public abstract class ApiParams {
        protected Dictionary<string, string> Args { get; }


        protected ApiParams() {
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
        
        protected void ClearParams() {
            Args.Clear();
        }
    }
}