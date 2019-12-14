// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System;
using System.Collections.Generic;
using WindowsInput.Events;
using WindowsInput.Native;

namespace WindowsInput.EventSources {
    public class CurrentThreadKeyboardEventSource : KeyboardEventSource {

        protected override HookHandle Subscribe() {

            return HookHandle.Create(
                HookType.Keyboard,
                HookProcedure,
                IntPtr.Zero,
                ThreadNativeMethods.GetCurrentThreadId());

        }

        protected override bool Callback(CallbackData data) {
            return false;
        }



        protected IEnumerable<EventSourceEventArgs<KeyPressData>> GetPressEventArgs(CallbackData data) {
            return ToAppKeypressEventArgs(data);
        }

        protected EventSourceEventArgs<KeyInput> GetDownUpEventArgs(CallbackData data) {
            return ToAppKeyEventArgs(data);
        }

        IEnumerable<EventSourceEventArgs<KeyPressData>> ToAppKeypressEventArgs(CallbackData data) {
            var wParam = data.WParam;
            var lParam = data.LParam;

            //http://msdn.microsoft.com/en-us/library/ms644984(v=VS.85).aspx

            const uint maskKeydown = 0x40000000; // for bit 30
            const uint maskKeyup = 0x80000000; // for bit 31
            const uint maskScanCode = 0xff0000; // for bit 23-16

            var flags = (uint)lParam.ToInt64();

            //bit 30 Specifies the previous key state. The value is 1 if the key is down before the message is sent; it is 0 if the key is up.
            var wasKeyDown = (flags & maskKeydown) > 0;
            //bit 31 Specifies the transition state. The value is 0 if the key is being pressed and 1 if it is being released.
            var isKeyReleased = (flags & maskKeyup) > 0;

            if (!wasKeyDown && !isKeyReleased)
                yield break;

            var virtualKeyCode = (int)wParam;
            var scanCode = checked((int)(flags & maskScanCode));
            const int fuState = 0;


            if (KeyboardNativeMethods.TryGetCharFromKeyboardState(virtualKeyCode, scanCode, fuState, out var chars)) {
                foreach (var ch in chars) {

                    var Data = new KeyPressData(ch);
                    var ret = EventSourceEventArgs.Create(DateTimeOffset.Now, false, Data);

                    yield return ret;
                }
            }
        }

        EventSourceEventArgs<KeyInput> ToAppKeyEventArgs(CallbackData data) {
            var wParam = data.WParam;
            var lParam = data.LParam;

            //http://msdn.microsoft.com/en-us/library/ms644984(v=VS.85).aspx

            const uint maskKeydown = 0x4000_0000; // for bit 30
            const uint maskKeyup = 0x8000_0000; // for bit 31
            const uint maskExtendedKey = 0x100_0000; // for bit 24

            var timestamp = Environment.TickCount;

            var flags = (uint)lParam.ToInt64();

            //bit 30 Specifies the previous key state. The value is 1 if the key is down before the message is sent; it is 0 if the key is up.
            var wasKeyDown = (flags & maskKeydown) > 0;
            //bit 31 Specifies the transition state. The value is 0 if the key is being pressed and 1 if it is being released.
            var isKeyReleased = (flags & maskKeyup) > 0;
            //bit 24 Specifies the extended key state. The value is 1 if the key is an extended key, otherwise the value is 0.
            var isExtendedKey = (flags & maskExtendedKey) > 0;


            var keyData = (KeyCode)wParam;
            var scanCode = (int)(((flags & 0x1_0000) | (flags & 0x2_0000) | (flags & 0x4_0000) | (flags & 0x8_0000) |
                                   (flags & 0x10_0000) | (flags & 0x20_0000) | (flags & 0x400000) | (flags & 0x80_0000)) >>
                                  16);
            var scanCode2 = ((int)flags & 0xFF_0000) >> 16;

            if (scanCode2 != scanCode) {

            }


            var isKeyDown = !isKeyReleased;
            var isKeyUp = wasKeyDown && isKeyReleased;

            var Status = KeyStatusValue.Compute(isKeyDown, isKeyUp);


            var Data = new KeyInput(keyData, isExtendedKey, scanCode, Status);
            var ret = EventSourceEventArgs.Create(timestamp, false, Data);

            return ret;
        }


    }

}