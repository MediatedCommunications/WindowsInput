// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using WindowsInput.Events;
using WindowsInput.Native;

namespace WindowsInput.Events.Sources {
    public class KeyboardEventSourceState {
        public DateTimeOffset LastInputDate { get; set; } = DateTimeOffset.Now;

        //Used to pass Unicode characters as if they were keystrokes. The VK_PACKET key is the low word of a 32-bit Virtual Key value used for non-keyboard input methods
        private KeyCode lastVirtualKeyCode;

        private int lastScanCode;
        private KeyboardState lastKeyState = KeyboardState.Blank();
        private bool lastIsDead;



        /// <summary>
        ///     Translates a virtual key to its character equivalent using the current keyboard layout
        /// </summary>
        /// <param name="virtualKeyCode"></param>
        /// <param name="scanCode"></param>
        /// <param name="fuState"></param>
        /// <param name="chars"></param>
        /// <returns></returns>
        public bool TryGetCharFromKeyboardState(KeyCode virtualKeyCode, int scanCode, ToUnicodeExFlags fuState, out string chars) {
            var Layout = KeyboardLayout.Current(); //get the active keyboard layout
            return TryGetCharFromKeyboardState(virtualKeyCode, scanCode, fuState, Layout, out chars);
        }

        public bool TryGetCharFromKeyboardState(KeyCode virtualKeyCode, int scanCode, ToUnicodeExFlags fuState, KeyboardLayout Layout, out string chars) {
            if (SWITCHES.Windows10_AtLeast_v1607_Enabled) {
                return TryGetCharFromKeyboardState1(virtualKeyCode, scanCode, fuState, Layout, out chars);
            } else {
                return TryGetCharFromKeyboardState2(virtualKeyCode, scanCode, fuState, Layout, out chars);
            }
        }


        //On Windows 10 v 1607 and above we can do the following because of the "DoNotChangeKeyboardState" member.  This is a lot more reliable when
        //Dealing with dead keys.
        private bool TryGetCharFromKeyboardState1(KeyCode virtualKeyCode, int scanCode, ToUnicodeExFlags fuState, KeyboardLayout Layout, out string chars) {

            var Keyboard = KeyboardState.Current();
            var ConversionStatus = ToUnicodeEx(Layout, Keyboard, ToUnicodeExFlags.DoNotChangeKeyboardState, virtualKeyCode, scanCode, out var Characters);
            chars = Characters;

            return chars != null;
        }

        /// <summary>
        ///     Translates a virtual key to its character equivalent using a specified keyboard layout
        /// </summary>
        /// <param name="virtualKeyCode"></param>
        /// <param name="scanCode"></param>
        /// <param name="fuState"></param>
        /// <param name="Layout"></param>
        /// <param name="chars"></param>
        /// <returns></returns>
        public bool TryGetCharFromKeyboardState2(KeyCode virtualKeyCode, int scanCode, ToUnicodeExFlags fuState, KeyboardLayout Layout, out string chars) {
            chars = default;

            var Keyboard = KeyboardState.Current();
            var isDead = false;

            if (Keyboard.IsDown(KeyCode.LShift) || Keyboard.IsDown(KeyCode.RShift))
                Keyboard[KeyCode.Shift] |= KeyboardKeyState.KeyDown;

            if (Keyboard.IsToggled(KeyCode.CapsLock))
                Keyboard[KeyCode.CapsLock] |= KeyboardKeyState.Toggled;

            //var ConversionStatus = ToUnicodeEx((KeyCode)virtualKeyCode, scanCode, currentKeyboardState, pwszBuff, pwszBuff.Capacity, fuState, Layout.Handle);

            var ConversionStatus = ToUnicodeEx(Layout, Keyboard, ToUnicodeExFlags.None, virtualKeyCode, scanCode, out var Characters);

            switch (ConversionStatus) {
                case ToUnicodeExStatus.DeadKey:
                    isDead = true;
                    ClearKeyboardBuffer(virtualKeyCode, scanCode, Layout);
                    break;

                case ToUnicodeExStatus.NoTranslation:
                    break;

                case ToUnicodeExStatus.Success:
                    chars = Characters;
                    break;
            }

            if (lastVirtualKeyCode != 0 && lastIsDead && chars != null) {
                //ToUnicodeEx(lastVirtualKeyCode, lastScanCode, lastKeyState.State, sbTemp, sbTemp.Capacity, 0, Layout.Handle);

                ToUnicodeEx(Layout, lastKeyState, ToUnicodeExFlags.None, lastVirtualKeyCode, lastScanCode, out _ );

                lastIsDead = false;
                lastVirtualKeyCode = 0;
            } else {
                lastScanCode = scanCode;
                lastVirtualKeyCode = virtualKeyCode;
                lastIsDead = isDead;
                lastKeyState = Keyboard.Clone();
            }

            return chars != null;
        }

        private void ClearKeyboardBuffer(KeyCode vk, int sc, KeyboardLayout Layout) {
            var sb = new StringBuilder(10);

            while (ToUnicodeEx(Layout, KeyboardState.Blank(), ToUnicodeExFlags.None, vk, sc, out _) == ToUnicodeExStatus.DeadKey) {
                //Do nothing.  Just eat through the characters
            }

        }


        [DllImport("user32.dll")]
        public static extern int ToUnicodeEx(
            KeyCode wVirtKey,
            int wScanCode,
            KeyboardKeyState[] lpKeyState,
            [Out] [MarshalAs(UnmanagedType.LPWStr, SizeConst = 64)] StringBuilder pwszBuff,
            int cchBuff,
            ToUnicodeExFlags wFlags,
            IntPtr dwhkl);


        public static string ToUnicodeEx(KeyCode VKey, int SKey, KeyboardLayout Layout, KeyboardState Keyboard) {
            var ret = "";

            var Buffer = new StringBuilder(64);
            var K = ToUnicodeEx(VKey, SKey, Keyboard.State, Buffer, Buffer.Capacity, 0, Layout.Handle);
            if (K >= 0) {
                ret = Buffer.ToString().Substring(0, K);
            }

            return ret;
        }

        public static ToUnicodeExStatus ToUnicodeEx(KeyboardLayout Layout, KeyboardState Keyboard, ToUnicodeExFlags Flags, KeyCode VKey, int SKey, out string Value) {
            Value = default;

            var Buffer = new StringBuilder(64);
            var K = ToUnicodeEx(VKey, SKey, Keyboard.State, Buffer, Buffer.Capacity, Flags, Layout.Handle);
            if (K >= 0) {
                Value = Buffer.ToString().Substring(0, K);
            }

            var ret = K switch
            {
                -1 => ToUnicodeExStatus.DeadKey,
                0 => ToUnicodeExStatus.NoTranslation,
                _ => ToUnicodeExStatus.Success
            };

            return ret;
        }

    }

    public enum ToUnicodeExFlags : uint {
        None = 0,
        Menu = 0x0001,
        Unknown = 0x0002,
        //This flag is only present on Windows10 v1607 and above
        DoNotChangeKeyboardState = 0x0004
    }

    public enum ToUnicodeExStatus {
        DeadKey = -1,
        NoTranslation = 0,
        Success = 1
    }


}