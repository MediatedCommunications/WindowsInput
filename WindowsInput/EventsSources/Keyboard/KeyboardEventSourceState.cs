using System;
using System.Runtime.InteropServices;
using System.Text;
using WindowsInput.Native;

namespace WindowsInput.Events.Sources {
    public class KeyboardEventSourceState {
        public DateTimeOffset LastInputDate { get; set; } = DateTimeOffset.Now;

        //Used to pass Unicode characters as if they were keystrokes. The VK_PACKET key is the low word of a 32-bit Virtual Key value used for non-keyboard input methods
        private KeyCode lastVirtualKeyCode;

        private int           lastScanCode;
        private KeyboardState lastKeyState = KeyboardState.Blank();
        private bool          lastIsDead;



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
            chars = default;

            var Keyboard = KeyboardState.Current();
            var isDead   = false;

            if (Keyboard[KeyCode.LShift].IsDown() || Keyboard[KeyCode.RShift].IsDown()) {
                Keyboard[KeyCode.Shift] |= KeyboardKeyState.KeyDown;
            }

            if (Keyboard[KeyCode.CapsLock].IsToggled()) {
                Keyboard[KeyCode.CapsLock] |= KeyboardKeyState.Toggled;
            }

            //var ConversionStatus = ToUnicodeEx((KeyCode)virtualKeyCode, scanCode, currentKeyboardState, pwszBuff, pwszBuff.Capacity, fuState, Layout.Handle);

            var ConversionStatus = ToUnicodeEx(Layout, Keyboard, fuState, virtualKeyCode, scanCode, out var Characters);

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

            if (lastVirtualKeyCode == 0 || !lastIsDead) {
                lastScanCode       = scanCode;
                lastVirtualKeyCode = virtualKeyCode;
                lastIsDead         = isDead;
                lastKeyState       = Keyboard.Clone();
            } else if(chars is {}) {
                //ToUnicodeEx(lastVirtualKeyCode, lastScanCode, lastKeyState.State, sbTemp, sbTemp.Capacity, 0, Layout.Handle);

                ToUnicodeEx(Layout, lastKeyState, ToUnicodeExFlags.None, lastVirtualKeyCode, lastScanCode, out _);

                lastIsDead         = false;
                lastVirtualKeyCode = 0;

            }

            return chars != null;
        }

        private void ClearKeyboardBuffer(KeyCode vk, int sc, KeyboardLayout Layout) {
            var sb = new StringBuilder(10);

            while (ToUnicodeEx(Layout, KeyboardState.Blank(), ToUnicodeExFlags.None, vk, sc, out _) == ToUnicodeExStatus.DeadKey) {
                //Do nothing.  Just eat through the characters
            }

        }


        [DllImport("user32.dll", EntryPoint = "ToUnicodeEx")]
        private static extern int ToUnicodeEx2(
            int                wVirtKey,
            int                wScanCode,
            KeyboardKeyState[] lpKeyState,
            [Out] [MarshalAs(UnmanagedType.LPWStr, SizeConst = 64)]
            StringBuilder pwszBuff,
            int              cchBuff,
            ToUnicodeExFlags wFlags,
            IntPtr           dwhkl);

        public static int ToUnicodeEx(
            KeyCode            wVirtKey,
            int                wScanCode,
            KeyboardKeyState[] lpKeyState,
            [Out] [MarshalAs(UnmanagedType.LPWStr, SizeConst = 64)]
            StringBuilder pwszBuff,
            int              cchBuff,
            ToUnicodeExFlags wFlags,
            IntPtr           dwhkl) {


            return ToUnicodeEx2((int) wVirtKey, wScanCode, lpKeyState, pwszBuff, cchBuff, wFlags, dwhkl);

        }



        public static ToUnicodeExStatus ToUnicodeEx(KeyboardLayout Layout, KeyboardState Keyboard, ToUnicodeExFlags Flags, KeyCode VKey, int SKey, out string Value) {
            Value = default;

            var Buffer = new StringBuilder(64);
            var K      = ToUnicodeEx(VKey, SKey, Keyboard.State, Buffer, Buffer.Capacity, Flags, Layout.Handle);
            if (K > 0) {
                var Content = Buffer.ToString();

                var Length = Math.Min(K, Content.Length);

                Value = Content.Substring(0, Length);
            }

            var ret = K switch
            {
                -1 => ToUnicodeExStatus.DeadKey,
                0  => ToUnicodeExStatus.NoTranslation,
                _  => ToUnicodeExStatus.Success
            };

            return ret;
        }

    }
}