// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System;
using WindowsInput.Events;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace WindowsInput.Native {
    [DebuggerDisplay(Debugger2.DISPLAY)]
    public class KeyboardLayout {
        public IntPtr Handle { get; private set; }

        protected virtual string DebuggerDisplay {
            get {
                return $@"Handle: {Handle}";
            }
        }

        /// <summary>
        ///     Retrieves the active input locale identifier (formerly called the keyboard layout) for the specified thread.
        ///     If the idThread parameter is zero, the input locale identifier for the active thread is returned.
        /// </summary>
        /// <param name="dwLayout">[in] The identifier of the thread to query, or 0 for the current thread. </param>
        /// <returns>
        ///     The return value is the input locale identifier for the thread. The low word contains a Language Identifier for the
        ///     input
        ///     language and the high word contains a device handle to the physical layout of the keyboard.
        /// </returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr GetKeyboardLayout(int dwLayout);

        /// <summary>
        ///     Gets the input locale identifier for the active application's thread.  Using this combined with the ToUnicodeEx and
        ///     MapVirtualKeyEx enables Windows to properly translate keys based on the keyboard layout designated for the
        ///     application.
        /// </summary>
        /// <returns>HKL</returns>
        public static KeyboardLayout Current() {
            var hActiveWnd = ThreadNativeMethods.GetForegroundWindow(); //handle to focused window
            var hCurrentWnd = ThreadNativeMethods.GetWindowThreadProcessId(hActiveWnd, out var dwProcessId);
            //thread of focused window
            var Handle = GetKeyboardLayout(hCurrentWnd); //get the layout identifier for the thread whose window is focused


            var ret = new KeyboardLayout() {
                Handle = Handle
            };

            return ret;
        }

        public int ToScanCode(KeyCode Value) {
            var ret = MapVirtualKeyEx((int)Value, MapVirtualKeyConversion.MAPVK_VK_TO_VSC, Handle);
            return ret;
        }


        public CharInfo ToChar(KeyCode Value) {
            var tret = ToChar(Value, out var Dead);

            return new CharInfo() {
                Value = tret,
                DeadKey = Dead,
            };
        }

        public char? ToChar(KeyCode Value, out bool DeadKey) {
            var ret = default(char?);
            DeadKey = false;
            var tret = MapVirtualKeyEx((int)Value, MapVirtualKeyConversion.MAPVK_VK_TO_CHAR, Handle);
            if((tret & 0x80000000) != 0) {
                DeadKey = true;
            }
            ret = (char)tret;

            return ret;
        }


        [DebuggerDisplay(Debugger2.DISPLAY)]
        public class CharInfo {
            public char? Value { get; set; }
            public bool DeadKey { get; set; }

            protected virtual string DebuggerDisplay {
                get {
                    var ret = $@"(null)";
                    if(Value is { } V1) {
                        ret = $@"{V1}";

                        if (DeadKey) {
                            ret += " (Dead)";
                        }
                    }

                    return ret;
                }
            }
        }


        /// <summary>
        ///     Translates (maps) a virtual-key code into a scan code or character value, or translates a scan code into a
        ///     virtual-key code.
        /// </summary>
        /// <param name="uCode">
        ///     [in] The virtual key code or scan code for a key. How this value is interpreted depends on the
        ///     value of the uMapType parameter.
        /// </param>
        /// <param name="conversion">
        ///     [in] The translation to be performed. The value of this parameter depends on the value of the
        ///     uCode parameter.
        /// </param>
        /// <param name="dwhkl">[in] The input locale identifier used to translate the specified code.</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int MapVirtualKeyEx(int uCode, MapVirtualKeyConversion conversion, IntPtr dwhkl);

        /// <summary>
        ///     MapVirtualKeys uMapType
        /// </summary>
        private enum MapVirtualKeyConversion : int {
            /// <summary>
            ///     uCode is a virtual-key code and is translated into an unshifted character value in the low-order word of the return
            ///     value. Dead keys (diacritics) are indicated by setting the top bit of the return value. If there is no translation,
            ///     the function returns 0.
            /// </summary>
            MAPVK_VK_TO_VSC = 0,

            /// <summary>
            ///     uCode is a virtual-key code and is translated into a scan code. If it is a virtual-key code that does not
            ///     distinguish between left- and right-hand keys, the left-hand scan code is returned. If there is no translation, the
            ///     function returns 0.
            /// </summary>
            MAPVK_VSC_TO_VK = 1,

            /// <summary>
            ///     uCode is a scan code and is translated into a virtual-key code that does not distinguish between left- and
            ///     right-hand keys. If there is no translation, the function returns 0.
            /// </summary>
            MAPVK_VK_TO_CHAR = 2,

            /// <summary>
            ///     uCode is a scan code and is translated into a virtual-key code that distinguishes between left- and right-hand
            ///     keys. If there is no translation, the function returns 0.
            /// </summary>
            MAPVK_VSC_TO_VK_EX = 3,

        }


    }

}