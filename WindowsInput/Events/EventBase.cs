using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsInput.Events {
    [DebuggerDisplay(Debugger2.DISPLAY)]
    public abstract class EventBase : IEvent {

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected virtual string DebuggerDisplay => $@"{this.GetType().Name}";

        public override string ToString() {
            return DebuggerDisplay;
        }

        protected abstract Task<bool> Invoke(InvokeOptions Options);

        Task<bool> IEvent.Invoke(InvokeOptions Options) {
            return Invoke(Options);
        }
    }
}
