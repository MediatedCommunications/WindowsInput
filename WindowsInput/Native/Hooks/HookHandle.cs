// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using Microsoft.Win32.SafeHandles;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace WindowsInput.Native {
    public class HookHandle : SafeHandleZeroOrMinusOneIsInvalid {

        public static HookHandle Create(HookType idHook, HookProcedure lpfn, IntPtr hMod,int dwThreadId) {
            var FunctionPointer = GCHandle.Alloc(lpfn);

            var ret = HookNativeMethods.SetWindowsHookEx(idHook, lpfn, hMod, dwThreadId);

            if (ret.IsInvalid) {
                FunctionPointer.Free();
                ThrowLastUnmanagedErrorAsException();
            } else {
                ret.FunctionPointer = FunctionPointer;
            }

            return ret;
        }

        protected static void ThrowLastUnmanagedErrorAsException() {
            var errorCode = Marshal.GetLastWin32Error();
            throw new Win32Exception(errorCode);
        }

        public HookHandle()
            : base(true) {
        }

        protected GCHandle FunctionPointer { get; set; }

        protected override bool ReleaseHandle() {
            var ret = HookNativeMethods.UnhookWindowsHookEx(handle) != 0;

            if (FunctionPointer.IsAllocated) {
                FunctionPointer.Free();
            }

            return ret;
        }
    }
}