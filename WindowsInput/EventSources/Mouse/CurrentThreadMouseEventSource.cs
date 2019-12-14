// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System;
using WindowsInput.Native;

namespace WindowsInput.EventSources {
    public class CurrentThreadMouseEventSource : MouseEventSource {

        protected override WindowsHookHandle Subscribe() {

            return WindowsHookHandle.Create(
                WindowsHookType.Mouse,
                HookProcedure,
                IntPtr.Zero,
                ThreadNativeMethods.GetCurrentThreadId());

        }


        protected override MouseInput GetEventArgs(CallbackData data) {
            return data.ToAppMouseEventArgs();
        }
    }
}