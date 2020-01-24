using System;
using System.Runtime.InteropServices;

namespace WindowsInput.Native
{
    /// <summary>
    /// References all of the Native Windows API methods for the WindowsInput functionality.
    /// </summary>
    public static class NativeMethods
    {

        /// <summary>
        /// Used to find the keyboard input scan code for single key input. Some applications do not receive the input when scan is not set.
        /// </summary>
        /// <param name="uCode"></param>
        /// <param name="uMapType"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern uint MapVirtualKey(UInt32 uCode, UInt32 uMapType);
    }
}