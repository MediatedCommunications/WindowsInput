// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using WindowsInput.Native;

using WindowsInput.Events;

namespace WindowsInput.Events.Sources {
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

            //This call is required to work around a bug in Window's 'Get Keyboard State' API.  When the bug occurs, it always returns the same keyboard state.
            KeyboardState.GetKeyState(KeyCode.None);

            var e = GetInputEventArgs(Message, keyboardHookStruct);

            var Input = e.Data;
            var Wait = new Wait(e.Timestamp - State.LastInputDate);
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

            var KeyEvent = new KeyboardEvent(Wait, KeyDown, TextClick, KeyUp);

            var ret = InvokeMany(KeyEvent, e.Timestamp);

            return ret.Next_Hook_Enabled;
        }

        protected EventSourceEventArgs<KeyInput> GetInputEventArgs(GlobalKeyboardMessage Message, KeyboardHookStruct keyboardHookStruct) {

            var keyData = keyboardHookStruct.KeyCode;

            var isKeyDown = Message.IsKeyDown();
            var isKeyUp = Message.IsKeyUp();

            var isExtendedKey = keyboardHookStruct.Flags.HasFlag(KeyboardHookStructFlags.Extended);

            var Status = KeyStatusValue.Compute(isKeyDown, isKeyUp);

            var Data = new KeyInput(keyData, isExtendedKey, keyboardHookStruct.ScanCode, Status);
            var ret = EventSourceEventArgs.Create(keyboardHookStruct.Time, Data);

            return ret;
        }

        protected TextClick GetTextClick(GlobalKeyboardMessage Message, KeyboardHookStruct keyboardHookStruct) {

            var ret = default(TextClick);

            //System.Diagnostics.Debug.WriteLine($@"{Message}: {keyboardHookStruct.ScanCode} => {keyboardHookStruct.KeyCode}" );

            if (Message.IsKeyDown()) {

                var virtualKeyCode = keyboardHookStruct.KeyCode;
                var scanCode = keyboardHookStruct.ScanCode;
                var fuState = keyboardHookStruct.Flags;

                if (keyboardHookStruct.KeyCode == KeyCode.Packet) {
                    var ch = (char)scanCode;

                    ret = new TextClick(new[] { ch });
                } else {
                    var UnicodeFlags = ToUnicodeExFlags.None;
                    
                    if (fuState.HasFlag(KeyboardHookStructFlags.Extended)) {
                        UnicodeFlags |= ToUnicodeExFlags.Menu;
                    }

                    if (State.TryGetCharFromKeyboardState(virtualKeyCode, scanCode, UnicodeFlags, out var chars)) {
                        ret = new TextClick(chars);
                    }

                }
            }

            return ret;
        }

    }
}