// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System;
using System.Collections.Generic;
using System.Linq;
using WindowsInput.Native;
using WindowsInput;
using WindowsInput.Events;
using System.Runtime.InteropServices;

namespace WindowsInput.Native {




    /// <summary>
    ///     Contains a snapshot of a keyboard state at certain moment and provides methods
    ///     of querying whether specific keys are pressed or locked.
    /// </summary>
    /// <remarks>
    ///     This class is basically a managed wrapper of GetKeyboardState API function
    ///     http://msdn.microsoft.com/en-us/library/ms646299
    /// </remarks>
    public class KeyboardState {
        public KeyboardKeyState[] State { get; } = new KeyboardKeyState[256];

        public IDictionary<KeyCode, KeyboardKeyState> ToDictionary() {
            var ret = new Dictionary<KeyCode, KeyboardKeyState>();
            for (int i = 0; i < State.Length; i++) {
                if(State[i] != KeyboardKeyState.Default) {
                    ret[(KeyCode)i] = State[i];
                }
            }

            return ret;
        }

        public KeyboardKeyState this[byte index] {
            get => State[index];
            set => State[index] = value;
        }

        public KeyboardKeyState this[int index] {
            get => State[index];
            set => State[index] = value;
        }

        public KeyboardKeyState this[KeyCode index] {
            get => State[(int)index];
            set => State[(int)index] = value;
        }


        public KeyboardState Clone() {
            var ret = new KeyboardState();

            Array.Copy(this.State, 0, ret.State, 0, ret.State.Length);

            return ret;
        }

        private KeyboardState() {
            
        }

        public static KeyboardState Current() {
            var ret = new KeyboardState();
            var RVal = GetKeyboardState(ret.State);

            return ret;
        }

        public static KeyboardState Blank() {
            var ret = new KeyboardState();

            return ret;
        }



        /// <summary>
        ///     The GetKeyboardState function copies the status of the 256 virtual keys to the
        ///     specified buffer.
        /// </summary>
        /// <param name="pbKeyState">
        ///     [in] Pointer to a 256-byte array that contains keyboard key states.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero.
        ///     If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </returns>
        /// <remarks>
        ///     http://msdn.microsoft.com/library/default.asp?url=/library/en-us/winui/winui/windowsuserinterface/userinput/keyboardinput/keyboardinputreference/keyboardinputfunctions/toascii.asp
        /// </remarks>
        [DllImport("user32.dll")]
        protected static extern int GetKeyboardState(KeyboardKeyState[] pbKeyState);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern KeyboardKeyState GetAsyncKeyState(int virtualKeyCode);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern KeyboardKeyState GetKeyState(int virtualKeyCode);

        public static KeyboardKeyState GetAsyncKeyState(KeyCode Key) => GetAsyncKeyState((int) Key);
        public static KeyboardKeyState GetKeyState(KeyCode Key) => GetKeyState((int)Key);


    }

    [Flags]
    public enum KeyboardKeyState : byte {
        Default = 0b_0000_0000,
        KeyDown = 0b_1000_0000,
        Toggled = 0b_0000_0001,
    }

    public static class KeyboardKeyStateExtensions {
        public static bool IsDown(this KeyboardKeyState This) {
            var ret = This.HasFlag(KeyboardKeyState.KeyDown)
                ;
            return ret;
        }

        public static bool IsUp(this KeyboardKeyState This) {
            var ret = !This.IsDown()
                ;
            return ret;
        }

        public static bool IsToggled(this KeyboardKeyState This) {
            var ret = This.HasFlag(KeyboardKeyState.Toggled)
                ;
            return ret;
        }


    }

}