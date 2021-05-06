// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System;
using WindowsInput.Native;

namespace WindowsInput.Events.Sources {
    public class GlobalMouseEventSource : MouseEventSource {

        public GlobalMouseEventSource() {

        }

        protected override HookHandle Subscribe() {

            return HookHandle.Create(
               HookType.GlobalMouse,
               HookProcedure,
               System.Diagnostics.Process.GetCurrentProcess().MainModule.BaseAddress,
               0);

        }

        protected override bool Callback(CallbackData data) {
            var e = data.ToGlobalMouseEventArgs();
            var Events = State.GetEventArgs(e);
            var ret = InvokeMany(Events.Data, e, Events.Timestamp);

            return ret.Next_Hook_Enabled;
        }
    }
}