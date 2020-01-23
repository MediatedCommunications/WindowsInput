using System;
using System.Runtime.InteropServices;

namespace WindowsInput.Native {
    /// <summary>
    /// Implements the <see cref="IInputMessageDispatcher"/> by calling <see cref="NativeMethods.SendInput"/>.
    /// </summary>
    public static class INPUTDispatcher {

        /// <summary>
        /// The SendInput function synthesizes keystrokes, mouse motions, and button clicks.
        /// </summary>
        /// <param name="numberOfInputs">Number of structures in the Inputs array.</param>
        /// <param name="inputs">Pointer to an array of INPUT structures. Each structure represents an event to be inserted into the keyboard or mouse input stream.</param>
        /// <param name="sizeOfInputStructure">Specifies the size, in bytes, of an INPUT structure. If cbSize is not the size of an INPUT structure, the function fails.</param>
        /// <returns>The function returns the number of events that it successfully inserted into the keyboard or mouse input stream. If the function returns zero, the input was already blocked by another thread. To get extended error information, call GetLastError.Microsoft Windows Vista. This function fails when it is blocked by User Interface Privilege Isolation (UIPI). Note that neither GetLastError nor the return value will indicate the failure was caused by UIPI blocking.</returns>
        /// <remarks>
        /// Microsoft Windows Vista. This function is subject to UIPI. Applications are permitted to inject input only into applications that are at an equal or lesser integrity level.
        /// The SendInput function inserts the events in the INPUT structures serially into the keyboard or mouse input stream. These events are not interspersed with other keyboard or mouse input events inserted either by the user (with the keyboard or mouse) or by calls to keybd_event, mouse_event, or other calls to SendInput.
        /// This function does not reset the keyboard's current state. Any keys that are already pressed when the function is called might interfere with the events that this function generates. To avoid this problem, check the keyboard's state with the GetAsyncKeyState function and correct as necessary.
        /// </remarks>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint SendInput(UInt32 numberOfInputs, INPUT[] inputs, Int32 sizeOfInputStructure);


        /// <summary>
        /// Dispatches the specified list of <see cref="INPUT"/> messages in their specified order by issuing a single called to <see cref="NativeMethods.SendInput"/>.
        /// </summary>
        /// <param name="inputs">The list of <see cref="INPUT"/> messages to be dispatched.</param>
        /// <exception cref="ArgumentException">If the <paramref name="inputs"/> array is empty.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="inputs"/> array is null.</exception>
        /// <exception cref="Exception">If the any of the commands in the <paramref name="inputs"/> array could not be sent successfully.</exception>
        public static uint SendInput(params INPUT[] inputs) {
            var ret = (uint)0;
            if (inputs?.Length > 0) {
                ret = SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));
            }
            
            return ret;
        }
    }
}