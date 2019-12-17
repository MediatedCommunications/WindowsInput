// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System;
using System.Runtime.InteropServices;

namespace WindowsInput.Native {
    /// <summary>
    ///     The AppMouseStruct structure contains information about a application-level mouse input event.
    /// </summary>
    /// <remarks>
    ///     See full documentation at http://globalmousekeyhook.codeplex.com/wikipage?title=MouseStruct
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct MOUSEHOOKSTRUCTEX {
        /// <summary>
        ///     Specifies a Point structure that contains the X- and Y-coordinates of the cursor, in screen coordinates.
        /// </summary>
        public POINT Point;
        public IntPtr Hwnd;
        public HitTestCode HitTestCode;
        public IntPtr ExtraInfo;
        public MouseDataStruct MouseData;
    }

    //32 Bits
    [StructLayout(LayoutKind.Explicit)]
    public struct MouseDataStruct {
        [FieldOffset(0)] public int Value;

        [FieldOffset(0)] public short LoWord;
        [FieldOffset(2)] public short HiWord;
    }

}