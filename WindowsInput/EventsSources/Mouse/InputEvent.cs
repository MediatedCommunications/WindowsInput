// This code is distributed under MIT license.
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using WindowsInput.Events;

namespace WindowsInput.Events.Sources {

    [DebuggerDisplay(Debugger2.GetDebuggerDisplay)]
    public abstract class InputEvent {
        public IReadOnlyList<IEvent> Events { get; }

        public InputEvent(IEnumerable<IEvent?> Events) {
            this.Events = Events.OfType<IEvent>().ToList().AsReadOnly();
        }

        public override string ToString() {
            return GetDebuggerDisplay();
        }

        protected virtual string GetDebuggerDisplay() {
            var ret = new StringBuilder();

            ret.AppendLine($@"{this.GetType()}");
            foreach (var item in this.Events) {
                ret.AppendLine($@"  {item}");
            }

            return ret.ToString();

        }

    }
}