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

        protected override HookHandle? Subscribe() {

            var ret = default(HookHandle?);

            if(System.Diagnostics.Process.GetCurrentProcess().MainModule?.BaseAddress is { } Handle) {
                ret = HookHandle.Create(
                    HookType.GlobalKeyboard,
                    HookProcedure,
                    Handle,
                    0);
            }

            


            return ret;
        }

        protected override bool Callback(CallbackData data) {

            var SW = System.Diagnostics.Stopwatch.StartNew();

            var NewData = data.ToGlobalKeyboardEventSourceCallbackData();

            //This call is required to work around a bug in Window's 'Get Keyboard State' API.  When the bug occurs, it always returns the same keyboard state.
            KeyboardState.GetKeyState(KeyCode.None);

            var e = GetInputEventArgs(NewData.Message, NewData);

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

            var TextClick = GetTextClick(NewData.Message, NewData);

            var KeyEvent = new KeyboardEvent(Wait, KeyDown, TextClick, KeyUp);

            var ret = InvokeMany(KeyEvent, NewData, e.Timestamp);

            Console.WriteLine(SW.Elapsed);

            return ret.Next_Hook_Enabled;
        }

        protected EventSourceEventArgs<KeyInput> GetInputEventArgs(GlobalKeyboardMessage Message, GlobalKeyboardEventSourceCallbackData keyboardHookStruct) {

            var keyData = keyboardHookStruct.Data.KeyCode;

            var isKeyDown = Message.IsKeyDown();
            var isKeyUp = Message.IsKeyUp();

            var isExtendedKey = keyboardHookStruct.Data.Flags.HasFlag(KeyboardHookStructFlags.Extended);

            var Status = KeyStatusValue.Compute(isKeyDown, isKeyUp);

            var Data = new KeyInput(keyData, isExtendedKey, keyboardHookStruct.Data.ScanCode, Status);
            var ret = EventSourceEventArgs.Create(keyboardHookStruct.Data.Time, Data, keyboardHookStruct);

            return ret;
        }

        protected TextClick? GetTextClick(GlobalKeyboardMessage Message, GlobalKeyboardEventSourceCallbackData keyboardHookStruct) {

            var ret = default(TextClick?);

            //System.Diagnostics.Debug.WriteLine($@"{Message}: {keyboardHookStruct.ScanCode} => {keyboardHookStruct.KeyCode}" );

            if (Message.IsKeyDown()) {

                var virtualKeyCode = keyboardHookStruct.Data.KeyCode;
                var scanCode = keyboardHookStruct.Data.ScanCode;
                var fuState = keyboardHookStruct.Data.Flags;

                if (keyboardHookStruct.Data.KeyCode == KeyCode.Packet) {
                    var ch = (char)scanCode;

                    ret = new TextClick(new[] { ch });
                } else {
                    var UnicodeFlags = ToUnicodeExFlags.None;
                    
                    if (fuState.HasFlag(KeyboardHookStructFlags.Extended)) {
                        UnicodeFlags |= ToUnicodeExFlags.Menu;
                    }

                    if (State.TryGetCharFromKeyboardState(virtualKeyCode, scanCode, UnicodeFlags, out var chars)) {
                        if (chars is { } V1 && V1.Length > 0) {
                            ret = new TextClick(V1);
                        }
                    }

                }
            }

            return ret;
        }

    }
}