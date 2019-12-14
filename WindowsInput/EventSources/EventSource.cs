// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using WindowsInput.Native;

namespace WindowsInput.EventSources {
    public abstract class EventSource {


        protected abstract HookHandle Subscribe();
        protected abstract bool Callback(CallbackData data);


        public bool Enabled {
            get {
                return Handle != null;
            }
            set {
                if (value) {
                    Enable();
                } else {
                    Disable();
                }

            }
        }

        public void Enable() {
            if (Handle == default) {
                EnableInternal();
            }
        }

        protected virtual void EnableInternal() {
            Handle = Subscribe();
        }


        public void Disable() {
            Handle?.Dispose();
            Handle = null;
        }



        protected HookHandle Handle { get; set; }

        public void Dispose() {
            Handle?.Dispose();
        }

        protected IntPtr HookProcedure(int nCode, IntPtr wParam, IntPtr lParam) {
            var ret = new IntPtr(-1);

            var passThrough = nCode != 0;
            if (passThrough) {
                ret = CallNextHookEx(nCode, wParam, lParam);
            } else {
                var callbackData = new CallbackData(wParam, lParam);
                var continueProcessing = Callback(callbackData);

                if (continueProcessing) {
                    ret = CallNextHookEx(nCode, wParam, lParam);
                }
            }

            return ret;
        }

        private static IntPtr CallNextHookEx(int nCode, IntPtr wParam, IntPtr lParam) {
            return HookNativeMethods.CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
        }


        protected bool InvokeEvent(params Func<bool>[] Actions) {
            var ret = false;
            foreach (var item in Actions) {
                ret = ret || item();
            }

            return ret;
        }

        protected bool InvokeEvent<T>(EventHandler<EventSourceEventArgs<T>> Event, T Data, DateTimeOffset Timestamp) {
            var ret = false;

            if (!EqualityComparer<T>.Default.Equals(Data, default)) {
                var Args = new EventSourceEventArgs<T>(Timestamp, false, Data);

                Event?.Invoke(this, Args);

                ret = Args.Handled;
            }

            return ret;
        }

    }
}