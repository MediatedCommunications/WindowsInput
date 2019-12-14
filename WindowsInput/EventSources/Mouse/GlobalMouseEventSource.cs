// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System;
using WindowsInput.Native;

namespace WindowsInput.EventSources {
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

        protected override EventSourceEventArgs<MouseInput> GetEventArgs(CallbackData data) {
            return data.ToGlobalMouseEventArgs();
        }
    }
}