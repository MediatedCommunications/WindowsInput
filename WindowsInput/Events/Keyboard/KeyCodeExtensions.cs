// This code is distributed under MIT license. 
// Copyright (c) 2010-2018 George Mamaladze
// See license.txt or https://mit-license.org/

using System.Collections.Generic;
using WindowsInput;

namespace WindowsInput.Events {

    public static class KeyCodeExtensions {

        /*
        public static KeyCode Normalize(this KeyCode key) {
            var ret = key;
            
            if(key.HasFlag(KeyCode.LControl) || key.HasFlag(KeyCode.RControl)) {
                ret = KeyCode.ControlModifier;
            } else if (key.HasFlag(KeyCode.LShift) || key.HasFlag(KeyCode.RShift)) {
                ret = KeyCode.ShiftModifier;
            } else if (key.HasFlag(KeyCode.LMenu) || key.HasFlag(KeyCode.RMenu)) {
                ret = KeyCode.AltModifier;
            }

            return ret;
        }
        */

        private static HashSet<KeyCode> ExtendedKeys = new HashSet<KeyCode>() {
                KeyCode.RAlt
                , KeyCode.RControl
                , KeyCode.Insert
                , KeyCode.Delete
                , KeyCode.Home
                , KeyCode.End
                , KeyCode.PageUp
                , KeyCode.PageDown
                , KeyCode.Right
                , KeyCode.Up
                , KeyCode.Left
                , KeyCode.Down
                , KeyCode.NumLock
                , KeyCode.Cancel
                , KeyCode.PrintScreen
                , KeyCode.Divide
        };

        /// <summary>
        /// Determines if the <see cref="KeyCode"/> is an ExtendedKey
        /// </summary>
        /// <param name="key">The key code.</param>
        /// <returns>true if the key code is an extended key; otherwise, false.</returns>
        /// <remarks>
        /// The extended keys consist of the ALT and CTRL keys on the right-hand side of the keyboard; the INS, DEL, HOME, END, PAGE UP, PAGE DOWN, and arrow keys in the clusters to the left of the numeric keypad; the NUM LOCK key; the BREAK (CTRL+PAUSE) key; the PRINT SCRN key; and the divide (/) and ENTER keys in the numeric keypad.
        /// 
        /// See http://msdn.microsoft.com/en-us/library/ms646267(v=vs.85).aspx Section "Extended-Key Flag"
        /// </remarks>
        public static bool IsExtended(this KeyCode key) {
            var ret = ExtendedKeys.Contains(key);

            return ret;
        }


    }
}