// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System;

namespace WindowsInput.Events.Sources {
    public class SuspendEventSource : IDisposable {

        protected IEventSource Target { get; }
        protected bool Original { get; }

        public SuspendEventSource(IEventSource Target) {
            this.Target = Target;

            this.Original = Target.Enabled;
            Target.Enabled = false;
        }

        public void Dispose() {
            Target.Enabled = Original;
        }
    }

}