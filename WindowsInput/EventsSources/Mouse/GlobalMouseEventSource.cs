// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System;
using WindowsInput.Native;

namespace WindowsInput.Events.Sources {
    public class GlobalMouseEventSource : MouseEventSource {

        public GlobalMouseEventSource() {

        }

        protected override HookHandle? Subscribe() {
            var ret = default(HookHandle?);

            if (System.Diagnostics.Process.GetCurrentProcess().MainModule?.BaseAddress is { } Handle) {
                ret = HookHandle.Create(
                    HookType.GlobalMouse,
                    HookProcedure,
                    Handle,
                    0);
            };

            return ret;
        }

        protected override bool Callback(CallbackData data) {
            var e = data.ToGlobalMouseEventArgs();
            var Events = State.GetEventArgs(e);
            var ret = InvokeMany(Events.Data, e, Events.Timestamp);

            return ret.Next_Hook_Enabled;
        }
    }
}