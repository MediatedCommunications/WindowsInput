// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System;
using System.Runtime.InteropServices;

namespace WindowsInput.Native {
    public enum WindowMessage : uint {
        //values from Winuser.h in Microsoft SDK.

        WM_NULL = 0x0000,

        /// <summary>
        ///     The WM_MOUSEMOVE message is posted to a window when the cursor moves.
        /// </summary>
        WM_MOUSEMOVE = 0x200,
        WM_MOUSEMOVE_NC = 0xA0,

        /// <summary>
        ///     The WM_LBUTTONDOWN message is posted when the user presses the left mouse button
        /// </summary>
        WM_LBUTTONDOWN = 0x201,
        WM_LBUTTONDOWN_NC = 0x00A1,

        /// <summary>
        ///     The WM_RBUTTONDOWN message is posted when the user presses the right mouse button
        /// </summary>
        WM_RBUTTONDOWN = 0x204,
        WM_RBUTTONDOWN_NC = 0x00A4,

        /// <summary>
        ///     The WM_MBUTTONDOWN message is posted when the user presses the middle mouse button
        /// </summary>
        WM_MBUTTONDOWN = 0x207,
        WM_MBUTTONDOWN_NC = 0x00A7,

        /// <summary>
        ///     The WM_LBUTTONUP message is posted when the user releases the left mouse button
        /// </summary>
        WM_LBUTTONUP = 0x202,
        WM_LBUTTONUP_NC = 0x00A2,

        /// <summary>
        ///     The WM_RBUTTONUP message is posted when the user releases the right mouse button
        /// </summary>
        WM_RBUTTONUP = 0x205,
        WM_RBUTTONUP_NC = 0x00A5,

        /// <summary>
        ///     The WM_MBUTTONUP message is posted when the user releases the middle mouse button
        /// </summary>
        WM_MBUTTONUP = 0x208,
        WM_MBUTTONUP_NC = 0x00A8,

        /// <summary>
        ///     The WM_LBUTTONDBLCLK message is posted when the user double-clicks the left mouse button
        /// </summary>
        WM_LBUTTONDBLCLK = 0x203,
        WM_LBUTTONDBLCLK_NC = 0x00A3,

        /// <summary>
        ///     The WM_RBUTTONDBLCLK message is posted when the user double-clicks the right mouse button
        /// </summary>
        WM_RBUTTONDBLCLK = 0x206,
        WM_RBUTTONDBLCLK_NC = 0x00A6,

        /// <summary>
        ///     The WM_RBUTTONDOWN message is posted when the user presses the right mouse button
        /// </summary>
        WM_MBUTTONDBLCLK = 0x209,
        WM_MBUTTONDBLCLK_NC = 0x00A9,

        /// <summary>
        ///     The WM_MOUSEWHEEL message is posted when the user presses the mouse wheel.
        /// </summary>
        WM_MOUSEWHEEL_V = 0x020A,
        WM_MOUSEWHEEL_H = 0x20E,


        /// <summary>
        ///     The WM_XBUTTONDOWN message is posted when the user presses the first or second X mouse
        ///     button.
        /// </summary>
        WM_XBUTTONDOWN = 0x20B,
        WM_XBUTTONDOWN_NC = 0x00AB,

        /// <summary>
        ///     The WM_XBUTTONUP message is posted when the user releases the first or second X  mouse
        ///     button.
        /// </summary>
        WM_XBUTTONUP = 0x20C,
        WM_XBUTTONUP_NC = 0x00AC,

        /// <summary>
        ///     The WM_XBUTTONDBLCLK message is posted when the user double-clicks the first or second
        ///     X mouse button.
        /// </summary>
        /// <remarks>Only windows that have the CS_DBLCLKS style can receive WM_XBUTTONDBLCLK messages.</remarks>
        WM_XBUTTONDBLCLK = 0x20D,
        WM_XBUTTONDBLCLK_NC = 0x00AD,



        /// <summary>
        ///     The WM_KEYDOWN message is posted to the window with the keyboard focus when a non-system
        ///     key is pressed. A non-system key is a key that is pressed when the ALT key is not pressed.
        /// </summary>
        WM_KEYDOWN = 0x100,

        /// <summary>
        ///     The WM_KEYUP message is posted to the window with the keyboard focus when a non-system
        ///     key is released. A non-system key is a key that is pressed when the ALT key is not pressed,
        ///     or a keyboard key that is pressed when a window has the keyboard focus.
        /// </summary>
        WM_KEYUP = 0x101,

        /// <summary>
        ///     The WM_SYSKEYDOWN message is posted to the window with the keyboard focus when the user
        ///     presses the F10 key (which activates the menu bar) or holds down the ALT key and then
        ///     presses another key. It also occurs when no window currently has the keyboard focus,
        ///     in this case, the WM_SYSKEYDOWN message is sent to the active window. The window that
        ///     receives the message can distinguish between these two contexts by checking the context
        ///     code in the lParam parameter.
        /// </summary>
        WM_SYSKEYDOWN = 0x104,

    }


    public static class WindowMessageDispatcher {
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessageTimeout(IntPtr hWnd, WindowMessage msg, UIntPtr wParam, IntPtr lParam, SendMessageTimeoutFlags fuFlags, uint uTimeout, out UIntPtr lpdwResult);

    }


    [Flags]
    public enum SendMessageTimeoutFlags : uint {
        SMTO_NORMAL = 0x0,
        SMTO_BLOCK = 0x1,
        SMTO_ABORTIFHUNG = 0x2,
        SMTO_NOTIMEOUTIFNOTHUNG = 0x8,
        SMTO_ERRORONEXIT = 0x20,
    }

}