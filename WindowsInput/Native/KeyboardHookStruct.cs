// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System.Runtime.InteropServices;
using WindowsInput.Events;

namespace WindowsInput.Native {

    [StructLayout(LayoutKind.Explicit)]
    public struct KeyboardHookStruct {
        /// <summary>
        ///     Specifies a virtual-key code. The code must be a value in the range 1 to 254.
        /// </summary>
        [FieldOffset(0)]
        public int KeyCodeRaw;

        [FieldOffset(0)]
        public KeyCode KeyCode;

        /// <summary>
        ///     Specifies a hardware scan code for the key.
        /// </summary>
        [FieldOffset(4)]
        public int ScanCode;

        /// <summary>
        ///     Specifies the extended-key flag, event-injected flag, context code, and transition-state flag.
        /// </summary>
        [FieldOffset(8)]
        public KeyboardHookStructFlags Flags;

        /// <summary>
        ///     Specifies the Time stamp for this message.
        /// </summary>
        [FieldOffset(12)]
        public int Time;

        /// <summary>
        ///     Specifies extra information associated with the message.
        /// </summary>
        [FieldOffset(16)]
        public int ExtraInfo;
    }
}