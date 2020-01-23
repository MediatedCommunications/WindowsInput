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
        /// The GetMessageExtraInfo function retrieves the extra message information for the current thread. Extra message information is an application- or driver-defined value associated with the current thread's message queue. 
        /// </summary>
        /// <returns></returns>
        /// <remarks>To set a thread's extra message information, use the SetMessageExtraInfo function. </remarks>
        [DllImport("user32.dll")]
        public static extern IntPtr GetMessageExtraInfo();

        /// <summary>
        /// Used to find the keyboard input scan code for single key input. Some applications do not receive the input when scan is not set.
        /// </summary>
        /// <param name="uCode"></param>
        /// <param name="uMapType"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern UInt32 MapVirtualKey(UInt32 uCode, UInt32 uMapType);
    }
}