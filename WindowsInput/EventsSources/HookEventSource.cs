// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using WindowsInput.Native;

namespace WindowsInput.Events.Sources {
    public abstract class HookEventSource : EventSourceBase {


        protected abstract HookHandle Subscribe();
        protected abstract bool Callback(CallbackData data);


        protected override void Enable() {
            Handle = Subscribe();
        }

        protected override void Disable() {
            if (Handle != default) {
                Handle?.Dispose();
                Handle = null;
            }
        }



        protected HookHandle Handle { get; set; }

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
                } else {

                }
            }

            return ret;
        }

        private static IntPtr CallNextHookEx(int nCode, IntPtr wParam, IntPtr lParam) {
            return HookNativeMethods.CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
        }


        protected EventSourceEventArgs InvokeMany(params Func<EventSourceEventArgs, EventSourceEventArgs>[] Actions) {
            var ret = new EventSourceEventArgs(DateTimeOffset.UtcNow); ;
            foreach (var item in Actions) {
                if (ret.Next_Event_Enabled) {
                    var tret = item(ret);
                    ret.Next_Event_Enabled = tret.Next_Event_Enabled;
                    ret.Next_Hook_Enabled = tret.Next_Hook_Enabled;
                }
            }

            return ret;
        }

        protected EventSourceEventArgs InvokeEvent<T>(EventSourceEventArgs args, EventHandler<EventSourceEventArgs<T>> Event, T Data, object RawData, DateTimeOffset Timestamp) {
            var ret = new EventSourceEventArgs(Timestamp);
            ret.Next_Event_Enabled = args.Next_Event_Enabled;
            ret.Next_Hook_Enabled = args.Next_Hook_Enabled;

            if (!EqualityComparer<T>.Default.Equals(Data, default)) {
                var Args = new EventSourceEventArgs<T>(Timestamp, Data, RawData) {
                    Next_Event_Enabled = args.Next_Event_Enabled,
                    Next_Hook_Enabled = args.Next_Hook_Enabled,
                };

                Event?.Invoke(this, Args);

                ret.Next_Event_Enabled = Args.Next_Event_Enabled;
                ret.Next_Hook_Enabled = Args.Next_Hook_Enabled;
            }

            return ret;
        }

    }
}