// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System;
using System.Collections.Generic;
using System.Text;
using WindowsInput.Events;
using WindowsInput.Native;

namespace WindowsInput.Events.Sources {
    public class CurrentThreadKeyboardEventSource : KeyboardEventSource {

        protected override HookHandle? Subscribe() {

            return HookHandle.Create(
                HookType.AppKeyboard,
                HookProcedure,
                IntPtr.Zero,
                ThreadNativeMethods.GetCurrentThreadId());

        }

        protected override bool Callback(CallbackData data) {

            var timestamp = DateTimeOffset.UtcNow;
            var Wait = new Wait(timestamp - State.LastInputDate);
            State.LastInputDate = DateTimeOffset.UtcNow;


            var Key = (KeyCode)data.WParam;

            var lParam = (long)data.LParam;

            const uint maskScanCode = 0xFF_0000; // for bit 23-16
            var ScanCode = (lParam & maskScanCode) >> 16;

            const uint ExtendedMask = 0b_1_00000000_00000000_00000000;
            var IsExtended = (lParam & ExtendedMask) != 0;

            const uint RepeatMask = 0xFF;
            var RepeatCount = lParam & RepeatMask;

            const uint WasDownMask = 0b_01000000_00000000_00000000_00000000;
            var WasDown = (lParam & WasDownMask) != 0;

            const uint NowReleasedMask = 0b_10000000_00000000_00000000_00000000;
            var NowReleased = (lParam & NowReleasedMask) != 0;


            var KeyDown = !NowReleased
                ? new KeyDown(Key, IsExtended)
                : null
                ;

            var TextClick = default(TextClick);
            
            if (State.TryGetCharFromKeyboardState(Key, (int) ScanCode, 0, out var chars)) {
                var Text = new StringBuilder();
                for (int i = 0; i < RepeatCount; i++) {
                    Text.Append(chars);
                }
                if(Text.Length > 0) {
                    TextClick = new TextClick(Text.ToString());
                }
            }
            
            var KeyUp = WasDown && NowReleased
                ? new KeyUp(Key, IsExtended)
                : null
                ;

            var Data = new KeyboardEvent(Wait, KeyDown, TextClick, KeyUp);


            var ret = InvokeMany(Data, data, timestamp);

            return ret.Next_Hook_Enabled; 
        }

    }

}