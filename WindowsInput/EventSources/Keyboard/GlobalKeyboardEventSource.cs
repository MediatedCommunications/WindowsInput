// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using WindowsInput.Native;

using WindowsInput.Events;

namespace WindowsInput.EventSources {
    public class GlobalKeyboardEventSource : KeyboardEventSource {

        protected override HookHandle Subscribe() {

            return HookHandle.Create(
               HookType.GlobalKeyboard,
               HookProcedure,
               System.Diagnostics.Process.GetCurrentProcess().MainModule.BaseAddress,
               0);

        }

        protected override bool Callback(CallbackData data) {
            var Message = (GlobalKeyboardMessage)data.WParam;
            var keyboardHookStruct = Marshal.PtrToStructure<KeyboardHookStruct>(data.LParam);

            var e = GetInputEventArgs(Message, keyboardHookStruct);

            var Input = e.Data;
            var Wait = State.LastInputDate != null ? new Wait(e.Timestamp - State.LastInputDate.Value) : null;
            State.LastInputDate = e.Timestamp;

            var KeyDown = Input.Status == KeyStatus.Pressed
                ? new KeyDown(e.Data.Key, e.Data.Extended)
                : null
                ;

            var KeyUp = Input.Status == KeyStatus.Released
                ? new KeyUp(e.Data.Key, e.Data.Extended)
                : null
                ;

            var TextClick = GetTextClick(Message, keyboardHookStruct);

            var KeyEvent = new KeyboardEvent(Wait, KeyDown, KeyUp, TextClick);

            var Handled = InvokeEvent(
                () => InvokeEvent(KeyEvent, e.Timestamp),

                () => InvokeEvent(Wait, e.Timestamp),

                () => InvokeEvent(KeyDown, e.Timestamp),
                () => InvokeEvent(TextClick, e.Timestamp),
                () => InvokeEvent(KeyUp, e.Timestamp)
            );

            return !Handled;
        }


        protected EventSourceEventArgs<KeyInput> GetInputEventArgs(GlobalKeyboardMessage Message, KeyboardHookStruct keyboardHookStruct) {

            var keyData = keyboardHookStruct.KeyCode;

            var isKeyDown = Message.IsKeyDown();
            var isKeyUp = Message.IsKeyUp();

            var isExtendedKey = keyboardHookStruct.Flags.HasFlag(KeyboardHookStructFlags.Extended);

            var Status = KeyStatusValue.Compute(isKeyDown, isKeyUp);

            var Data = new KeyInput(keyData, isExtendedKey, keyboardHookStruct.ScanCode, Status);
            var ret = EventSourceEventArgs.Create(keyboardHookStruct.Time, false, Data);

            return ret;
        }

        protected TextClick GetTextClick(GlobalKeyboardMessage Message, KeyboardHookStruct keyboardHookStruct) {

            var ret = default(TextClick);

            if (Message.IsKeyDown()) {

                var virtualKeyCode = keyboardHookStruct.KeyCode;
                var scanCode = keyboardHookStruct.ScanCode;
                var fuState = keyboardHookStruct.Flags;

                if (keyboardHookStruct.KeyCode == KeyCode.Packet) {
                    var ch = (char)scanCode;

                    ret = new TextClick(new[] { ch });
                } else {

                    if (KeyboardNativeMethods.TryGetCharFromKeyboardState((int)virtualKeyCode, scanCode, (int)fuState, out var chars)) {
                        ret = new TextClick(chars);
                    }

                }
            }

            return ret;
        }

    }
}