using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsInput.Events {
    [DebuggerDisplay(Debugger2.GetDebuggerDisplay)]
    public abstract class EventBase : IEvent {

        protected virtual string GetDebuggerDisplay() {
            var ret = $@"{this.GetType().Name}";

            return ret;
        }

        public override string ToString() {
            return GetDebuggerDisplay();
        }

        protected abstract Task<bool> Invoke(InvokeOptions Options);

        Task<bool> IEvent.Invoke(InvokeOptions Options) {
            return Invoke(Options);
        }
    }
}
