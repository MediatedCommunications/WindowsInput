// This code is distributed under MIT license.
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using WindowsInput.Events;

namespace WindowsInput.Events.Sources {

    [DebuggerDisplay(Debugger2.DISPLAY)]
    public abstract class InputEvent {
        public IReadOnlyCollection<IEvent> Events { get; protected set; }

        public override string ToString() {
            return DebuggerDisplay;
        }

        protected virtual string DebuggerDisplay {
            get {
                var ret = new StringBuilder();

                ret.AppendLine($@"{this.GetType()}");
                foreach (var item in this.Events) {
                    ret.AppendLine($@"  {item}");
                }

                return ret.ToString();
            }
        }

    }
}