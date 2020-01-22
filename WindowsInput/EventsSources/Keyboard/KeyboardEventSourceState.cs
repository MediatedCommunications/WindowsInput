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
        private int lastVirtualKeyCode;

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
        public bool TryGetCharFromKeyboardState(int virtualKeyCode, int scanCode, int fuState, out char[] chars) {
            var dwhkl = KeyboardLayout.Current(); //get the active keyboard layout
            return TryGetCharFromKeyboardState(virtualKeyCode, scanCode, fuState, dwhkl.Handle, out chars);
        }

        /// <summary>
        ///     Translates a virtual key to its character equivalent using a specified keyboard layout
        /// </summary>
        /// <param name="virtualKeyCode"></param>
        /// <param name="scanCode"></param>
        /// <param name="fuState"></param>
        /// <param name="dwhkl"></param>
        /// <param name="chars"></param>
        /// <returns></returns>
        public bool TryGetCharFromKeyboardState(int virtualKeyCode, int scanCode, int fuState, IntPtr dwhkl, out char[] chars) {
            Debug.WriteLine($@"{virtualKeyCode} {scanCode} {fuState} {dwhkl}");
            
            
            var pwszBuff = new StringBuilder(64);
            var keyboardState = KeyboardState.Current();
            var currentKeyboardState = keyboardState.State;
            var isDead = false;

            if (keyboardState.IsDown(KeyCode.Shift))
                currentKeyboardState[(byte)KeyCode.Shift] |= KeyboardKeyState.KeyDown;

            if (keyboardState.IsToggled(KeyCode.CapsLock))
                currentKeyboardState[(byte)KeyCode.CapsLock] |= KeyboardKeyState.Toggled;

            var relevantChars = ToUnicodeEx((KeyCode)virtualKeyCode, scanCode, currentKeyboardState, pwszBuff, pwszBuff.Capacity,
                fuState, dwhkl);

            switch (relevantChars) {
                case -1:
                    isDead = true;
                    ClearKeyboardBuffer(virtualKeyCode, scanCode, dwhkl);
                    chars = null;
                    break;

                case 0:
                    chars = null;
                    break;

                case 1:
                    if (pwszBuff.Length > 0) chars = new[] { pwszBuff[0] };
                    else chars = null;
                    break;

                // Two or more (only two of them is relevant)
                default:
                    if (pwszBuff.Length > 1) chars = new[] { pwszBuff[0], pwszBuff[1] };
                    else chars = new[] { pwszBuff[0] };
                    break;
            }

            if (lastVirtualKeyCode != 0 && lastIsDead && chars != null) {
                var sbTemp = new StringBuilder(5);
                ToUnicodeEx((KeyCode)lastVirtualKeyCode, lastScanCode, lastKeyState.State, sbTemp, sbTemp.Capacity, 0, dwhkl);
                lastIsDead = false;
                lastVirtualKeyCode = 0;
            } else {
                lastScanCode = scanCode;
                lastVirtualKeyCode = virtualKeyCode;
                lastIsDead = isDead;
                lastKeyState = keyboardState.Clone();
            }

            return chars != null;
        }

        private void ClearKeyboardBuffer(int vk, int sc, IntPtr hkl) {
            var sb = new StringBuilder(10);

            while (ToUnicodeEx((KeyCode)vk, sc, KeyboardState.Blank().State, sb, sb.Capacity, 0, hkl) < 0) {
                //Do nothing.  Just eat through the characters
            }

        }


        /// <summary>
        ///     Translates the specified virtual-key code and keyboard state to the corresponding Unicode character or characters.
        /// </summary>
        /// <param name="wVirtKey">[in] The virtual-key code to be translated.</param>
        /// <param name="wScanCode">
        ///     [in] The hardware scan code of the key to be translated. The high-order bit of this value is
        ///     set if the key is up.
        /// </param>
        /// <param name="lpKeyState">
        ///     [in, optional] A pointer to a 256-byte array that contains the current keyboard state. Each
        ///     element (byte) in the array contains the state of one key. If the high-order bit of a byte is set, the key is down.
        /// </param>
        /// <param name="pwszBuff">
        ///     [out] The buffer that receives the translated Unicode character or characters. However, this
        ///     buffer may be returned without being null-terminated even though the variable name suggests that it is
        ///     null-terminated.
        /// </param>
        /// <param name="cchBuff">[in] The size, in characters, of the buffer pointed to by the pwszBuff parameter.</param>
        /// <param name="wFlags">
        ///     [in] The behavior of the function. If bit 0 is set, a menu is active. Bits 1 through 31 are
        ///     reserved.
        /// </param>
        /// <param name="dwhkl">The input locale identifier used to translate the specified code.</param>
        /// <returns>
        ///     -1 &lt;= return &lt;= n
        ///     <list type="bullet">
        ///         <item>
        ///             -1    = The specified virtual key is a dead-key character (accent or diacritic). This value is returned
        ///             regardless of the keyboard layout, even if several characters have been typed and are stored in the
        ///             keyboard state. If possible, even with Unicode keyboard layouts, the function has written a spacing version
        ///             of the dead-key character to the buffer specified by pwszBuff. For example, the function writes the
        ///             character SPACING ACUTE (0x00B4), rather than the character NON_SPACING ACUTE (0x0301).
        ///         </item>
        ///         <item>
        ///             0    = The specified virtual key has no translation for the current state of the keyboard. Nothing was
        ///             written to the buffer specified by pwszBuff.
        ///         </item>
        ///         <item> 1    = One character was written to the buffer specified by pwszBuff.</item>
        ///         <item>
        ///             n    = Two or more characters were written to the buffer specified by pwszBuff. The most common cause
        ///             for this is that a dead-key character (accent or diacritic) stored in the keyboard layout could not be
        ///             combined with the specified virtual key to form a single character. However, the buffer may contain more
        ///             characters than the return value specifies. When this happens, any extra characters are invalid and should
        ///             be ignored.
        ///         </item>
        ///     </list>
        /// </returns>
        [DllImport("user32.dll")]
        public static extern int ToUnicodeEx(KeyCode wVirtKey,
            int wScanCode,
            KeyboardKeyState[] lpKeyState,
            [Out] [MarshalAs(UnmanagedType.LPWStr, SizeConst = 64)] StringBuilder pwszBuff,
            int cchBuff,
            int wFlags,
            IntPtr dwhkl);


        public static string ToUnicodeEx(KeyCode VKey, int SKey, IntPtr KeyboardLayout, KeyboardState Keyboard) {
            var ret = "";

            var Buffer = new StringBuilder(64);
            var K = ToUnicodeEx(VKey, SKey, Keyboard.State, Buffer, Buffer.Capacity, 0, KeyboardLayout);
            if (K >= 0) {
                ret = Buffer.ToString().Substring(0, K);
            }

            return ret;
        }

    }
}